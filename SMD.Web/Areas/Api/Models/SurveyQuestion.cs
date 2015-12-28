using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model 
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
        public int? Status { get; set; }
        public string DisplayQuestion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Approved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
        public bool? DiscountVoucherApplied { get; set; }
        public string VoucherCode { get; set; }
        public long? DiscountVoucherId { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorAddress { get; set; }
        public int? AgeRangeStart { get; set; }
        public int? AgeRangeEnd { get; set; }
        public List<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
        public List<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
        public int? Gender { get; set; }
    }
    public class SurveyQuestionTargetCriteria
    {
        public long Id { get; set; }
        public long? SqId { get; set; }
        public int? Type { get; set; }
        public int? PqId { get; set; }
        public int? PqAnswerId { get; set; }
        public long? LinkedSqId { get; set; }
        public int? LinkedSqAnswer { get; set; }
        public bool? IncludeorExclude { get; set; }
        public int? LanguageId { get; set; }
        public int? IndustryId { get; set; }

        public string questionString { get; set; }   // survey Question , profile Question,  industry name
        public string answerString { get; set; }  // survey Question Answer , profile Question Answer
        public string Language { get; set; } // language name
        public string Industry { get; set; }  // industry name 
    }
    public class SurveyQuestionTargetLocation
    {
        public long Id { get; set; }
        public long? SqId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Radius { get; set; }
        public bool? IncludeorExclude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}