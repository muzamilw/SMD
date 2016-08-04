using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Response Model 
    /// </summary>
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