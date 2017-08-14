using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AdCampaignTargetLocation
    {
        public long Id { get; set; }
        public long? CampaignId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Radius { get; set; }
        public bool? IncludeorExclude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}