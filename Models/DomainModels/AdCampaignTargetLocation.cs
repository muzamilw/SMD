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
        public bool? IncludeorExclude { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual AdCampaign AdCampaign { get; set; }

        /// Makes a copy of Campaign response
        /// </summary>
        public void Clone(AdCampaignTargetLocation target)
        {

            target.CountryId = CountryId;
            target.CityId = CityId;
            target.Radius = Radius;
            target.IncludeorExclude = IncludeorExclude;
           
        }
    }
}
