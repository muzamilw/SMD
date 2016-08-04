
namespace SMD.Models.DomainModels
{
    /// <summary>
    /// SP Response For API 
    /// </summary>
    public class GetAds_Result
    {
        public long CampaignID { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public string VerifyQuestion { get; set; }
        public string LandingPageVideoLink { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int? CorrectAnswer { get; set; }
        public double? ClickRate { get; set; }
        public int? AdType { get; set; }
    }
}
