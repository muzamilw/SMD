using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

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
      
        
        #endregion
    }
}
