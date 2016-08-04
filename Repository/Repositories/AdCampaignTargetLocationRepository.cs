using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
   
    public class AdCampaignTargetLocationRepository : BaseRepository<AdCampaignTargetLocation>, IAdCampaignTargetLocationRepository
    {

        public AdCampaignTargetLocationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaignTargetLocation> DbSet
        {
            get
            {
                return db.AdCampaignTargetLocations;
            }
        }

        public void RemoveAll(List<AdCampaignTargetLocation> locations)
        {

            db.AdCampaignTargetLocations.RemoveRange(locations);
            db.SaveChanges();

        }
    }
}
