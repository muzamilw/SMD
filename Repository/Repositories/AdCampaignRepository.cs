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

        public List<AdCampaign> GetAdvertsByUserId() 
        {
           // return db.AdCampaigns.Where(a => a.UserId == UserId).ToList();
            return db.AdCampaigns.ToList();
        }
    }
}
