using System;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Profile Question Web Model
    /// </summary>
    public class ProfileQuestion
    {
        public int PqId { get; set; }
        public int? LanguageId { get; set; }
        public int? CountryId { get; set; }
        public int? ProfileGroupId { get; set; }
        public string   ProfileGroupName { get; set; }
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
        public Nullable<int> AnswerNeeded { get; set; }
        public Nullable<int> AsnswerCount { get; set; }
        public Nullable<int> AgeRangeStart { get; set; }
        public Nullable<int> AgeRangeEnd { get; set; }
        public Nullable<int> Gender { get; set; }

        public int? Status { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedByUserID { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string RejectionReason { get; set; }
        public string UserID { get; set; }
        public double? AmountCharged { get; set; }

        public List<ProfileQuestionAnswer> ProfileQuestionAnswers { get; set; }
        public List<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriteria { get; set; }
        public List<ProfileQuestionTargetLocation> ProfileQuestionTargetLocation { get; set; }

    }
}