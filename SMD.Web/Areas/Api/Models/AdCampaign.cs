using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string CampaignImagePath { get; set; }
        public string CampaignTypeImagePath { get; set; }
        public List<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public List<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public string Voucher1Heading { get; set; }
        public string Voucher1Description { get; set; }
        public string Voucher1Value { get; set; }
        public string Voucher2Heading { get; set; }
        public string Voucher2Description { get; set; }
        public string Voucher2Value { get; set; }

        public string Voucher1ImagePath { get; set; }
        public string VoucherImagePath { get; set; }
        public string VideoUrl { get; set; }
        public string BuuyItLine1 { get; set; }
        public string BuyItLine2 { get; set; }
        public string BuyItLine3 { get; set; }
        public string BuyItButtonLabel { get; set; }
        public string BuyItImageUrl { get; set; }
        public bool IsShowVoucherSetting { get; set; }
        public string VideoLink2 { get; set; }
        [NotMapped]

        public int AdViews { get; set; }

        public int? CompanyId { get; set; }
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public Nullable<int> CouponTakenCount { get; set; }
        public Nullable<int> CouponQuantity { get; set; }
        public Nullable<int> priority { get; set; }
        public Nullable<double> CouponDiscountValue { get; set; }
        public string couponSmdComission { get; set; }
        public string couponImage2 { get; set; }

        public List<CouponCategory> CouponCategories { get; set; }
        public string CouponImage3 { get; set; }
        public string CouponImage4 { get; set; }
        public string CouponExpiryLabel { get; set; }
        public List<CouponCodeModel> CouponCodes { get; set; }
        public int IsUseFilter { get; set; }
        public string LogoUrl { get; set; }
        public string VoucherAdditionalInfo { get; set; }
        public long CouponId { get; set; }
        public int CouponType { get; set; }
        public bool IsSavedCoupon { get; set; }
        public int DeliveryDays { get; set; }
        public int ChannelType { get; set; }
    }
}