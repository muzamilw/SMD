using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface ISurveySharingGroupService
    {

        SurveySharingGroup Create(SurveySharingGroup group);

        List<SurveySharingGroup> GetUserGroups(string UserId);

        SurveySharingGroup GetGroupDetails(long SharingGroupId);


        SurveySharingGroup Update(SurveySharingGroup group);

        bool DeleteGroup(long SharingGroupId);
    }
}
