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
    public class AdCampaignResponseRepository : BaseRepository<AdCampaignResponse>, IAdCampaignResponseRepository
    {

        public AdCampaignResponseRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaignResponse> DbSet
        {
            get
            {
                return db.AdCampaignResponses;
            }
        }

        /// <summary>
        /// Find Queston by Id 
        /// </summary>
        public AdCampaignResponse Find(int id)
        {
            return DbSet.Find(id);
        }

    }
}
