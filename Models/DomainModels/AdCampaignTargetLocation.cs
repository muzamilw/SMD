namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaignTargetLocation Domain Model
    /// </summary>
    public class AdCampaignTargetLocation
    {
        public long Id { get; set; }
        public long? CampaignId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Radius { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual AdCampaign AdCampaign { get; set; }
    }
}
