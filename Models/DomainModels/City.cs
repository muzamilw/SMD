using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> IsCapital { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
