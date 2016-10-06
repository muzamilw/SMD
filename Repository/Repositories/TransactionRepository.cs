using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;
using System;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Transaction Repository 
    /// </summary>
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        #region Private
       
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public TransactionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Transaction> DbSet
        {
            get { return db.Transactions; }
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Un-debited transactions
        /// </summary>
        public IEnumerable<Transaction> GetUnprocessedTransactionsForDebit()
        {
            return DbSet.Where(trans => trans.DebitAmount != null && trans.CreditAmount == null 
                && (trans.isProcessed == null || trans.isProcessed == false)).ToList();
        }
        public IEnumerable<Transaction> GetTransactionByAccountId(int accountId)
        {
            return DbSet.Where(trans => trans.AccountId == accountId).ToList();
        }
        public IEnumerable<GetRevenueOverTime_Result> GetRevenueOverTime(int CompanyId, DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            return db.GetRevenueOverTime(CompanyId, DateFrom, DateTo, Granularity);
        }

        
        public List<vw_GetUserTransactions> GetUserTransactions()
        {
            return db.vw_GetUserTransactions.ToList();
        }


          public List<GetTransactions_Result> GetTransactions(int CompanyId, int AccountType, int Rows )
        {
            return db.GetTransactions(CompanyId, AccountType, Rows).ToList();

        }

          public IEnumerable<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights()
          {
              return db.GetAdminDashBoardInsights();

          }
          public IEnumerable<getPayoutVSRevenueOverTime_Result> getPayoutVSRevenueOverTime(DateTime DateFrom, DateTime DateTo, int Granularity)
          {
              return db.getPayoutVSRevenueOverTime(DateFrom, DateTo, Granularity);
          }
          public IEnumerable<GetRevenueByCampaignOverTime_Result> GetRevenueByCampaignOverTime(int compnyId, int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity)
          {
              return db.GetRevenueByCampaignOverTime(compnyId,CampaignType, DateFrom, DateTo, Granularity);
          }
        #endregion 
    }
}
