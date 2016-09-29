using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// SurveyQuestion Domain Model
    /// </summary>
    public class SurveyQuestion
    {
        public long SqId { get; set; }
        public int? LanguageId { get; set; }
        public int? CountryId { get; set; }
        public int? Type { get; set; }
        public string UserId { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public int? RepeatPeriod { get; set; }
        public string DisplayQuestion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
        public bool? DiscountVoucherApplied { get; set; }
        public string VoucherCode { get; set; }
        public long? DiscountVoucherId { get; set; }
        public string RejectionReason { get; set; }

        public int? Status { get; set; }
        public long? ProjectedReach { get; set; }
        public long? ResultClicks { get; set; }
        public int? AgeRangeStart { get; set; }
        public int? AgeRangeEnd { get; set; }
        public int? Gender { get; set; }
        public long? ParentSurveyId { get; set; }
        public int? Priority { get; set; }
        public Nullable<int> CompanyId { get; set; }


        public int? AnswerNeeded { get; set; }

        public double? AmountCharged { get; set; }
        


        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual User User { get; set; }
        public virtual Country Country { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> LinkedSurveyQuestionTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }

        public virtual ICollection<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual Company Company { get; set; }
        //left image and right image not mapped bytes
        //[NotMapped]
        public string LeftPictureBytes { get; set; }
        //[NotMapped]
        public string RightPictureBytes { get; set; }

        public virtual ICollection<CampaignEventHistory> CampaignEventHistories { get; set; }
    }
}
