using SMD.Models.Common;
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Ad Viewed Request Model 
    /// </summary>
    public class AdViewedRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// AdCampaign Id
        /// </summary>
        public long AdCampaignId { get; set; }

        public int companyId { get; set; }
        public int? userQuizSelection { get; set; }

        public CampaignResponseEventType ResponeEventType { get; set; }
    }
}
