using System.Transactions;
using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Debit Scheduler BackGround Service 
    /// </summary>
    public class DebitScheduler : Registry
    {
        #region Private
        // Properties 
        [Dependency]
        private ITransactionService TransactionService { get; set; }

        [Dependency]
        private WebApiUserService WebApiUserService { get; set; }

        [Dependency]
        private IStripeService StripeService { get; set; }

        [Dependency]
        private ITransactionRepository TransactionRepository { get; set; }

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DebitScheduler()
        {
            // Registration of Debit Process Scheduler Run after every 7 days 
            Schedule(PerformDebit).ToRunEvery(7).Days().At(23,55);
        }

        /// <summary>
        /// Perform Debit work after scheduled time | 7 Days
        /// </summary>
        private void PerformDebit()
        {
            // Get All Un-Processed Debit Transactions 
            var unProcessedTrasactions = TransactionService.GetUnprocessedTransactionsForDebit();
            foreach (var transaction in unProcessedTrasactions)
            {
                if (transaction.AccountId != null)
                {
                    // Get User from which credit to debit 
                   var user = WebApiUserService.GetUserByUserId(transaction.Account.UserId);
                    // Secure Transation
                    using (var tran = new TransactionScope())
                    {
                        // Stripe + Invoice Work 
                        var response = StripeService.ChargeCustomer((int?)transaction.DebitAmount, user.StripeCustomerId);
                        if (response != "failed")
                        {
                            // Success
                            transaction.isProcessed = true;
                        }
                        // Indicates we are happy
                        tran.Complete();
                    }
                }
            }
            TransactionRepository.SaveChanges();
        }

        #endregion
    }
}
