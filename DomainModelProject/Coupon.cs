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
    
    public partial class Coupon
    {
        public Coupon()
        {
            this.CouponCategories = new HashSet<CouponCategories>();
            this.UserPurchasedCoupons = new HashSet<UserPurchasedCoupon>();
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
            this.CouponPriceOptions = new HashSet<CouponPriceOption>();
            this.CampaignEventHistories = new HashSet<CampaignEventHistory>();
            this.CouponRatingReviews = new HashSet<CouponRatingReview>();
        }
    
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
        public Nullable<int> LocationCountryId { get; set; }
        public Nullable<System.DateTime> CouponStartDate { get; set; }
        public Nullable<System.DateTime> CouponEndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> ShowBuyitBtn { get; set; }
        public string BuyitLandingPageUrl { get; set; }
        public string BuyitBtnLabel { get; set; }
        public string YoutubeLink { get; set; }
        public string CouponImage4 { get; set; }
        public string CouponImage5 { get; set; }
        public string CouponImage6 { get; set; }
        public Nullable<bool> IsPaymentCollected { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<bool> IsShowReviews { get; set; }
        public Nullable<bool> IsShowAddress { get; set; }
        public Nullable<bool> IsShowPhoneNo { get; set; }
        public Nullable<bool> IsShowMap { get; set; }
        public Nullable<bool> IsShowyouTube { get; set; }
        public Nullable<bool> IsShowAboutUs { get; set; }
        public Nullable<bool> IsMarketingStories { get; set; }
    
        public virtual CompanyBranch CompanyBranch { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<CouponCategories> CouponCategories { get; set; }
        public virtual ICollection<UserPurchasedCoupon> UserPurchasedCoupons { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<CouponPriceOption> CouponPriceOptions { get; set; }
        public virtual ICollection<CampaignEventHistory> CampaignEventHistories { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CouponRatingReview> CouponRatingReviews { get; set; }
    }
}
