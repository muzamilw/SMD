using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Get Ad Campaign | API Reposne
    /// </summary>
    public class AdCampaignApiSearchRequestResponse
    {
        /// <summary>
        /// List of AdCampaigns
        /// </summary>
        public IEnumerable<AdCampaign> AdCampaigns { get; set; }
    }
}
