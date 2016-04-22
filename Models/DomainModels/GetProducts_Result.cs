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
        public int? PqA1LinkedQ1 { get; set; }
        public int? PqA1LinkedQ2 { get; set; }
        public int? PqA1Type { get; set; }
        public int? PqA1SortOrder { get; set; }
        public string PqA1ImagePath { get; set; }
        public int? PqAnswer2Id { get; set; }
        public string PqAnswer2 { get; set; }
        public int? PqA2LinkedQ1 { get; set; }
        public int? PqA2LinkedQ2 { get; set; }
        public int? PqA2Type { get; set; }
        public int? PqA2SortOrder { get; set; }
        public string PqA2ImagePath { get; set; }
        public int? PqAnswer3Id { get; set; }
        public string PqAnswer3 { get; set; }
        public int? PqA3LinkedQ1 { get; set; }
        public int? PqA3LinkedQ2 { get; set; }
        public int? PqA3Type { get; set; }
        public int? PqA3SortOrder { get; set; }
        public string PqA3ImagePath { get; set; }
        public int? PqAnswer4Id { get; set; }
        public string PqAnswer4 { get; set; }
        public int? PqA4LinkedQ1 { get; set; }
        public int? PqA4LinkedQ2 { get; set; }
        public int? PqA4Type { get; set; }
        public int? PqA4SortOrder { get; set; }
        public string PqA4ImagePath { get; set; }
        public int? PqAnswer5Id { get; set; }
        public string PqAnswer5 { get; set; }
        public int? PqA5LinkedQ1 { get; set; }
        public int? PqA5LinkedQ2 { get; set; }
        public int? PqA5Type { get; set; }
        public int? PqA5SortOrder { get; set; }
        public string PqA5ImagePath { get; set; }
        public int? PqAnswer6Id { get; set; }
        public string PqAnswer6 { get; set; }
        public int? PqA6LinkedQ1 { get; set; }
        public int? PqA6LinkedQ2 { get; set; }
        public int? PqA6Type { get; set; }
        public int? PqA6SortOrder { get; set; }
        public string PqA6ImagePath { get; set; }
        public long? Weightage { get; set; }
        public int? TotalItems { get; set; }
        public double? SqLeftImagePercentage { get; set; }
        public double? SqRightImagePercentage { get; set; }


        public string LandingPageUrl { get; set; }
        public string BuyItLine1 { get; set; }
        public string BuyItLine2 { get; set; }
        public string BuyItLine3 { get; set; }
        public string BuyItButtonText { get; set; }
        public string BuyItImageUrl { get; set; }

        public string VoucherImagePath { get; set; }

        public int? PQA1LinkedQ3 { get; set; }
        public int? PQA1LinkedQ4 { get; set; }
        public int? PQA1LinkedQ5 { get; set; }
        public int? PQA1LinkedQ6 { get; set; }
        public int? PQA2LinkedQ3 { get; set; }
        public int? PQA2LinkedQ4 { get; set; }
        public int? PQA2LinkedQ5 { get; set; }
        public int? PQA2LinkedQ6 { get; set; }
        public int? PQA3LinkedQ3 { get; set; }
        public int? PQA3LinkedQ4 { get; set; }
        public int? PQA3LinkedQ5 { get; set; }
        public int? PQA3LinkedQ6 { get; set; }
        public int? PQA4LinkedQ3 { get; set; }
        public int? PQA4LinkedQ4 { get; set; }
        public int? PQA4LinkedQ5 { get; set; }
        public int? PQA4LinkedQ6 { get; set; }
        public int? PQA5LinkedQ3 { get; set; }
        public int? PQA5LinkedQ4 { get; set; }
        public int? PQA5LinkedQ5 { get; set; }
        public int? PQA5LinkedQ6 { get; set; }
        public int? PQA6LinkedQ3 { get; set; }
        public int? PQA6LinkedQ4 { get; set; }
        public int? PQA6LinkedQ5 { get; set; }
        public int? PQA6LinkedQ6 { get; set; }
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
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer1Id.Value,
                        AnswerString = PqAnswer1,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA1LinkedQ1,
                        LinkedQuestion2Id = PqA1LinkedQ2,
                        LinkedQuestion3Id = PQA1LinkedQ3,
                        LinkedQuestion4Id = PQA1LinkedQ4,
                        LinkedQuestion5Id = PQA1LinkedQ5,
                        LinkedQuestion6Id = PQA1LinkedQ6,
                        Type = PqA1Type,
                        SortOrder = PqA1SortOrder,
                        ImagePath = PqA1ImagePath
                    }); 
                }
                if (PqAnswer2Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer2Id.Value,
                        AnswerString = PqAnswer2,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA2LinkedQ1,
                        LinkedQuestion2Id = PqA2LinkedQ2,
                        LinkedQuestion3Id = PQA2LinkedQ3,
                        LinkedQuestion4Id = PQA2LinkedQ4,
                        LinkedQuestion5Id = PQA2LinkedQ5,
                        LinkedQuestion6Id = PQA2LinkedQ6,
                        Type = PqA2Type,
                        SortOrder = PqA2SortOrder,
                        ImagePath = PqA2ImagePath
                    });
                }
                if (PqAnswer3Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer3Id.Value,
                        AnswerString = PqAnswer3,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA3LinkedQ1,
                        LinkedQuestion2Id = PqA3LinkedQ2,
                        LinkedQuestion3Id = PQA3LinkedQ3,
                        LinkedQuestion4Id = PQA3LinkedQ4,
                        LinkedQuestion5Id = PQA3LinkedQ5,
                        LinkedQuestion6Id = PQA3LinkedQ6,
                        Type = PqA3Type,
                        SortOrder = PqA3SortOrder,
                        ImagePath = PqA3ImagePath
                    });
                }
                if (PqAnswer4Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer4Id.Value,
                        AnswerString = PqAnswer4,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA4LinkedQ1,
                        LinkedQuestion2Id = PqA4LinkedQ2,
                        LinkedQuestion3Id = PQA4LinkedQ3,
                        LinkedQuestion4Id = PQA4LinkedQ4,
                        LinkedQuestion5Id = PQA4LinkedQ5,
                        LinkedQuestion6Id = PQA4LinkedQ6,
                        Type = PqA4Type,
                        SortOrder = PqA4SortOrder,
                        ImagePath = PqA4ImagePath
                    });
                }
                if (PqAnswer5Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer5Id.Value,
                        AnswerString = PqAnswer5,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA5LinkedQ1,
                        LinkedQuestion2Id = PqA5LinkedQ2,
                        LinkedQuestion3Id = PQA5LinkedQ3,
                        LinkedQuestion4Id = PQA5LinkedQ4,
                        LinkedQuestion5Id = PQA5LinkedQ5,
                        LinkedQuestion6Id = PQA5LinkedQ6,
                        Type = PqA5Type,
                        SortOrder = PqA5SortOrder,
                        ImagePath = PqA5ImagePath
                    });
                }
                if (PqAnswer6Id.HasValue)
                {
                    answers.Add(new ProfileQuestionAnswer
                    {
                        PqAnswerId = PqAnswer6Id.Value,
                        AnswerString = PqAnswer6,
                        PqId = (int)ItemId,
                        LinkedQuestion1Id = PqA6LinkedQ1,
                        LinkedQuestion2Id = PqA6LinkedQ2,
                        LinkedQuestion3Id = PQA6LinkedQ3,
                        LinkedQuestion4Id = PQA6LinkedQ4,
                        LinkedQuestion5Id = PQA6LinkedQ5,
                        LinkedQuestion6Id = PQA6LinkedQ6,
                        Type = PqA6Type,
                        SortOrder = PqA6SortOrder,
                        ImagePath = PqA6ImagePath
                    });
                }

                return answers;
            } 
        }
    }
}
