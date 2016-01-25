using System.Globalization;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web.Http.Filters;
using Transaction = SMD.Models.DomainModels.Transaction;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Credit Scheduler 
    /// </summary>
    public class PayOutScheduler
    {
        #region Private
        // Properties 

        [Dependency]
        private static IPaypalService PaypalService { get; set; }

        [Dependency]
        private static IEmailManagerService EmailSerice { get; set; }

        private static ISMDLogger _smdLogger;
        /// <summary>
        /// Get Configured logger
        /// </summary>
        private static ISMDLogger SmdLogger
        {

            get
            {
                if (_smdLogger != null) return _smdLogger;
                _smdLogger = (UnityConfig.UnityContainer).Resolve<ISMDLogger>();
                return _smdLogger;
            }
        }

        /// <summary>
        /// Log Error
        /// </summary>
        private static void LogError(Exception exp, string requestContents)
        {
            SmdLogger.Write(exp, SMDLogCategory.Error, -1, -1, TraceEventType.Warning, "", new Dictionary<string, object> {  
            { "RequestContents", requestContents } });
        }

        /// <summary>
        /// Updates User's and Cash4Ads Accounts
        /// </summary>
        private static void UpdateAccounts(Transaction transaction, User user, User smdUser)
        {
            // Update users virtual account and add to paypal account
            Account usersPaypalAccount = user.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
            if (usersPaypalAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    transaction.Account.UserId, "Paypal"));
            }

            if (!usersPaypalAccount.AccountBalance.HasValue)
            {
                usersPaypalAccount.AccountBalance = 0;
            }

            if (transaction.CreditAmount != null)
            {
                usersPaypalAccount.AccountBalance += (decimal)transaction.CreditAmount;
                transaction.Account.AccountBalance -= (decimal)transaction.CreditAmount;

                // Update Cash4ads accounts
                Account smdUsersPaypalAccount = smdUser.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
                if (smdUsersPaypalAccount == null)
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.CollectionService_AccountNotRegistered,
                        smdUser.Id, "Paypal"));
                }

                if (!smdUsersPaypalAccount.AccountBalance.HasValue)
                {
                    smdUsersPaypalAccount.AccountBalance = 0;
                }

                smdUsersPaypalAccount.AccountBalance -= (decimal)transaction.CreditAmount;
            }
        }

        /// <summary>
        /// Looks for user cash4ads
        /// </summary>
        private static User GetCash4AdsUser(BaseDbContext dbContext)
        {
            var smdUser = dbContext.Users.FirstOrDefault(obj => obj.Email == SystemUsers.Cash4Ads);
            if (smdUser == null)
            {
                try
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.CollectionService_UserNotFound,
                        "Cash4Ads"));
                }
                catch (Exception exception)
                {
                    LogError(exception, new HttpActionExecutedContext().Request.Content.ToString());
                }
            }
            return smdUser;
        }

        /// <summary>
        /// Checks if accounts are registered
        /// </summary>
        private static void CheckAccounts(User smdUser, string preferedAccount, Transaction transaction)
        {
            if (preferedAccount == null) throw new ArgumentNullException("preferedAccount");

            if (string.IsNullOrEmpty(smdUser.PaypalCustomerId))
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    smdUser.Id, "Paypal"));
            }

            if (string.IsNullOrEmpty(preferedAccount))
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    transaction.Account.UserId, "Paypal"));
            }
        }

        #endregion
        #region Constructor

        #endregion
        #region Funcs

        /// <summary>
        /// Scheduler Initializer
        /// </summary>
        public static void SetDebitScheduler(Registry registry)
        {
            // Registration of Credit Process Scheduler Run after every 7 days 
            registry.Schedule(PerformCredit).ToRunEvery(7).Days();
        }

        /// <summary>
        /// Perform Credit work after scheduled time | 7 Days
        /// </summary>
        public static void PerformCredit()
        {

            // Initialize Service
            PaypalService = UnityConfig.UnityContainer.Resolve<IPaypalService>();
            EmailSerice = UnityConfig.UnityContainer.Resolve<IEmailManagerService>();

            // Using Base DB Context
            using (var dbContext = new BaseDbContext())
            {
                // Get UnProcessed Credit Trasactions
                var unProcessedTrasactions = dbContext.Transactions.
                    Where(trans => trans.DebitAmount == null && trans.CreditAmount != null
                && (trans.isProcessed == null || trans.isProcessed == false)).ToList();

                foreach (var transaction in unProcessedTrasactions)
                {
                    if (transaction.AccountId != null)
                    {
                        // Get User from which credit to debit 
                        var user = dbContext.Users.Find(transaction.Account.UserId);

                        // User's Prefered Account
                        var preferedAccount = user.PreferredPayoutAccount == 1
                            ? user.PaypalCustomerId
                            : user.GoogleWalletCustomerId;

                        var smdUser = GetCash4AdsUser(dbContext);

                        // Secure Transation
                        using (var tran = new TransactionScope())
                        {
                            try
                            {
                                CheckAccounts(smdUser, preferedAccount, transaction);

                                // PayPal Request Model 
                                var requestModel = new MakePaypalPaymentRequest
                                {
                                    Amount = (int?) transaction.CreditAmount,
                                    RecieverEmails = new List<string> { preferedAccount },
                                    SenderEmail = smdUser.PaypalCustomerId
                                };

                                // Stripe + Invoice Work 
                                PaypalService.MakeAdaptiveImplicitPayment(requestModel);

                                transaction.isProcessed = true;
                                // Transaction log entery 
                                if (requestModel.Amount != null)
                                {
                                    var transactionLog = new TransactionLog
                                                         {
                                                             Amount = (double) requestModel.Amount,
                                                             FromUser = requestModel.SenderEmail,
                                                             Type = 1, // credit 
                                                             IsCompleted = true,
                                                             LogDate = DateTime.Now,
                                                             ToUser = requestModel.RecieverEmails.FirstOrDefault(),
                                                             TxId = transaction.TxId
                                                         };
                                    dbContext.TransactionLogs.Add(transactionLog);
                                }

                                UpdateAccounts(transaction, user, smdUser);

                                dbContext.SaveChanges();
                                // Email To User 
                                EmailSerice.SendPayOutRoutineEmail(user.Id);
                                // Indicates we are happy
                                tran.Complete();
                            }
                            catch (Exception excep)
                            {
                                // Exception Loging
                                LogError(excep, new HttpActionExecutedContext().Request.Content.ToString());
                            }
                        }
                    }
                }
                dbContext.SaveChanges();
            }
        }
        
        #endregion
    }
}
