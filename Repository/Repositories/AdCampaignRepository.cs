using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class AdCampaignRepository : BaseRepository<AdCampaign>, IAdCampaignRepository
    {

        public AdCampaignRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaign> DbSet
        {
            get
            {
                return db.AdCampaigns;
            }
        }

        public List<AdvertGridRequest> GetAdvertsByUserId()
        {
            var query = from ad in db.AdCampaigns
                        where ad.UserId == LoggedInUserIdentity
                        select new AdvertGridRequest()
                        {
                            AmountSpent = ad.AmountSpent,
                            CampaignId = ad.CampaignId,
                            DisplayTitle = ad.DisplayTitle,
                            StartDateTime = ad.StartDateTime ?? DateTime.Now,
                            Status = ad.Status ?? 0,
                            ClickRate = ad.ClickRate ?? 0.0,
                            EndDateTime = ad.EndDateTime ?? DateTime.Now,
                            MaxBudget = ad.MaxBudget ?? 0.0,
                            ResultClicks = ad.ResultClicks ?? 0
                        };

            return query.ToList<AdvertGridRequest>();

        }
    }
}
