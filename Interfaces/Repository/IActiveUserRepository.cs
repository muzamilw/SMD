using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IActiveUserRepository : IBaseRepository<CompaniesAspNetUser, long>
    {

        ActiveUserResponseModel getActiveUser() ;
        
        
        
    }
}
