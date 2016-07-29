//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModelProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        public City()
        {
            this.AdCampaignTargetLocations = new HashSet<AdCampaignTargetLocation>();
            this.Companies = new HashSet<Company>();
            this.SurveyQuestionTargetLocations = new HashSet<SurveyQuestionTargetLocation>();
        }
    
        public int CityID { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> IsCapital { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string GeoLONG { get; set; }
        public string GeoLAT { get; set; }
    
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
    }
}
