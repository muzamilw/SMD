using System.Globalization;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using Transaction = SMD.Models.DomainModels.Transaction;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Credit Scheduler 
    /// </summary>
    public class PayOutScheduler
    {
        #region Private

        [Dependency]
        private static IPaypalService PaypalService { get; set; }


        /// <summary>
        /// Updates User's and Cash4Ads Accounts
        /// </summary>
        private static void UpdateAccounts(User user, User smdUser, double amount, 
            long? adCampaignId, BaseDbContext dbContext)
        {
            // Update users virtual account and add to paypal account
            UpdateUsersPaypalAccount(user, amount, adCampaignId, dbContext);

            // Update Cash4ads accounts
            UpdateUsersPaypalAccount(smdUser, amount, adCampaignId, dbContext, false);
        }

        /// <summary>
        /// Updates Users Paypal Account
        /// </summary>
        private static void UpdateUsersPaypalAccount(User user, double amount, long? adCampaignId,
            BaseDbContext dbContext, bool isCredit = true)
        {
            Account usersPaypalAccount = user.Accounts.FirstOrDefault(acc => acc.AccountType == (int) AccountType.Paypal);
            if (usersPaypalAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    user.Id, "Paypal"));
            }

            if (!usersPaypalAccount.AccountBalance.HasValue)
            {
                usersPaypalAccount.AccountBalance = 0;
            }

            var batchTransaction = new Transaction
                                   {
                                       AccountId = usersPaypalAccount.AccountId,
                                       Type = 1,
                                       AdCampaignId = adCampaignId,
                                       isProcessed = true,
                                       TransactionDate = DateTime.Now,
                                       TransactionLogs = new List<TransactionLog>
                                                         {
                                                             new TransactionLog
                                                             {
                                                                 IsCompleted = true, 
                                                                 Amount = amount, 
                                                                 FromUser = "Cash4Ads",
                                                                 LogDate = DateTime.Now,
                                                                 ToUser = user.FullName,
                                                                 Type = isCredit ? 1 : 2
                                                             }
                                                         }
                                   };

            if (isCredit)
            {
                batchTransaction.CreditAmount = amount;
                usersPaypalAccount.AccountBalance += amount;
            }
            else
            {
                batchTransaction.DebitAmount = amount;
                usersPaypalAccount.AccountBalance -= amount;
            }

            usersPaypalAccount.Transactions.Add(batchTransaction);
            dbContext.Transactions.Add(batchTransaction);
        }

        /// <summary>
        /// Looks for user cash4ads
        /// </summary>
        private static User GetCash4AdsUser(BaseDbContext dbContext)
        {
            var smdUser = dbContext.Users.FirstOrDefault(obj => obj.Email == SystemUsers.Cash4Ads);
            if (smdUser == null)
            {

                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_UserNotFound,
                    "Cash4Ads"));

            }
            return smdUser;
        }

        /// <summary>
        /// Checks if accounts are registered
        /// </summary>
        private static void CheckAccounts(User smdUser, string preferedAccount, Account account)
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
                    account.UserId, "Paypal"));
            }
        }

        /// <summary>
        /// Updates Transactions
        /// </summary>
        private static void UpdateTransactions(Transaction tran, double? creditAmount, MakePaypalPaymentRequest requestModel,
            BaseDbContext dbContext)
        {
            tran.isProcessed = true;

            var transactionLog = new TransactionLog
            {
                Amount = creditAmount ?? 0,
                FromUser = requestModel.SenderEmail,
                Type = 1, // credit 
                IsCompleted = true,
                LogDate = DateTime.Now,
                ToUser = requestModel.RecieverEmails.FirstOrDefault(),
                TxId = tran.TxId
            };

            dbContext.TransactionLogs.Add(transactionLog);
            tran.TransactionLogs.Add(transactionLog);
        }

        /// <summary>
        /// Logs Error
        /// </summary>
        private static void LogError(Exception exp, string transactionFrom, string transactionTo,
            long transactionId, double amount, BaseDbContext dbContext)
        {
            dbContext.TransactionLogs.Add(new TransactionLog
            {
                TxId = transactionId,
                Description = exp.Message,
                LogDate = DateTime.Now,
                FromUser = transactionFrom,
                ToUser = transactionTo,
                Type = 1, // Credit
                Amount = amount
            });
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

            // Using Base DB Context
            using (var dbContext = new BaseDbContext())
            {
                // Get UnProcessed Credit Trasactions
                var unProcessedTrasactions = dbContext.Transactions.
                    Where(trans => trans.DebitAmount == null && trans.CreditAmount != null
                && (trans.isProcessed == null || trans.isProcessed == false)).ToList();

                // Get Distinct AdCampaign to process transactions
                // in a batch for each adcampaign
                List<long?> adCampaigns =
                        unProcessedTrasactions
                        .Select(trn => trn.AdCampaignId).Distinct().ToList();

                // Process transactions for each account
                foreach (long? adCampaign in adCampaigns)
                {
                    // Get Distinct Accounts so that transactions 
                    // for an account should be processed in a batch
                    List<Account> accounts =
                        unProcessedTrasactions
                            .Where(trn => trn.AdCampaignId == adCampaign)
                            .Select(trn => trn.Account).Distinct().ToList();

                    double? creditAmount = 0;
                    User user = null;
                    foreach (Account account in accounts)
                    {
                        try
                        {
                            // Batch of transaction for this account and adcampaign
                            List<Transaction> transactions =
                            unProcessedTrasactions
                            .Where(trn => trn.AccountId == account.AccountId && trn.AdCampaignId == adCampaign).ToList();

                            // Get User from which credit to debit 
                            user = dbContext.Users.Find(account.UserId);

                            // User's Prefered Account
                            var preferedAccount = user.PreferredPayoutAccount == 1
                                ? user.PaypalCustomerId
                                : user.GoogleWalletCustomerId;

                            var smdUser = GetCash4AdsUser(dbContext);

                            CheckAccounts(smdUser, preferedAccount, account);

                            creditAmount = transactions.Sum(trn => trn.CreditAmount);

                            // PayPal Request Model 
                            var requestModel = new MakePaypalPaymentRequest
                            {
                                Amount = (decimal)creditAmount,
                                RecieverEmails = new List<string> { preferedAccount },
                                SenderEmail = smdUser.PaypalCustomerId
                            };

                            // Stripe + Invoice Work 
                            PaypalService.MakeAdaptiveImplicitPayment(requestModel);

                            // Update Transactions
                            transactions.ForEach(tran => UpdateTransactions(tran, creditAmount, requestModel, dbContext));

                            // Update Accounts
                            UpdateAccounts(user, smdUser, creditAmount.Value, adCampaign, dbContext);

                            // Save Changes
                            dbContext.SaveChanges();

                            // Email To User 
                            BackgroundEmailManagerService.SendPayOutRoutineEmail(dbContext, user.Id);
                        }
                        catch (Exception exp)
                        {

                            var transaction = account.Transactions.FirstOrDefault();
                            LogError(exp, user != null ? user.FullName : string.Empty,
                                "Cash4Ads", transaction != null ? transaction.TxId : 0, creditAmount.Value, dbContext);
                        }
                    }
                }
            }
        }
        
        #endregion
    }
}
