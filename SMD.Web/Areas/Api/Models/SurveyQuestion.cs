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

    }
}