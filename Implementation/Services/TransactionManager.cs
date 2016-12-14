﻿using System.Configuration;
using System.Globalization;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Interfaces.Repository;
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
    public class TransactionManager
    {
        #region Private

        [Dependency]
        private static IPaypalService PaypalService { get; set; }


         [Dependency]
        private static IPayOutHistoryRepository payoutRepository { get; set; }


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

        } 
        //  private static void UpdateAccounts(Company company, Company smdCompany, double amount,
        //   BaseDbContext dbContext)
        //{
        //    // Update users virtual account and add to paypal account
        //    UpdateUsersPaypalAccountNew(company, amount,  dbContext);

        //    // Update Cash4ads accounts
        //    UpdateUsersPaypalAccountNew(smdCompany, amount, dbContext, false);

        //    // update users  virutal accont debit 
        //    updateUsersVirtualAccount(company, amount, dbContext,1, false);
        //    // update smd users  virutal accont debit 
        //    updateUsersVirtualAccount(smdCompany, amount, dbContext,1);

        //}


          public static bool CouponPurchaseTransaction(long couponId, double SwapCost, int CompanyId)
          {

              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  // if coupon 
                  // get money from user virtual account 
                  // add it to smd and users virtual account 
                  // send user voucher email 

                  // deduct user centz balance.
                  updateVirtualAccount(userCompany, SwapCost, dbContext, TransactionType.CouponPurchased, false, couponId,null );
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, SwapCost, dbContext, TransactionType.CouponPurchased, true, couponId,null );
                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }
            
          }


          public static bool CouponApprovalTransaction(long couponId, double Payment, int CompanyId)
          {

              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                 //get money from stripe

                  // update company  stripe accont debit 
                  updateStripeAccount(userCompany, Payment, dbContext, TransactionType.ApproveCoupon, false, couponId, null);
                  // update smd users  stripe accont credit 
                  updateStripeAccount(smdCompany, Payment, dbContext, TransactionType.ApproveCoupon, true, couponId, null);



                  // 1 UsD = 100 Centz into virtual
                  // update users  virutal accont debit 
                  updateVirtualAccount(userCompany, Payment * 100, dbContext, TransactionType.ApproveCoupon, true, couponId, null);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, Payment * 100, dbContext, TransactionType.ApproveCoupon, false, couponId, null);


                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }

          }


          public static bool CouponSubscriptionPaymentTransaction(long? couponId, double Payment, int CompanyId)
          {

              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  //get money from stripe

                  // update company  stripe accont debit 
                  updateStripeAccount(userCompany, Payment, dbContext, TransactionType.ApproveCoupon, false, couponId, null);
                  // update smd users  stripe accont credit 
                  updateStripeAccount(smdCompany, Payment, dbContext, TransactionType.ApproveCoupon, true, couponId, null);



                  // 1 UsD = 100 Centz into virtual
                  // update users  virutal accont debit 
                  updateVirtualAccount(userCompany, Payment * 100, dbContext, TransactionType.ApproveCoupon, true, couponId, null);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, Payment * 100, dbContext, TransactionType.ApproveCoupon, false, couponId, null);


                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }

          }




          public static bool SurveyApproveTransaction(long SQID, double Payment, int CompanyId)
          {
              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  //get money from stripe

                  // update company  stripe accont debit 
                  updateStripeAccount(userCompany, Payment, dbContext, TransactionType.ApproveSurvey, false, null, null,SQID);
                  // update smd users  stripe accont credit 
                  updateStripeAccount(smdCompany, Payment, dbContext, TransactionType.ApproveSurvey, true, null, null, SQID);



                  // 1 UsD = 100 Centz into virtual
                  // update users  virutal accont debit 
                  updateVirtualAccount(userCompany, Payment * 100, dbContext, TransactionType.ApproveSurvey, true, null, null, SQID);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, Payment * 100, dbContext, TransactionType.ApproveSurvey, false, null, null, SQID);


                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }
          }


          public static bool ProfileQuestionApproveTransaction(int PQID, double Payment, int CompanyId)
          {
              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  //get money from stripe

                  // update company  stripe accont debit 
                  updateStripeAccount(userCompany, Payment, dbContext, TransactionType.ApproveProfileQuestion, false, null, null,null, PQID);
                  // update smd users  stripe accont credit 
                  updateStripeAccount(smdCompany, Payment, dbContext, TransactionType.ApproveProfileQuestion, true, null, null, null, PQID);



                  // 1 UsD = 100 Centz into virtual
                  // update users  virutal accont debit 
                  updateVirtualAccount(userCompany, Payment * 100, dbContext, TransactionType.ApproveProfileQuestion, true, null, null, null, PQID);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, Payment * 100, dbContext, TransactionType.ApproveProfileQuestion, false, null, null, null, PQID);


                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }
          }

          public static bool AddCompaignApprovalTransaction(long AdId, double Payment, int CompanyId)
          {

              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  //get money from stripe

                  // update company  stripe accont debit 
                  updateStripeAccount(userCompany, Payment, dbContext, TransactionType.ApproveAd, false,null,AdId, null);
                  // update smd users  stripe accont credit 
                  updateStripeAccount(smdCompany, Payment, dbContext, TransactionType.ApproveAd, true, null, AdId, null);


                  // 1 UsD = 100 Centz into virtual
                  // update users  virutal accont debit 
                  updateVirtualAccount(userCompany, Payment * 100, dbContext, TransactionType.ApproveAd, true, null, AdId, null);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, Payment * 100, dbContext, TransactionType.ApproveAd, false, null, AdId, null);


                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }

          }
          public static bool UserSignupFreeGiftBalanceTransaction(double GiftAmount, int CompanyId)
          {

              using (var dbContext = new BaseDbContext())
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  // if coupon 
                  // get money from user virtual account 
                  // add it to smd and users virtual account 
                  // send user voucher email 

                  // deduct user centz balance.
                  updateVirtualAccount(userCompany, GiftAmount, dbContext, TransactionType.WelcomeGiftBalance, true, null, null);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, GiftAmount, dbContext, TransactionType.WelcomeGiftBalance, false, null, null);
                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }

          }




          public static bool ArchiveUserResetVirtualAccountBalance(int CompanyId)
          {

              using (var dbContext = new BaseDbContext()) 
              {

                  var userCompany = dbContext.Companies.Where(g => g.CompanyId == CompanyId).SingleOrDefault();

                  var smdCompany = GetCash4AdsUser(dbContext).Company;


                  // if coupon 
                  // get money from user virtual account 
                  // add it to smd and users virtual account 
                  // send user voucher email 


                  Account userVirtualAccount = userCompany.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);

                  // deduct user centz balance.
                  updateVirtualAccount(userCompany, userVirtualAccount.AccountBalance.Value, dbContext, TransactionType.UnCollectedCentz, false, null, null);
                  // update smd users  virutal accont credit 
                  updateVirtualAccount(smdCompany, userVirtualAccount.AccountBalance.Value, dbContext, TransactionType.UnCollectedCentz, true, null, null);
                  // update smd users  virutal accont credit 
                  //updateUsersVirtualAccount(coupon.Company, SwapCost, dbContext, 2, true, null, couponId);

                  dbContext.SaveChanges();
                  return true;
              }

          }



        // used by new payout scheduler 
        private static void UpdateAccountsUserPayout(Company Usercompany, Company smdCompany, double amount,
           BaseDbContext dbContext)
        {
            // Update users virtual account and add to paypal account
            UpdateUsersPaypalAccountNew(Usercompany, amount, dbContext);

            // Update Cash4ads accounts
            UpdateUsersPaypalAccountNew(smdCompany, amount, dbContext, false);

            // update users  virutal accont debit 
            updateVirtualAccount(Usercompany, amount, dbContext,  TransactionType.UserCashOutPaypal, false);
            // update smd users  virutal accont debit 
            updateVirtualAccount(smdCompany, amount, dbContext, TransactionType.UserCashOutPaypal);

        }
        private static void UpdateUsersPaypalAccountNew(Company company, double amount,  BaseDbContext dbContext, bool isCredit = true)
        {
            Account usersPaypalAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
            //Account userVirtualAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
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
                

            }
            else
            {
                batchTransaction.DebitAmount = amount;
                usersPaypalAccount.AccountBalance -= amount;
            }

            usersPaypalAccount.Transactions.Add(batchTransaction);
            dbContext.Transactions.Add(batchTransaction);
        }
        private static void updateVirtualAccount(Company company, double amount, BaseDbContext dbContext, TransactionType type, bool isCredit = true, long? CouponId = null, long? CampaignId = null, long? SurveyId = null, int? ProfileQuestionID = null)
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
                Type = (int)type,
                // AdCampaignId = adCampaignId,
                isProcessed = true,
                TransactionDate = DateTime.Now,
                CouponId = CouponId,
                AdCampaignId = CampaignId,
                PQID = ProfileQuestionID,
                SQId = SurveyId,
                
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
                //if (type ==  TransactionType.AdClick || type == TransactionType.SurveyWatched || type == TransactionType.ProfileQuestionAnswered )
                    userVirtualAccount.AccountBalance += amount;

                batchTransaction.AccountBalance = userVirtualAccount.AccountBalance;
            }
            else
            {
                batchTransaction.DebitAmount = amount;
                //usersPaypalAccount.AccountBalance -= amount;
                //if (type == 2)
                    userVirtualAccount.AccountBalance -= amount;

                batchTransaction.AccountBalance = userVirtualAccount.AccountBalance;
            }



            userVirtualAccount.Transactions.Add(batchTransaction);
            dbContext.Transactions.Add(batchTransaction);
        }



        private static void updateStripeAccount(Company company, double amount, BaseDbContext dbContext, TransactionType type, bool isCredit = true, long? CouponId = null, long? CampaignId = null, long? SurveyId = null, int? ProfileQuestionID = null)
        {
            Account stripeAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Stripe);
            if (stripeAccount == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.CollectionService_AccountNotRegistered,
                    company.CompanyId, "Stripe"));
            }
            var batchTransaction = new Transaction
            {
                AccountId = stripeAccount.AccountId,
                Type = (int)type,
                // AdCampaignId = adCampaignId,
                isProcessed = true,
                TransactionDate = DateTime.Now,
                CouponId = CouponId,
                AdCampaignId = CampaignId,
                SQId = SurveyId,
                PQID = ProfileQuestionID,

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
                //if (type ==  TransactionType.AdClick || type == TransactionType.SurveyWatched || type == TransactionType.ProfileQuestionAnswered )
                stripeAccount.AccountBalance += amount;

                batchTransaction.AccountBalance = stripeAccount.AccountBalance;
            }
            else
            {
                batchTransaction.DebitAmount = amount;
                //usersPaypalAccount.AccountBalance -= amount;
                //if (type == 2)
                stripeAccount.AccountBalance -= amount;

                batchTransaction.AccountBalance = stripeAccount.AccountBalance;
            }



            stripeAccount.Transactions.Add(batchTransaction);
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
                            var user = dbContext.Users.Where(g => g.CompanyId == company.CompanyId).FirstOrDefault();

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
                            BackgroundEmailManagerService.EmailNotificationPayOutToUser(dbContext, user, creditAmount.Value);
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

        /// <summary>
        /// Deducting the amount from account only and not performing the paypal transaction. this will be enabled later on.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="CentzAmount"></param>
        /// <returns>1 for success, 2 for balance insufficient, 3 for amount less than minimum limit, 0 for error</returns>
        public static int PerformUserPayout(string UserId,int companyId,double CentzAmount, string PayPalId, string Phone )
        {
            payoutRepository = UnityConfig.UnityContainer.Resolve<IPayOutHistoryRepository>();

            var cashoutMinLimit = ConfigurationManager.AppSettings["CashoutMinLimit"];

            if (CentzAmount < (Convert.ToDouble(cashoutMinLimit)))
                return 3;

            double dollarAmount = CentzAmount * 0.08;


            PaypalService = UnityConfig.UnityContainer.Resolve<IPaypalService>();
            using (var dbContext = new BaseDbContext())
            {
                Company company = null;

                var user = dbContext.Users.Where(g => g.Id == UserId).SingleOrDefault();

                // Get User from which credit to debit 
                company = dbContext.Companies.Find(companyId);
                if (company == null)
                    return 0;
                var smdUser = GetCash4AdsUser(dbContext);
                if (smdUser == null || smdUser.Company == null)
                    return 0;

                //only send payment to paypal.
                // User's Prefered Account
                //var preferedAccount = company.PreferredPayoutAccount == 1
                //    ? company.PaypalCustomerId
                //    : company.GoogleWalletCustomerId;

                company.PaypalCustomerId = PayPalId;
                user.Phone1 = Phone;


                List<Account> accounts = dbContext.Accounts.Where(g => g.CompanyId == companyId).ToList();
                
            
                    var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
                    if (CentzAmount < userVirtualAccount.AccountBalance)
                    {
                        // PayPal Request Model 
                        var requestModel = new MakePaypalPaymentRequest
                        {
                            Amount = (decimal)CentzAmount,
                            RecieverEmails = new List<string> { PayPalId },
                            SenderEmail = smdUser.Company.PaypalCustomerId
                        };

                        // Stripe + Invoice Work 
                        //PaypalService.MakeAdaptiveImplicitPayment(requestModel);
                        // Update Accounts
                        UpdateAccountsUserPayout(company, smdUser.Company, CentzAmount, dbContext);



                        // Save Changes
                        dbContext.SaveChanges();

                        //creating payout history record
                        var payOutHistory = new PayOutHistory { CentzAmount = CentzAmount, CompanyId = companyId, DollarAmount = dollarAmount, RequestDateTime = DateTime.Now, TargetPayoutAccount = PayPalId };
                        payoutRepository.Add(payOutHistory);
                        payoutRepository.SaveChanges();

                        // Email To User 
                        BackgroundEmailManagerService.EmailNotificationPayOutToUser(dbContext, user, dollarAmount);
                        //email to smd admin
                        BackgroundEmailManagerService.EmailNotificationPayOutToAdmin(dbContext, user, company, dollarAmount);
                        return 1;
                    } else
                    {
                        return 2;// insufficent balance 
                    }
                
            }

        }


        public static List<vw_GetUserTransactions> GetUserTransactions()
        {
            List<vw_GetUserTransactions> result = new List<vw_GetUserTransactions>();
            using (var dbContext = new BaseDbContext())
            {
                result = dbContext.vw_GetUserTransactions.ToList();
            }
            return result;
        }
        public static List<vw_PublisherTransaction> GetPublisherTransactions()
        {
            List<vw_PublisherTransaction> result = new List<vw_PublisherTransaction>();
            using (var dbContext = new BaseDbContext())
            {
                result = dbContext.vw_PublisherTransaction.ToList();
            }
            return result;
        }
        public static List<vw_Cash4AdsReport> GetSmdReport()
        {
            List<vw_Cash4AdsReport> result = new List<vw_Cash4AdsReport>();
            using (var dbContext = new BaseDbContext())
            {
                result = dbContext.vw_Cash4AdsReport.ToList();
            }
            return result;
        }
        #endregion
    }
}
