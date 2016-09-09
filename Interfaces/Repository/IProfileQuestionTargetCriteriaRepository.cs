﻿using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IProfileQuestionTargetCriteriaRepository : IBaseRepository<ProfileQuestionTargetCriteria, long>
    {
        List<ProfileQuestionTargetCriteria> GetProfileQuestionTargetCiteria(long PQID);
         void RemoveCriteria(int PqID);
         bool UpdateTargetcriteria(ProfileQuestionTargetCriteria Crt);
    }
}
