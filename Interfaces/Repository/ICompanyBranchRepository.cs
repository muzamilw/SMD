using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICompanyBranchRepository : IBaseRepository<CompanyBranch, long>
    {
        CompanyBranch GetBranchsByCategoryId(long id);
        CompanyBranch GetDefaultCompanyBranch(int companyId = 0);
    }

}
