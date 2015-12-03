using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.IdentityModels;

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
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
        public bool? DiscountVoucherApplied { get; set; }
        public string VoucherCode { get; set; }
        public long? DiscountVoucherId { get; set; }
        public string RejectionReason { get; set; }

        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual User User { get; set; }
        public virtual Country Country { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> LinkedSurveyQuestionTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
    }
}
