using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{

    public interface IAspnetUsersRepository : IBaseRepository<User, long>
    {
        int GetUserProfileCompletness(string UserId);
        String GetUserEmail(int companyId);
        String GetUserid(int companyId);
    }
}
