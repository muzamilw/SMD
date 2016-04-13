using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
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
    public class CampaignCategoriesRepository : BaseRepository<CampaignCategory>, ICampaignCategoriesRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
       public CampaignCategoriesRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
       protected override IDbSet<CampaignCategory> DbSet
        {
            get { return db.CampaignCategories; }
        }
        #endregion
       public void RemoveAll(List<CampaignCategory> categories)
       {

           db.CampaignCategories.RemoveRange(categories);
           db.SaveChanges();

       }
    }
}
