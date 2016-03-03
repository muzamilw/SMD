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

        bool createCompany(string userId, string email, string fullname,string guid);
        bool updateCompany(UpdateUserProfileRequest request);

        User getUserBasedOnAuthenticationToken(string token);
    }
}
