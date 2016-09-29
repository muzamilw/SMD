using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company, int>
    {
        int GetUserCompany(string userId);
        bool updateCompanyLogo(string url, int companyId);

        int createCompany(string userId, string email, string fullname,string guid);
        bool updateCompany(Company request);

        User getUserBasedOnAuthenticationToken(string token);
        string GetCompanyNameByID(int CompanyId);
        Company GetCompanyById();
        Company GetCompanyWithoutChilds(int companyId = 0);
    }
}
