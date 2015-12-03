using System;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaignResponse Domain Model
    /// </summary>
    public class AdCampaignResponse
    {
        public int ResponseId { get; set; }
        public long? CampaignId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public double? EndUserDollarAmount { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual User AspNetUser { get; set; }
    }
}
