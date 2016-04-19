using System.Configuration;
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
        private static void UpdateAccounts(Company company, Company smdCompany, double amount, 
            long? adCampaignId, BaseDbContext dbContext)
        {
            // Update users  add to paypal account
            UpdateUsersPaypalAccount(company, amount, adCampaignId, dbContext);

            // Update Cash4ads accounts
            UpdateUsersPaypalAccount(smdCompany, amount, adCampaignId, dbContext, false);

        } //UpdateCouponAccounts
          private static void UpdateAccounts(Company company, Company smdCompany, double amount,
           BaseDbContext dbContext)
        {
            // Update users virtual account and add to paypal account
            UpdateUsersPaypalAccountNew(company, amount,  dbContext);

            // Update Cash4ads accounts
            UpdateUsersPaypalAccountNew(smdCompany, amount, dbContext, false);

            // update users  virutal accont debit 
            updateUsersVirtualAccount(company, amount, dbContext,1, false);
            // update smd users  virutal accont debit 
            updateUsersVirtualAccount(smdCompany, amount, dbContext,1);

        }
          private static string UpdateCouponAccounts(Company company, Company smdCompany, long couponId,
           BaseDbContext dbContext)
          {
              // if coupon 
              // get money from user virtual account 
              // add it to smd and users virtual account 
              // send user voucher email 
              var coupon = dbContext.AdCampaigns.Where(g => g.CampaignId == couponId).SingleOrDefault();
              if (coupon == null)
                  return "";
              double totalAmount = Convert.ToDouble( coupon.CouponSwapValue);
              double smdValue = totalAmount - (Convert.ToDouble(coupon.couponSmdComission));
              double VoucherSellerValue = totalAmount - smdValue;

              var couponCode = dbContext.CouponCode.Where(g => g.CampaignId == couponId && g.IsTaken != true).SingleOrDefault();
              couponCode.IsTaken = true;
              
              
          

              // update users  virutal accont debit 
              updateUsersVirtualAccount(company, totalAmount, dbContext,2, false);
              // update smd users  virutal accont credit 
              updateUsersVirtualAccount(smdCompany, smdValue, dbContext, 2);
              // update smd users  virutal accont credit 
              updateUsersVirtualAccount(coupon.Company, VoucherSellerValue, dbContext, 2);
              return couponCode.Code;
          }
        // used by new payout scheduler 
        private static void UpdateAccountsNew(Company company, Company smdCompany, double amount,
           BaseDbContext dbContext)
        {
            // Update users virtual account and add to paypal account
            UpdateUsersPaypalAccountNew(company, amount,  dbContext);

            // Update Cash4ads accounts
            UpdateUsersPaypalAccountNew(smdCompany, amount, dbContext, false);

            // update users  virutal accont debit 
            updateUsersVirtualAccount(company, amount, dbContext,1, false);
            // update smd users  virutal accont debit 
            updateUsersVirtualAccount(smdCompany, amount, dbContext, 1);

        }
        private static void UpdateUsersPaypalAccountNew(Company company, double amount,  BaseDbContext dbContext, bool isCredit = true)
        {
            Account usersPaypalAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
            Account userVirtualAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
            if (usersPaypalAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    company.CompanyId, "Paypal"));
            }

            if (!usersPaypalAccount.AccountBalance.HasValue)
            {
                usersPaypalAccount.AccountBalance = 0;
            }

            var batchTransaction = new Transaction
            {
                AccountId = usersPaypalAccount.AccountId,
                Type = 1,
               // AdCampaignId = adCampaignId,
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
                                                                 ToUser = company.CompanyName,
                                                                 Type = isCredit ? 1 : 2
                                                             }
                                                         }
            };

            if (isCredit)
            {
                batchTransaction.CreditAmount = amount;
                usersPaypalAccount.AccountBalance += amount;
                userVirtualAccount.AccountBalance -= amount;

            }
            else
            {
                batchTransaction.DebitAmount = amount;
                usersPaypalAccount.AccountBalance -= amount;
            }

            usersPaypalAccount.Transactions.Add(batchTransaction);
            dbContext.Transactions.Add(batchTransaction);
        }
        private static void updateUsersVirtualAccount(Company company, double amount, BaseDbContext dbContext,int type, bool isCredit = true)
        {
            Account userVirtualAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
            if (userVirtualAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    company.CompanyId, "Virtual"));
            }
            var batchTransaction = new Transaction
            {
                AccountId = userVirtualAccount.AccountId,
                Type = 1,
                // AdCampaignId = adCampaignId,
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
                                                                 ToUser = company.CompanyName,
                                                                 Type = isCredit ? 1 : 2
                                                             }
                                                         }
            };
            if (isCredit)
            {
                batchTransaction.CreditAmount = amount;
                //usersPaypalAccount.AccountBalance += amount;
                //userVirtualAccount.AccountBalance -= amount;
                if (type == 2)
                    userVirtualAccount.AccountBalance += amount;
            }
            else
            {
                batchTransaction.DebitAmount = amount;
                //usersPaypalAccount.AccountBalance -= amount;
                if (type == 2)
                    userVirtualAccount.AccountBalance -= amount;
            }
            userVirtualAccount.Transactions.Add(batchTransaction);
            dbContext.Transactions.Add(batchTransaction);
        }
        /// <summary>
        /// Updates Users Paypal Account
        /// </summary>
        private static void UpdateUsersPaypalAccount(Company company, double amount, long? adCampaignId,
            BaseDbContext dbContext, bool isCredit = true)
        {
            Account usersPaypalAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
            Account userVirtualAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
            if (usersPaypalAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    company.CompanyId, "Paypal"));
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
                                                                 ToUser = company.CompanyName,
                                                                 Type = isCredit ? 1 : 2
                                                             }
                                                         }
                                   };

            if (isCredit)
            {
                batchTransaction.CreditAmount = amount;
                usersPaypalAccount.AccountBalance += amount;
                userVirtualAccount.AccountBalance -= amount;

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

            if (string.IsNullOrEmpty(smdUser.Company.PaypalCustomerId))
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    smdUser.Company.CompanyId, "Paypal"));
            }

            if (string.IsNullOrEmpty(preferedAccount))
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    account.CompanyId, "Paypal"));
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
            registry.Schedule(PerformPayout).ToRunEvery(1).Days();
        }

        /// <summary>
        /// Perform Credit work after scheduled time | 7 Days
        /// </summary>
        public static void PerformPayout()
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
                    Company company = null;
                    foreach (Account account in accounts)
                    {
                        try
                        {
                            // Batch of transaction for this account and adcampaign
                            List<Transaction> transactions =
                            unProcessedTrasactions
                            .Where(trn => trn.AccountId == account.AccountId && trn.isProcessed != true).ToList();
                            

                            // Get User from which credit to debit 
                            company = dbContext.Companies.Find(account.CompanyId);
                            // skip if no transaction found
                            if (transactions.Count == 0)
                                continue;
                            // User's Prefered Account
                            var preferedAccount = company.PreferredPayoutAccount == 1
                                ? company.PaypalCustomerId
                                : company.GoogleWalletCustomerId;

                            var smdUser = GetCash4AdsUser(dbContext);

                            CheckAccounts(smdUser, preferedAccount, account);

                            creditAmount = transactions.Sum(trn => trn.CreditAmount);
                            // Check if Payout amount has reached a limit e.g. $20 then process it.
                            var payoutLimit = ConfigurationManager.AppSettings["PayoutLimit"];
                            if (payoutLimit != null && creditAmount < Convert.ToDouble(payoutLimit))
                            {
                                continue;
                            }

                            // PayPal Request Model 
                            var requestModel = new MakePaypalPaymentRequest
                            {
                                Amount = (decimal)creditAmount,
                                RecieverEmails = new List<string> { preferedAccount },
                                SenderEmail = smdUser.Company.PaypalCustomerId
                            };

                            // Stripe + Invoice Work 
                            PaypalService.MakeAdaptiveImplicitPayment(requestModel);

                            // Update Transactions
                            transactions.ForEach(tran => UpdateTransactions(tran, creditAmount, requestModel, dbContext));

                            // Update Accounts
                            UpdateAccounts(company, smdUser.Company, creditAmount.Value, adCampaign, dbContext);

                            // Save Changes
                            dbContext.SaveChanges();

                            // Email To User 
                            BackgroundEmailManagerService.SendPayOutRoutineEmail(dbContext, company.CompanyId);
                        }
                        catch (Exception exp)
                        {

                            var transaction = account.Transactions.FirstOrDefault();
                            LogError(exp, company != null ? company.CompanyName : string.Empty,
                                "Cash4Ads", transaction != null ? transaction.TxId : 0, creditAmount.Value, dbContext);
                        }
                    }
                }
            }
        }
        public static bool PerformUserPayout(int companyId, int CouponId, int mode,double amount)
        {
            PaypalService = UnityConfig.UnityContainer.Resolve<IPaypalService>();
            using (var dbContext = new BaseDbContext())
            {
                Company company = null;
                // Get User from which credit to debit 
                company = dbContext.Companies.Find(companyId);
                if (company == null)
                    return false;
                var smdUser = GetCash4AdsUser(dbContext);
                if (smdUser == null || smdUser.Company == null)
                    return false;
                // User's Prefered Account
                var preferedAccount = company.PreferredPayoutAccount == 1
                    ? company.PaypalCustomerId
                    : company.GoogleWalletCustomerId;

                List<Account> accounts = dbContext.Accounts.Where(g => g.CompanyId == companyId).ToList();
                if (mode == (int)PaymentMethod.Coupon)
                {
                    var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
                    if (amount < userVirtualAccount.AccountBalance)
                    {
                       
                        // Update Accounts
                        string codeCode = UpdateCouponAccounts(company, smdUser.Company,CouponId , dbContext);

                        // Save Changes
                        dbContext.SaveChanges();

                        // Email To User coupon code 
                        try
                        {
                            BackgroundEmailManagerService.SendVoucherCodeEmail(dbContext, company.CompanyId, codeCode);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                    else
                    {
                        return false;// insufficent balance 
                    }
                }
                else
                {
                    var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
                    if (amount < userVirtualAccount.AccountBalance)
                    {
                        // PayPal Request Model 
                        var requestModel = new MakePaypalPaymentRequest
                        {
                            Amount = (decimal)amount,
                            RecieverEmails = new List<string> { preferedAccount },
                            SenderEmail = smdUser.Company.PaypalCustomerId
                        };

                        // Stripe + Invoice Work 
                        PaypalService.MakeAdaptiveImplicitPayment(requestModel);
                        // Update Accounts
                        UpdateAccountsNew(company, smdUser.Company, amount, dbContext);

                        // Save Changes
                        dbContext.SaveChanges();

                        // Email To User 
                        BackgroundEmailManagerService.SendPayOutRoutineEmail(dbContext, company.CompanyId);
                    } else
                    {
                        return false;// insufficent balance 
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
