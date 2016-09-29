
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
    public class CompanyBranchRepository : BaseRepository<CompanyBranch>, ICompanyBranchRepository
    {
        public CompanyBranchRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<CompanyBranch> DbSet
        {
            get { return db.CompanyBranches; }
        }
        public CompanyBranch GetBranchsByCategoryId(long id)
        {
            return DbSet.Where(b => b.BranchId == id).FirstOrDefault();

        }

        public CompanyBranch GetDefaultCompanyBranch(int companyId = 0)
        {
            //Condition is to apply for isDefault once field added int DB
            int compId = companyId > 0 ? companyId : CompanyId;
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.FirstOrDefault(b => b.CompanyId == compId);
        }
       
    }
}
