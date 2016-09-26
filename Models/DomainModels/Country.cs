using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public Nullable<int> CurrencyID { get; set; }



        public virtual ICollection<ProfileQuestionGroup> ProfileQuestionGroups { get; set; }
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<ProfileQuestionTargetLocation> ProfileQuestionTargetLocations { get; set; }
    }
}
