﻿using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignTargetLocationRepository : IBaseRepository<AdCampaignTargetLocation, long>
    {
        void RemoveAll(List<AdCampaignTargetLocation> locations);
    }
}
