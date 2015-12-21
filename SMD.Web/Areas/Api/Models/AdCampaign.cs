using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model
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
        public List<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public List<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
    }
}