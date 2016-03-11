using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IManageUserRepository : IBaseRepository<User, int>
    {
        List<User> getManageUsers();

        
    }
}
