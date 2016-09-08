using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;

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


        public List<GetTransactions_Result> GetUserVirtualTransactions(int companyId)
        {


            return transactionRepository.GetTransactions(companyId, 4, 30);
        }

        public IEnumerable<RevenueOverTimeResponseModel> getRevenueOverTime(int accountId)
        {

            IEnumerable<Transaction> list = transactionRepository.GetTransactionByAccountId(accountId);

            List<RevenueOverTimeResponseModel> result = new List<RevenueOverTimeResponseModel>();

            return result;

        }

       // public IEnumerable<Transaction> GetTransactionByAccountId(int accountId)
        #endregion
    }
}
