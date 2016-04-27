using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Transaction Service Implementation 
    /// </summary>
    public class TransactionService : ITransactionService
    {
        #region Private

        private readonly ITransactionRepository transactionRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        #endregion
        #region Public
        public void SurveyApprovalTransaction(long sQid)
        {

        }

        /// <summary>
        /// Get Un-debited transactions
        /// </summary>
        public IEnumerable<Transaction> GetUnprocessedTransactionsForDebit()
        {
            return transactionRepository.GetUnprocessedTransactionsForDebit();
        }
        public List<vw_GetUserTransactions> GetUserTransactions()
        {
            return transactionRepository.GetUserTransactions();
        }
        #endregion
    }
}
