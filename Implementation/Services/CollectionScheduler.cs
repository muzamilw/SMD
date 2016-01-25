using System.Globalization;
using System.Linq;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using System.Web.Http.Filters;
using Transaction = SMD.Models.DomainModels.Transaction;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Debit Scheduler BackGround Service 
    /// </summary>
    public class CollectionScheduler
    {
        #region Private
        // Properties 

        [Dependency]
        private static IStripeService StripeService { get; set; }


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
        /// Updates Accounts
        /// </summary>
        private static void UpdateAccounts(User user, Transaction transaction, BaseDbContext dbContext)
        {
            Account usersStripeAccount = user.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Stripe);
            if (usersStripeAccount != null)
            {
                if (!usersStripeAccount.AccountBalance.HasValue)
                {
                    usersStripeAccount.AccountBalance = 0;
                }
                usersStripeAccount.AccountBalance -= (decimal?)transaction.DebitAmount;
                transaction.Account.AccountBalance += (decimal?)transaction.DebitAmount;
            }
            // Update Cash4Ads User Stripe Account
            var smdUser = dbContext.Users.FirstOrDefault(obj => obj.Email == SystemUsers.Cash4Ads);
            if (smdUser == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            Account smdUserStripeAccount = smdUser.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Stripe);
            if (smdUserStripeAccount != null)
            {
                if (!smdUserStripeAccount.AccountBalance.HasValue)
                {
                    smdUserStripeAccount.AccountBalance = 0;
                }
                smdUserStripeAccount.AccountBalance += (decimal?)transaction.DebitAmount;
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
            // Registration of Debit Process Scheduler Run after every 7 days 
            registry.Schedule(PerformDebit).ToRunEvery(7).Days().At(23, 55);
        }

        /// <summary>
        /// Perform Debit work after scheduled time | 7 Days
        /// </summary>
        public static void PerformDebit()
        {
            // Initialize Service
            StripeService = UnityConfig.UnityContainer.Resolve<IStripeService>();
            EmailSerice = UnityConfig.UnityContainer.Resolve<IEmailManagerService>();

            // Using Base DB Context
            using (var dbContext = new BaseDbContext())
            {
                // Get UnProcessed Debit Trasactions
                var unProcessedTrasactions = dbContext.Transactions.Where(trans => trans.DebitAmount != null && trans.CreditAmount == null
                && (trans.isProcessed == null || trans.isProcessed == false)).ToList();

                foreach (var transaction in unProcessedTrasactions)
                {
                    if (transaction.AccountId != null)
                    {
                        // Get User from which credit to debit 
                        var user = dbContext.Users.Find(transaction.Account.UserId);
                        if (user == null)
                        {
                            try
                            {
                                throw new Exception(string.Format(CultureInfo.InvariantCulture, LanguageResources.CollectionService_UserNotFound,
                                    transaction.Account.UserId));
                            }
                            catch (Exception exception)
                            {
                                // Exception Loging
                                LogError(exception, new HttpActionExecutedContext().Request.Content.ToString());   
                            }
                        }
                        // Secure Transation
                        using (var tran = new TransactionScope())
                        {
                            string response = null;
                            try
                            {
                                // If It is not System User then make transation 
                                Boolean isSystemUser;
                                if (user.Roles.Any(role => role.Name.ToLower().Equals("user")))
                                {
                                    if (string.IsNullOrEmpty(user.StripeCustomerId))
                                    {
                                        throw new Exception(string.Format(CultureInfo.InvariantCulture,
                                            LanguageResources.CollectionService_AccountNotRegistered,
                                            transaction.Account.UserId, "Stripe"));
                                    }

                                    response = StripeService.ChargeCustomer((int?)transaction.DebitAmount, user.StripeCustomerId);
                                    isSystemUser = false;
                                }
                                else
                                {
                                    isSystemUser = true;
                                    transaction.TxId = 0; // No Transaction Made
                                }
                                // Stripe + Invoice Work 
                                if (isSystemUser || (response != "failed"))
                                {
                                    // Success
                                    transaction.isProcessed = true;

                                    #region Transaction Log
                                    // Transaction log entery 
                                    var transactionLog = new TransactionLog
                                    {
                                        Amount = (double)transaction.DebitAmount,
                                        FromUser = user.Email,
                                        Type = 2, // debit 
                                        IsCompleted = true,
                                        LogDate = DateTime.Now,
                                        ToUser = "Cash4Ads",
                                        TxId = transaction.TxId
                                    };
                                    dbContext.TransactionLogs.Add(transactionLog);
                                    #endregion
                                    #region Add Invoice

                                    // Add invoice data
                                    var invoice = new Invoice
                                    {
                                        Country = user.CountryId.ToString(),
                                        Total = (double)transaction.DebitAmount,
                                        NetTotal = (double)transaction.DebitAmount,
                                        InvoiceDate = DateTime.Now,
                                        InvoiceDueDate = DateTime.Now.AddDays(7),
                                        Address1 = user.CountryId.ToString(),
                                        UserId = user.Id,
                                        CompanyName = "Cash4Ads",
                                        CreditCardRef = response
                                    };
                                    dbContext.Invoices.Add(invoice);

                                    #endregion
                                    #region Add Invoice Detail

                                    // Add Invoice Detail Data 
                                    var invoiceDetail = new InvoiceDetail
                                    {
                                        InvoiceId = invoice.InvoiceId,
                                        SqId = null,
                                        ProductId = null,
                                        ItemName = "Item from Scheduler",
                                        ItemAmount = (double)transaction.DebitAmount,
                                        ItemTax = (double)transaction.TaxValue,
                                        ItemDescription = "This is description!",
                                        ItemGrossAmount = (double)transaction.DebitAmount,
                                        CampaignId = transaction.AdCampaignId,

                                    };
                                    dbContext.InvoiceDetails.Add(invoiceDetail);

                                    #endregion

                                    #region Update Accounts

                                    // Update User's Virtual and stripe account
                                    UpdateAccounts(user, transaction, dbContext);

                                    #endregion

                                    if (!isSystemUser)
                                    {
                                        // Email To User 
                                        EmailSerice.SendCollectionRoutineEmail(user.Id);
                                    }

                                    dbContext.SaveChanges();
                                }
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
