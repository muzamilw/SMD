using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using System;
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

        /// <summary>
        /// Updates Accounts
        /// </summary>
        private static void UpdateAccounts(Company company, double? amount, long adCampaignId, BaseDbContext dbContext)
        {
            // Debit Advertiser
            UpdateUsersStripeAccount(company, company, amount, adCampaignId, dbContext, false);

            // Credit Cash4Ads
            UpdateCash4AdsStripeAccount(company, amount, adCampaignId, dbContext);
        }

        /// <summary>
        /// Updates Cash4Ads Stripe Account and Adds Transaction for it
        /// </summary>
        private static void UpdateCash4AdsStripeAccount(Company transactionCompany, double? amount, long adCampaignId, BaseDbContext dbContext)
        {
            // Update Cash4Ads User Stripe Account
            var smdUser = dbContext.Users.FirstOrDefault(obj => obj.Email == SystemUsers.Cash4Ads);
            if (smdUser == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            UpdateUsersStripeAccount(smdUser.Company, transactionCompany, amount, adCampaignId, dbContext);
        }

        /// <summary>
        /// Updates Users Stripe Account and 
        /// </summary>
        private static void UpdateUsersStripeAccount(Company company, Company transactionCompany, double? amount, long adCampaignId,
            BaseDbContext dbContext, bool isCredit = true)
        {
            Account usersStripeAccount = company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Stripe);
            if (usersStripeAccount != null)
            {
                if (!usersStripeAccount.AccountBalance.HasValue)
                {
                    usersStripeAccount.AccountBalance = 0;
                }
                
                var batchTransaction = new Transaction
                                       {
                                           AccountId = usersStripeAccount.AccountId,
                                           Type = 1,
                                           AdCampaignId = adCampaignId,
                                           isProcessed = true,
                                           TransactionDate = DateTime.Now,
                                           TransactionLogs = new List<TransactionLog>
                                                         {
                                                             new TransactionLog
                                                             {
                                                                 IsCompleted = true, 
                                                                 Amount = amount ?? 0, 
                                                                 FromUser = transactionCompany.CompanyName,
                                                                 LogDate = DateTime.Now,
                                                                 ToUser = company.CompanyName,
                                                                 Type = isCredit ? 1 : 2
                                                             }
                                                         }
                                       };

                if (isCredit)
                {
                    batchTransaction.CreditAmount = amount;
                    usersStripeAccount.AccountBalance += amount;
                }
                else
                {
                    batchTransaction.DebitAmount = amount;
                    usersStripeAccount.AccountBalance -= amount;
                }

                usersStripeAccount.Transactions.Add(batchTransaction);
                dbContext.Transactions.Add(batchTransaction);
            }
        }

        /// <summary>
        /// Genereate Invoice
        /// </summary>
        private static void GenerateInvoice(Company company, double? amount, BaseDbContext dbContext, List<InvoiceDetail> invoiceDetails)
        {
            string userCountry = company.Country != null ? company.Country.CountryName : string.Empty;

            // Add invoice data
            var invoice = new Invoice
            {
                Country = userCountry,
                Total = amount ?? 0,
                NetTotal = amount ?? 0,
                InvoiceDate = DateTime.Now,
                InvoiceDueDate = DateTime.Now.AddDays(7),
                Address1 = company.AddressLine1,
                CompanyId = company.CompanyId,
                CompanyName = "Cash4Ads",
                InvoiceDetails = new List<InvoiceDetail>()
            };

            dbContext.Invoices.Add(invoice);
            invoiceDetails.ForEach(inv =>
            {
                dbContext.InvoiceDetails.Add(inv);
                invoice.InvoiceDetails.Add(inv);
            });
        }

        /// <summary>
        /// Create Transaction Logs with Invoice Details
        /// </summary>
        private static void CreateTransactionLogWithInvoice(Transaction transaction, Company company, BaseDbContext dbContext,
            List<InvoiceDetail> invoiceDetails)
        {
            transaction.isProcessed = true;

            #region Transaction Log

            // Transaction log entery 
            var transactionLog = new TransactionLog
            {
                Amount = transaction.DebitAmount ?? 0,
                FromUser = company.ReplyEmail,
                Type = 2, // debit 
                IsCompleted = true,
                LogDate = DateTime.Now,
                ToUser = "Cash4Ads",
                TxId = transaction.TxId
            };

            dbContext.TransactionLogs.Add(transactionLog);
            transaction.TransactionLogs.Add(transactionLog);

            #endregion

            // Add Invoice Detail 
            var invoiceDetail = new InvoiceDetail
            {
                ItemName = "AdCampaign",
                ItemAmount = transaction.DebitAmount ?? 0,
                ItemTax = transaction.TaxValue ?? 0,
                ItemDescription = "Ad Click",
                ItemGrossAmount = transaction.DebitAmount ?? 0,
                CampaignId = transaction.AdCampaignId
            };
            invoiceDetails.Add(invoiceDetail);
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
                                              Type = 2, // Debit
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
            // Registration of Debit Process Scheduler Run after every 7 days 
            registry.Schedule(PerformDebit).ToRunEvery(7).Days().At(23, 59);
        }

        /// <summary>
        /// Perform Debit work after scheduled time | 7 Days
        /// </summary>
        public static void PerformDebit()
        {
            // Initialize Service
            StripeService = UnityConfig.UnityContainer.Resolve<IStripeService>();

            // Using Base DB Context
            using (var dbContext = new BaseDbContext())
            {
                // Get UnProcessed Debit Trasactions
                var unProcessedTrasactions = dbContext.Transactions.Where(trans => trans.DebitAmount != null && trans.CreditAmount == null
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

                    // If It is not System User then make transation 
                    Boolean isSystemUser = true;
                    double? debitAmount = 0;
                    foreach (Account account in accounts)
                    {
                        try
                        {
                            // Batch of transaction for this account and adcampaign
                            List<Transaction> transactions =
                            unProcessedTrasactions
                            .Where(trn => trn.AccountId == account.AccountId && trn.AdCampaignId == adCampaign).ToList();

                            debitAmount = transactions.Sum(trn => trn.DebitAmount);
                            // Stripe charges in cents i.e. $1 = 100 cents minimum is $0.50
                            double? chargeAmount = debitAmount.HasValue ? debitAmount * 100 : 0; 

                            // Get User from which credit to debit 
                            var company = dbContext.Companies.Find(account.CompanyId);
                            if (company == null)
                            {
                                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                                    LanguageResources.CollectionService_UserNotFound,
                                    account.CompanyId));
                            }

                            string response = null;
                            if (company.CompanyType == (int)CompanyType.User)
                            {
                                if (string.IsNullOrEmpty(company.StripeCustomerId))
                                {
                                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                                        LanguageResources.CollectionService_AccountNotRegistered,
                                        account.CompanyId, "Stripe"));
                                }

                                response = StripeService.ChargeCustomer((int?)chargeAmount, company.StripeCustomerId);
                                isSystemUser = false;
                            }
                            // Stripe + Invoice Work 
                            if (isSystemUser || (response != "failed"))
                            {
                                // Success
                                var invoiceDetails = new List<InvoiceDetail>();
                                transactions.ForEach(transaction => CreateTransactionLogWithInvoice(transaction, company, dbContext, invoiceDetails));

                                #region Update Accounts

                                // Update User's Virtual and stripe account
                                UpdateAccounts(company, debitAmount, adCampaign.Value, dbContext);

                                #endregion

                                // Generate Invoice
                                #region Add Invoice

                                GenerateInvoice(company, debitAmount, dbContext, invoiceDetails);

                                #endregion
                                
                                if (!isSystemUser)
                                {
                                    // Email To User 
                                    BackgroundEmailManagerService.SendCollectionRoutineEmail(dbContext, company.CompanyId);
                                }
                            }
                        }
                        catch (Exception exp)
                        {
                            LogError(exp, account.Company.CompanyName,
                                "Cash4Ads", account.Transactions.FirstOrDefault().TxId, debitAmount ?? 0, dbContext);
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
        }

        #endregion
    }
}
