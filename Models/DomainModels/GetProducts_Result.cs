using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Get Products Result Domain Model
    /// </summary>
// ReSharper disable InconsistentNaming
    public class GetProducts_Result
// ReSharper restore InconsistentNaming
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
        public string AdvertisersLogoPath { get; set; }
        public string SqLeftImagePath { get; set; }
        public string SqRightImagePath { get; set; }
        public string GameUrl { get; set; }
        public int? PqAnswer1Id { get; set; }
        public string PqAnswer1 { get; set; }
        public int? PqAnswer2Id { get; set; }
        public string PqAnswer2 { get; set; }
        public int? PqAnswer3Id { get; set; }
        public string PqAnswer3 { get; set; }
        public int? PqAnswer4Id { get; set; }
        public string PqAnswer4 { get; set; }
        public int? PqAnswer5Id { get; set; }
        public string PqAnswer5 { get; set; }
        public int? PqAnswer6Id { get; set; }
        public string PqAnswer6 { get; set; }
        public long? Weightage { get; set; }
        public int? TotalItems { get; set; }
        public double? SqLeftImagePercentage { get; set; }
        public double? SqRightImagePercentage { get; set; }

        /// <summary>
        /// Profile Question Answers
        /// </summary>
        [NotMapped]
        public ICollection<ProfileQuestionAnswer> PqAnswers {
            get
            {
                var answers = new List<ProfileQuestionAnswer>();
                if (Type != "Question" || !ItemId.HasValue)
                {
                    return answers;
                }

                if (PqAnswer1Id.HasValue)
                {
                   answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer1Id.Value, AnswerString = PqAnswer1, PqId = (int)ItemId }); 
                }
                if (PqAnswer2Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer2Id.Value, AnswerString = PqAnswer2, PqId = (int)ItemId });
                }
                if (PqAnswer3Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer3Id.Value, AnswerString = PqAnswer3, PqId = (int)ItemId });
                }
                if (PqAnswer4Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer4Id.Value, AnswerString = PqAnswer4, PqId = (int)ItemId });
                }
                if (PqAnswer5Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer5Id.Value, AnswerString = PqAnswer5, PqId = (int)ItemId });
                }
                if (PqAnswer6Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer { PqAnswerId = PqAnswer6Id.Value, AnswerString = PqAnswer6, PqId = (int)ItemId });
                }

                return answers;
            } 
        }
    }
}
