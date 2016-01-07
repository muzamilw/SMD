
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Transaction Service INterface
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Makes Transaction on Approval 
        /// </summary>
        void SurveyApprovalTransaction(long sQid);

        /// <summary>
        /// Get Un-debited transactions
        /// </summary>
        IEnumerable<Transaction> GetUnprocessedTransactionsForDebit();
    }
}
