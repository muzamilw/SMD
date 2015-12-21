using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model | API Resposne
    /// </summary>
    public class AdCampaignApiSearchRequestResponse : BaseApiResponse
    {
        /// <summary>
        /// List of AdCampaigns
        /// </summary>
        public IEnumerable<AdCampaignApiModel> AdCampaigns { get; set; }
    }
}