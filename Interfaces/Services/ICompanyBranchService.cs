using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface ICompanyBranchService
    {
        CompanyBranch GetBranchsByCategoryId(long id);
        long CreateBranchAddress(CompanyBranch branch);
        long UpdateBranchAddress(CompanyBranch branch);
        bool DeleteCompanyBranch(CompanyBranch branch);
        List<Country> GetAllCountries();
    }
}
