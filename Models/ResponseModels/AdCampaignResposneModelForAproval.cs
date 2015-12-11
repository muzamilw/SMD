﻿using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    public class AdCampaignResposneModelForAproval
    {
        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
        public IEnumerable<AdCampaign> AdCampaigns { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
    }
}
