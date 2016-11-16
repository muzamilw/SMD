using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ISurveySharingGroupRepository : IBaseRepository<SurveySharingGroup, long>
    {
        IEnumerable<SurveySharingGroup> GetUserGroups(string UserId);

        bool DeleteUserGroup(long SharingGroupId);
    }
}
