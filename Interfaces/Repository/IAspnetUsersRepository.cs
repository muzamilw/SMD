using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{

    public interface IAspnetUsersRepository : IBaseRepository<User, long>
    {
        int GetUserProfileCompletness(string UserId);
    }
}
