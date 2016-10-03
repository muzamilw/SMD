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
    /// Tax Repository 
    /// </summary>
    public class AdCampaignClickRateHistoryRepository : BaseRepository<AdCampaignClickRateHistory>, IAdCampaignClickRateHistoryRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public AdCampaignClickRateHistoryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaignClickRateHistory> DbSet
        {
            get { return db.AdCampaignClickRateHistory; }
        }
        #endregion
        #region Public

      
        #endregion
    }
}
