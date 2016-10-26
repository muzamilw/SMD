using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;
using SMD.Models.Common;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Tax Repository 
    /// </summary>
    public class CampaignEventHistoryRepository : BaseRepository<CampaignEventHistory>, ICampaignEventHistoryRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CampaignEventHistoryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CampaignEventHistory> DbSet
        {
            get { return db.CampaignEventHistory; }
        }
        #endregion
        #region Public

        public bool InsertCampaignEvent(AdCampaignStatus Event, long Campaignid)
        {
            var eventx = new CampaignEventHistory { CampaignID = Campaignid, EventStatusId = (int)Event, EventDateTime = DateTime.Now};

            db.CampaignEventHistory.Add(eventx);
            db.SaveChanges();

            return true;
        }

        public bool InsertCouponEvent(AdCampaignStatus Event, long CouponId)
        {
            var eventx = new CampaignEventHistory { CouponId = CouponId, EventStatusId = (int)Event, EventDateTime = DateTime.Now };

            db.CampaignEventHistory.Add(eventx);
            db.SaveChanges();

            return true;

        }

        public bool InsertSurveyQuestionEvent(AdCampaignStatus Event, long SQID)
        {
            var eventx = new CampaignEventHistory { SQID = SQID, EventStatusId = (int)Event, EventDateTime = DateTime.Now };

            db.CampaignEventHistory.Add(eventx);
            db.SaveChanges();

            return true;
        }

        public bool InsertProfileQuestionEvent(AdCampaignStatus Event, int PQID)
        {
            var eventx = new CampaignEventHistory { PQID = PQID, EventStatusId = (int)Event, EventDateTime = DateTime.Now };

            db.CampaignEventHistory.Add(eventx);
            db.SaveChanges();

            return true;
        }
        #endregion
    }
}
