﻿using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces
{
    public interface IProfileQuestionTargetLocationRepository : IBaseRepository<ProfileQuestionTargetLocation, long>
    {
        //List<ProfileQuestionTargetLocation> GetPQlocation(long pqId);
       bool UpdateTargetLocation(ProfileQuestionTargetLocation Tlocation);
       bool DeleteTargetLocation(int PQID);
    }
}
