using Microsoft.Practices.Unity;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SMD.Models.DomainModels;
using System.Threading.Tasks;
using SMD.Interfaces.Repository;

namespace SMD.Repository.Repositories
{
    public class BranchCategoryRepository : BaseRepository<BranchCategory>, IBranchCategoryRepository
    {


        public BranchCategoryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        
         protected override IDbSet<BranchCategory> DbSet
         {
            get { return db.BranchCategories; }
         }
        /// <summary>
        /// Get Branch Categories on the base of Companyid
        /// </summary>
        /// <returns></returns>
         public List<BranchCategory> GetAllBranchCategories()
         {
             db.Configuration.LazyLoadingEnabled = false;
             return DbSet.Include(c => c.CompanyBranches).Where(c => c.CompanyId == CompanyId).ToList();
            
         }
      

    }
}
