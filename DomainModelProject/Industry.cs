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
    
    public partial class Industry
    {
        public Industry()
        {
            this.AdCampaignTargetCriterias = new HashSet<AdCampaignTargetCriteria>();
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.SurveyQuestionTargetCriterias = new HashSet<SurveyQuestionTargetCriteria>();
        }
    
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
    }
}