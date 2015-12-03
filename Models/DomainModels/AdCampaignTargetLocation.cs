﻿namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaignTargetLocation Domain Model
    /// </summary>
    public class AdCampaignTargetLocation
    {
        public long Id { get; set; }
        public long? SqId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Radius { get; set; }

        public virtual Country Country { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
