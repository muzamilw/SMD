using System.Collections.Generic;
using SMD.Models.DomainModels;
using System;

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
        IEnumerable<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights();
        IEnumerable<GetRevenueOverTime_Result> GetRevenueOverTime(int CompanyId, DateTime DateFrom, DateTime DateTo, int Granularity);


    
    }
}
