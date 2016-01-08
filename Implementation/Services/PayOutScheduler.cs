using System.Linq;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using System.Web.Http.Filters;
using System.Web.Configuration;

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
        public PayOutScheduler()
        {
           //todo 
        }
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
        private static void PerformCredit()
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

                        var smdUser = dbContext.Users.FirstOrDefault(obj => obj.Email == "MuzzammilShb@mpc.com");
                        if (smdUser == null)
                        {
                            throw new Exception("SMD User does not exist!");
                        }

                        // Secure Transation
                        using (var tran = new TransactionScope())
                        {
                            try
                            {
                                // PayPal Request Model 
                                var requestModel = new MakePaypalPaymentRequest()
                                {
                                    Amount = (int?) transaction.CreditAmount,
                                    RecieverEmails = new List<string> { preferedAccount },
                                    SenderEmail = smdUser.PaypalCustomerId
                                };

                                // Stripe + Invoice Work 
                                PaypalService.MakeAdaptiveImplicitPayment(requestModel);

                                transaction.isProcessed = true;
                                var transactionLog = new TransactionLog
                                {
                                    Amount = (double) requestModel.Amount,
                                    FromUser = requestModel.SenderEmail,
                                    Type = 1, // credit 
                                    IsCompleted = true,
                                    LogDate = DateTime.Now,
                                    ToUser = requestModel.RecieverEmails.FirstOrDefault(),
                                    TxId = 1
                                };
                                dbContext.TransactionLogs.Add(transactionLog);
                                dbContext.SaveChanges();

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
