using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{

    public interface IAspnetUsersRepository : IBaseRepository<User, long>
    {
        int GetUserProfileCompletness(string UserId);
        String GetUserEmail(int companyId);
        String GetUserid(int companyId);
        IEnumerable<GetRegisteredUserData_Result> GetRegisteredUsers(RegisteredUsersSearchRequest request, out int rowCount);
    }
}
