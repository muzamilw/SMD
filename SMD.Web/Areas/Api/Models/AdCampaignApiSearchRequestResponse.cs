using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model | API Resposne
    /// </summary>
    public class AdCampaignApiSearchRequestResponse
    {
        /// <summary>
        /// List of AdCampaigns
        /// </summary>
        public IEnumerable<AdCampaign> AdCampaigns { get; set; }
    }
}