
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    public class AudienceAdCampaignForApiResponse :BaseApiResponse
    {
        /// <summary>
        /// Matched User Count
        /// </summary>
        public long UserCount { get; set; }
    }
}