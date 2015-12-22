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
    public class AdCampaignTargetCriteriaRepository : BaseRepository<AdCampaignTargetCriteria>, IAdCampaignTargetCriteriaRepository
    {

        public AdCampaignTargetCriteriaRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaignTargetCriteria> DbSet
        {
            get
            {
                return db.AdCampaignTargetCriterias;
            }
        }

        public void RemoveAll(List<AdCampaignTargetCriteria> criterias)
        {

            db.AdCampaignTargetCriterias.RemoveRange(criterias);
            db.SaveChanges();
           
        }
    }
}
