using System.Linq;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using System.Web.Http.Filters;

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

        private static ISMDLogger smdLogger;
        /// <summary>
        /// Get Configured logger
        /// </summary>
        private static ISMDLogger SmdLogger
        {
            
            get
            {
                if (smdLogger != null) return smdLogger;
                smdLogger = (UnityConfig.UnityContainer).Resolve<ISMDLogger>();
                return smdLogger;
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

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CollectionScheduler()
        {
           //todo 
        }
        #endregion
        #region Funcs

        /// <summary>
        /// Scheduler Initializer
        /// </summary>
        public static void SetDebitScheduler(Registry registry )
        {
            // Registration of Debit Process Scheduler Run after every 7 days 
            registry.Schedule(PerformDebit).ToRunEvery(7).Days().At(23, 55);
        }

        /// <summary>
        /// Perform Debit work after scheduled time | 7 Days
        /// </summary>
        private static void PerformDebit()
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
                        // Secure Transation
                        using (var tran = new TransactionScope())
                        {
                            string response = null;
                            Boolean isSystemUser;
                            try
                            {
                                // If It is not System User then make transation 
                                if (user.Roles.Any(role => role.Name.ToLower().Equals("user")))
                                {
                                   response = StripeService.ChargeCustomer((int?)transaction.DebitAmount, user.StripeCustomerId);
                                   isSystemUser = false;
                                }
                                else
                                {
                                    isSystemUser = true;
                                    transaction.TxId = 0; // No Transaction Made
                                }
                                // Stripe + Invoice Work 
                                if (isSystemUser || ( response != "failed"))
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
                                        ToUser = "SMD",
                                        TxId = transaction.TxId
                                    };
                                    dbContext.TransactionLogs.Add(transactionLog);
                                    #endregion
                                    #region Add Invoice

                                    // Add invoice data
                                    var invoice = new Invoice
                                    {
                                        Country = user.CountryId.ToString(),
                                        Total = (double) transaction.DebitAmount,
                                        NetTotal = (double)transaction.DebitAmount,
                                        InvoiceDate = DateTime.Now,
                                        InvoiceDueDate = DateTime.Now.AddDays(7),
                                        Address1 = user.CountryId.ToString(),
                                        UserId = user.Id,
                                        CompanyName = "My Company",
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
                                        ItemGrossAmount = (double) transaction.DebitAmount,
                                        CampaignId = transaction.AdCampaignId,

                                    };
                                    dbContext.InvoiceDetails.Add(invoiceDetail);

                                    #endregion
                                    // Email To User 
                                    EmailSerice.SendCollectionRoutineEmail(user.Id);
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
