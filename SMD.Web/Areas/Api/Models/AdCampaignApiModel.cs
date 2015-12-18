
namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// API Model | Web 
    /// </summary>
    public class AdCampaignApiModel
    {
        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public string VerifyQuestion { get; set; }
        public string LandingPageVideoLink { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int? CorrectAnswer { get; set; }
        public double? ClickRate { get; set; }
    }
}