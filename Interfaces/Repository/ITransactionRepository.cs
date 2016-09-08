using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Transaction Repository Interface 
    /// </summary>
    public interface ITransactionRepository : IBaseRepository<Transaction, long>
    {
        /// <summary>
        /// Get Un-debited transactions
        /// </summary>
        IEnumerable<Transaction> GetUnprocessedTransactionsForDebit();
        List<vw_GetUserTransactions> GetUserTransactions();

        List<GetTransactions_Result> GetTransactions(int CompanyId, int AccountType, int Rows);
        IEnumerable<Transaction> GetTransactionByAccountId(int accountId);
    }
}
