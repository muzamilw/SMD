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
    
    public partial class ProfileQuestionAnswer
    {
        public ProfileQuestionAnswer()
        {
            this.AdCampaignTargetCriterias = new HashSet<AdCampaignTargetCriteria>();
            this.SurveyQuestionTargetCriterias = new HashSet<SurveyQuestionTargetCriteria>();
            this.ProfileQuestionTargetCriterias = new HashSet<ProfileQuestionTargetCriteria>();
            this.ProfileQuestionUserAnswers = new HashSet<ProfileQuestionUserAnswer>();
        }
    
        public int PQAnswerID { get; set; }
        public Nullable<int> PQID { get; set; }
        public Nullable<int> type { get; set; }
        public string AnswerString { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> LinkedQuestion1ID { get; set; }
        public Nullable<int> LinkedQuestion2ID { get; set; }
        public Nullable<int> LinkedQuestion3ID { get; set; }
        public Nullable<int> LinkedQuestion4ID { get; set; }
        public Nullable<int> LinkedQuestion5ID { get; set; }
        public Nullable<int> LinkedQuestion6ID { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
        public virtual ICollection<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
    }
}
