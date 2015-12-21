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
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}