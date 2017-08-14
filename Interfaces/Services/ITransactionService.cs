
using System.Collections.Generic;
using SMD.Models.DomainModels;
using System;

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
        List<vw_GetUserTransactions> GetUserTransactions();

        List<GetTransactions_Result> GetUserVirtualTransactions(int companyId);
        IEnumerable<getPayoutVSRevenueOverTime_Result> getPayoutVSRevenueOverTime(DateTime DateFrom, DateTime DateTo, int Granularity);
        IEnumerable<GetRevenueByCampaignOverTime_Result> GetRevenueByCampaignOverTime(int compnyId, int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity);
    }
}
