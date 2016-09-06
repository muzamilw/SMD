using System;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Combination of Ads, Surveys, Questions
    /// </summary>
    public class ProductView
    {
        public long? ItemId { get; set; }
        public string ItemName { get; set; }
        public string Type { get; set; }
        public int? ItemType { get; set; }
        public string Description { get; set; }
        public double? AdClickRate { get; set; }
        public string AdImagePath { get; set; }
        public string AdVideoLink { get; set; }
        public string AdAnswer1 { get; set; }
        public string AdAnswer2 { get; set; }
        public string AdAnswer3 { get; set; }
        public int? AdCorrectAnswer { get; set; }
        public string AdVerifyQuestion { get; set; }
        public int? AdRewardType { get; set; }
        public string AdVoucher1Heading { get; set; }
        public string AdVoucher1Description { get; set; }
        public string AdVoucher1Value { get; set; }
        public string SqLeftImagePath { get; set; }
        public string SqRightImagePath { get; set; }
        public string GameUrl { get; set; }
        public double? SqLeftImagePercentage { get; set; }
        public double? SqRightImagePercentage { get; set; }
        public string AdvertisersLogoPath { get; set; }
        public List<ProfileQuestionAnswerView> PqAnswers { get; set; }
        public string LandingPageUrl { get; set; }
        public string BuyItLine1 { get; set; }
        public string BuyItLine2 { get; set; }
        public string BuyItLine3 { get; set; }
        public string BuyItButtonText { get; set; }
        public string BuyItImageUrl { get; set; }
        public string VoucherImagePath { get; set; }
        public string VideoLink2 { get; set; }
        public bool IsShowVoucherSetting { get; set; }


        //this contains the vouchers associated with the author of Ads.
        public int VouchersCount { get; set; }
        public int CompanyId { get; set; }

        public long GameId { get; set; }

        public Nullable<long> FreeCouponID { get; set; }

        public string SocialHandle { get; set; }
        public string SocialHandleType { get; set; }
    }
}