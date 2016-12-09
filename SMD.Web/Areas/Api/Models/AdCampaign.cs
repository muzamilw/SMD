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
        public Nullable<bool> ShowBuyitBtn { get; set; }
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


        public Nullable<int> viewCountToday { get; set; }
        public Nullable<int> viewCountYesterday { get; set; }
        public Nullable<int> viewCountAllTime { get; set; }
        public string Locationss { get; set; }
        public Nullable<double> MaxDailyBudget { get; set; }
        public Nullable<System.DateTime> SubmissionDateTime { get; set; }
        public Nullable<bool> IsPaymentCollected { get; set; }

        public Nullable<System.DateTime> PaymentDate { get; set; }

        public Nullable<int> clickThroughsToday { get; set; }
        public Nullable<int> clickThroughsYesterday { get; set; }
        public Nullable<int> clickThroughsAllTime { get; set; }


    }
    public class Coupon
    {
        public string CouponImage4 { get; set; }

        public string CouponImage5 { get; set; }

        public string CouponImage6 { get; set; }
        public long CouponId { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public string UserId { get; set; }
        public string CouponTitle { get; set; }
        public string SearchKeywords { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> Archived { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovalDateTime { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public string RejectedReason { get; set; }
        public Nullable<System.DateTime> Rejecteddatetime { get; set; }
        public string RejectedBy { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public Nullable<double> SwapCost { get; set; }
        public Nullable<int> CouponViewCount { get; set; }
        public Nullable<int> CouponIssuedCount { get; set; }
        public Nullable<int> CouponRedeemedCount { get; set; }
        public Nullable<int> CouponQtyPerUser { get; set; }
        public Nullable<int> CouponListingMode { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CouponActiveMonth { get; set; }
        public Nullable<int> CouponActiveYear { get; set; }
        public Nullable<System.DateTime> CouponExpirydate { get; set; }
        public string couponImage1 { get; set; }
        public string CouponImage2 { get; set; }
        public string CouponImage3 { get; set; }
        public string LogoUrl { get; set; }
        public string HighlightLine1 { get; set; }
        public string HighlightLine2 { get; set; }
        public string HighlightLine3 { get; set; }
        public string HighlightLine4 { get; set; }
        public string HighlightLine5 { get; set; }
        public string FinePrintLine1 { get; set; }
        public string FinePrintLine2 { get; set; }
        public string FinePrintLine3 { get; set; }
        public string FinePrintLine4 { get; set; }
        public string FinePrintLine5 { get; set; }
        public Nullable<long> LocationBranchId { get; set; }
        public string LocationTitle { get; set; }
        public string LocationLine1 { get; set; }
        public string LocationLine2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZipCode { get; set; }
        public string LocationLAT { get; set; }
        public string LocationLON { get; set; }
        public string LocationPhone { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeographyColumn { get; set; }
        public string HowToRedeemLine1 { get; set; }
        public string HowToRedeemLine2 { get; set; }
        public string HowToRedeemLine3 { get; set; }
        public string HowToRedeemLine4 { get; set; }
        public string HowToRedeemLine5 { get; set; }
        public Nullable<System.DateTime> SubmissionDateTime { get; set; }
        public Nullable<System.DateTime> CouponStartDate { get; set; }
        public Nullable<System.DateTime> CouponEndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public string CurrencySymbol { get; set; }

        public Nullable<bool> ShowBuyitBtn { get; set; }
        public string BuyitLandingPageUrl { get; set; }
        public string BuyitBtnLabel { get; set; }

        public virtual IEnumerable<SMD.MIS.Areas.Api.Models.CouponCategories> CouponCategories { get; set; }
        public string YoutubeLink { get; set; }
        public Nullable<bool> IsPaymentCollected { get; set; }

        public Nullable<System.DateTime> PaymentDate { get; set; }
        
        public virtual IEnumerable<CouponPriceOption> CouponPriceOptions { get; set; }
        public Nullable<bool> IsShowReviews { get; set; }
        public Nullable<bool> IsShowAddress { get; set; }
        public Nullable<bool> IsShowPhoneNo { get; set; }
        public Nullable<bool> IsShowMap { get; set; }
        public Nullable<bool> IsShowyouTube { get; set; }
        public Nullable<bool> IsShowAboutUs { get; set; }
        public int DealsinGroupCount { get; set; }
        public Nullable<bool> IsPerSaving3days { get; set; }
        public Nullable<bool> IsPerSaving2days { get; set; }
        public Nullable<bool> IsPerSavingLastday { get; set; }
        public Nullable<bool> IsDollarSaving3days { get; set; }
        public Nullable<bool> IsDollarSaving2days { get; set; }
        public Nullable<bool> IsDollarSavingLastday { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}