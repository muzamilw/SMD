using System;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Profile Question Domain Model
    /// </summary>
    public  class ProfileQuestion
    {
        public int PqId { get; set; }
        public int? LanguageId { get; set; }
        public int? CountryId { get; set; }
        public int? ProfileGroupId { get; set; }
        public int? Priority { get; set; }
        public int? Type { get; set; }
        public string Question { get; set; }
        public int? RefreshTime { get; set; }
        public int? SkippedCount { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? HasLinkedQuestions { get; set; }
        public int? PenalityForNotAnswering { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<ProfileQuestionAnswer> ProfileQuestionAnswers { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ProfileQuestionGroup ProfileQuestionGroup { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
    }
}
