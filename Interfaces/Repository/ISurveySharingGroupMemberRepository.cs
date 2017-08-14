using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ISurveySharingGroupMemberRepository : IBaseRepository<SurveySharingGroupMember, long>
    {
        List<SurveySharingGroupMember> GetAllGroupMembers(long SharingGroupId);
    }
}
