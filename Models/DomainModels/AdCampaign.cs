using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaign Domain Model
    /// </summary>
    public class AdCampaign
    {
        public long CampaignId { get; set; }
        public int? LanguageId { get; set; }
        public string UserId { get; set; }
        public bool? SmdCampaign { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDescription { get; set; }
        public int? Status { get; set; }
        public bool? Archived { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public double? MaxBudget { get; set; }
        public int? Type { get; set; }
        public double? ClickRate { get; set; }
        public int? SmdCredits { get; set; }
        public string DisplayTitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string LandingPageVideoLink { get; set; }
        public string VerifyQuestion { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int? CorrectAnswer { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public int? AgeRangeStart { get; set; }
        public int? AgeRangeEnd { get; set; }
        public int? Gender { get; set; }
        public string RejectedReason { get; set; }
        public long? ProjectedReach { get; set; }
        public long? ResultClicks { get; set; }
        public double? AmountSpent { get; set; }
        public int? RewardType { get; set; }
        public string Voucher1Heading { get; set; }
        public string Voucher1Description { get; set; } 
        public string Voucher1Value { get; set; }
        public string Voucher1ImagePath { get; set; }
        public string Voucher2Heading { get; set; }
        public string Voucher2Description { get; set; }
        public string Voucher2Value { get; set; }
        public string Voucher2ImagePath { get; set; }
        public string VideoUrl { get; set; }
        public string BuuyItLine1 { get; set; }
        public string BuyItLine2 { get; set; }
        public string BuyItLine3 { get; set; }
        public string BuyItButtonLabel { get; set; }
        public string BuyItImageUrl { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [NotMapped]
        public string CampaignImagePath { get; set; }
        [NotMapped]
        public string CampaignTypeImagePath { get; set; }
        [NotMapped]
        public string VoucherImagePath { get; set; }
        [NotMapped]
        public string buyItImageBytes { get; set; }
    }
}
