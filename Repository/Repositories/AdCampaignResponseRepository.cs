using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        /// <summary>
        /// Returns Users response for campaign
        /// </summary>
        public AdCampaignResponse GetByUserId(long campaignId, string userId)
        {
            return DbSet.FirstOrDefault(adr => adr.CampaignId == campaignId && adr.UserId == userId);
        }
        public int getCampaignByIdQQFormAnalytic(int CampaignId, int Choice, int Gender, int age, string Profession, string City)
        { 
             return db.getCampaignByIdQQFormAnalytic(CampaignId, Choice, Gender, age, Profession, City).ToList().FirstOrDefault();
        }

       
    }
}
