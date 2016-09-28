
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

        public bool IsCompanyBranchExist()
        {
            return DbSet.Any(b => b.CompanyId == CompanyId);
        }
       
    }
}
