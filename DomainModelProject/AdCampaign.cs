//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModelProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdCampaign
    {
        public AdCampaign()
        {
            this.AdCampaignResponses = new HashSet<AdCampaignResponse>();
            this.AdCampaignTargetCriterias = new HashSet<AdCampaignTargetCriteria>();
            this.AdCampaignTargetCriterias1 = new HashSet<AdCampaignTargetCriteria>();
            this.AdCampaignTargetLocations = new HashSet<AdCampaignTargetLocation>();
            this.CampaignCategories = new HashSet<CampaignCategory>();
            this.CouponCodes = new HashSet<CouponCode>();
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    
        public long CampaignID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public string UserID { get; set; }
        public Nullable<bool> SMDCampaign { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDescription { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> Archived { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovalDateTime { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public Nullable<double> MaxBudget { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<double> ClickRate { get; set; }
        public Nullable<int> SMDCredits { get; set; }
        public string DisplayTitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string LandingPageVideoLink { get; set; }
        public string VerifyQuestion { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public Nullable<int> CorrectAnswer { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> AgeRangeStart { get; set; }
        public Nullable<int> AgeRangeEnd { get; set; }
        public Nullable<int> Gender { get; set; }
        public string RejectedReason { get; set; }
        public Nullable<long> ProjectedReach { get; set; }
        public Nullable<long> ResultClicks { get; set; }
        public Nullable<double> AmountSpent { get; set; }
        public Nullable<int> RewardType { get; set; }
        public string Voucher1Heading { get; set; }
        public string Voucher1Description { get; set; }
        public string Voucher1Value { get; set; }
        public string Voucher2Heading { get; set; }
        public string Voucher2Description { get; set; }
        public string Voucher2Value { get; set; }
        public string Voucher1ImagePath { get; set; }
        public string Voucher2ImagePath { get; set; }
        public string VideoUrl { get; set; }
        public string BuuyItLine1 { get; set; }
        public string BuyItLine2 { get; set; }
        public string BuyItLine3 { get; set; }
        public string BuyItButtonLabel { get; set; }
        public string BuyItImageUrl { get; set; }
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public Nullable<int> CouponTakenCount { get; set; }
        public Nullable<int> CouponQuantity { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> priority { get; set; }
        public string couponSmdComission { get; set; }
        public string couponImage2 { get; set; }
        public string CouponImage3 { get; set; }
        public string CouponImage4 { get; set; }
        public string CouponExpiryLabel { get; set; }
        public Nullable<double> CouponDiscountValue { get; set; }
        public Nullable<int> CouponType { get; set; }
        public Nullable<int> DeliveryDays { get; set; }
        public Nullable<bool> IsUseFilter { get; set; }
        public string LogoUrl { get; set; }
        public string VoucherAdditionalInfo { get; set; }
        public Nullable<long> CouponId { get; set; }
        public Nullable<bool> IsShowVoucherSetting { get; set; }
        public string VideoLink2 { get; set; }
        public string VoucherHighlightLine1 { get; set; }
        public string VoucherHighlightLine2 { get; set; }
        public string VoucherHighlightLine3 { get; set; }
        public string VoucherHighlightLine4 { get; set; }
        public string VoucherHighlightLine5 { get; set; }
        public string VoucherFinePrintLine1 { get; set; }
        public string VoucherFinePrintLine2 { get; set; }
        public string VoucherFinePrintLine3 { get; set; }
        public string VoucherFinePrintLine4 { get; set; }
        public string VoucherFinePrintLine5 { get; set; }
        public string VoucherLocationLine1 { get; set; }
        public string VoucherLocationLine2 { get; set; }
        public string VoucherLocationLine3 { get; set; }
        public string VoucherLocationLine4 { get; set; }
        public string VoucherLocationLine5 { get; set; }
        public string VoucherHowToRedeemLine1 { get; set; }
        public string VoucherHowToRedeemLine2 { get; set; }
        public string VoucherHowToRedeemLine3 { get; set; }
        public string VoucherHowToRedeemLine4 { get; set; }
        public string VoucherHowToRedeemLine5 { get; set; }
        public string VoucherRedemptionPhone { get; set; }
        public string VoucherLocationLAT { get; set; }
        public string VoucherLocationLON { get; set; }
        public Nullable<double> MaxDailyBudget { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias1 { get; set; }
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual ICollection<CampaignCategory> CampaignCategories { get; set; }
        public virtual ICollection<CouponCode> CouponCodes { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}