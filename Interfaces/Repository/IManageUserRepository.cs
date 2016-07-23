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
    public interface IManageUserRepository : IBaseRepository<User, int>
    {
        List<vw_CompanyUsers> getManageUsers(int CompanyId);

        List<Role> getUserRoles();

        void UpdateRoles(string Id, UpdateUserProfileRequest SourceUser);

        string getCompanyName(out string UserName, out int companyid);
        string getCompanyName(out string UserName,  int companyid);
        User GetByUserId(string userId);
    }
}
