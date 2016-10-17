using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;
using SMD.Models.RequestModels;
using System.Linq.Expressions;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Country Repository 
    /// </summary>
    public class PayOutHistoryRepository : BaseRepository<PayOutHistory>, IPayOutHistoryRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PayOutHistoryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PayOutHistory> DbSet
        {
            get { return db.PayOutHistory; }
        }
        #endregion

        
        #region Public

        public PayOutHistory Find(long id)
        {
            return DbSet.Where(g => g.PayOutId == id).SingleOrDefault();
        }

        public List<PayOutHistory> GetPendingStageOnePayOuts()
        {
            return DbSet.Where(g => g.StageOneStatus == null).ToList();
        }

        public List<PayOutHistory> GetPendingStageTwoPayOuts()
        {
            return DbSet.Where(g => g.StageOneStatus.Value == 1 && g.StageTwoStatus == null).ToList();
        }
        public IEnumerable<PayOutHistory> GetPayOutHistoryForApprovalStage1(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<PayOutHistory, bool>> query =
                s => s.StageOneStatus == null;
            rowCount = DbSet.Count(query);
            var res = DbSet.Where(query)
            .OrderByDescending(p => p.CentzAmount);
            return res.Skip(fromRow)
                .Take(toRow);
        }
        public IEnumerable<PayOutHistory> GetPayOutHistoryForApprovalStage2(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<PayOutHistory, bool>> query =
                s => s.StageOneStatus == 1 && s.StageTwoStatus ==null;
            rowCount = DbSet.Count(query);
            var res = DbSet.Where(query)
             .OrderByDescending(p => p.CentzAmount);
            return res.Skip(fromRow)
                .Take(toRow);
        }

      
        
        #endregion
    }
}
