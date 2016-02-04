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
        public List<ProfileQuestionAnswerDropdown> PqAnswers { get; set; }
    }
}