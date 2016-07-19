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
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public Nullable<int> CouponTakenCount { get; set; }
        public Nullable<int> CouponQuantity { get; set; }
        public int? priority { get; set; }
        public string couponSmdComission { get; set; }
        public string couponImage2 { get; set; }
        public string CouponImage3 { get; set; }
        public string CouponImage4 { get; set; }
        public string CouponExpiryLabel { get; set; }
        public double? CouponDiscountValue { get; set; }
        public Nullable<int> CouponType { get; set; }
        public virtual Company Company { get; set; }
        public virtual Language Language { get; set; }
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





        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<CouponCategory> CouponCategories { get; set; }
        public virtual ICollection<CouponCode> CouponCodes { get; set; }
        public virtual ICollection<CampaignCategory> CampaignCategories { get; set; }
        [NotMapped]
        public string CampaignImagePath { get; set; }
        [NotMapped]
        public string CampaignTypeImagePath { get; set; }
        [NotMapped]
        public string VoucherImagePath { get; set; }
        [NotMapped]
        public string buyItImageBytes { get; set; }
        [NotMapped]
        public string LogoImageBytes { get; set; }

        [NotMapped]
        public int AdViews { get; set; }


        /// <summary>
        /// Makes a copy of Campaign
        /// </summary>
        public void Clone(AdCampaign target)
        {
      
            target.LanguageId = LanguageId;
            target.UserId = UserId;
            target.SmdCampaign = SmdCampaign;

            target.CampaignName = CampaignName + "- Copy";
            target.CampaignDescription = CampaignDescription;
            target.Status = Status;


            target.Archived = Archived;
            target.Approved = Approved;
            target.ApprovedBy = ApprovedBy;

            target.ApprovalDateTime = ApprovalDateTime;
            target.StartDateTime = StartDateTime;
            target.EndDateTime = EndDateTime;

            target.MaxBudget = MaxBudget;
            target.Type = Type;
            target.ClickRate = ClickRate;

            target.SmdCredits = SmdCredits;
            target.DisplayTitle = DisplayTitle;
            target.Description = Description;

            target.ImagePath = ImagePath;
            target.LandingPageVideoLink = LandingPageVideoLink;
            target.VerifyQuestion = VerifyQuestion;

            target.Answer1 = Answer1;
            target.Answer2 = Answer2;
            target.Answer3 = Answer3;
            target.CorrectAnswer = CorrectAnswer;

            target.CreatedDateTime = CreatedDateTime;
            target.CreatedBy = CreatedBy;
            target.ModifiedDateTime = ModifiedDateTime;


            target.ModifiedBy = ModifiedBy;
            target.AgeRangeStart = AgeRangeStart;
            target.AgeRangeEnd = AgeRangeEnd;

            target.Gender = Gender;
            target.RejectedReason = RejectedReason;
            target.ProjectedReach = ProjectedReach;

            target.ResultClicks = ResultClicks;
            target.AmountSpent = AmountSpent;
            target.RewardType = RewardType;

            target.Voucher1Heading = Voucher1Heading;
            target.Voucher1Description = Voucher1Description;
            target.Voucher1Value = Voucher1Value;
           

            target.Voucher1ImagePath = Voucher1ImagePath;
            target.Voucher2Heading = Voucher2Heading;
            target.Voucher2Description = Voucher2Description;

            target.Voucher2Value = Voucher2Value;
            target.Voucher2ImagePath = Voucher2ImagePath;
            target.VideoUrl = VideoUrl;

            target.BuyItLine2 = BuyItLine2;
            target.BuyItLine3 = BuyItLine3;


            target.BuyItButtonLabel = BuyItButtonLabel;
            target.BuyItImageUrl = BuyItImageUrl;
            target.CompanyId = CompanyId;

            target.CouponSwapValue = CouponSwapValue;
            target.CouponActualValue = CouponActualValue;
            target.CouponTakenCount = CouponTakenCount;
            target.CouponQuantity = CouponQuantity;
            target.priority = priority;
            target.CouponDiscountValue = CouponDiscountValue;
            target.IsUseFilter = IsUseFilter;
            target.CouponType = CouponType;
            target.DeliveryDays = DeliveryDays;
            target.VoucherAdditionalInfo = VoucherAdditionalInfo;
              
        }
    }
}
