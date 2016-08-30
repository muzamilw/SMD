using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Profile Question Answer Domain Model
    /// </summary>
    public class ProfileQuestionAnswer
    {
        public int PqAnswerId { get; set; }
        public int? PqId { get; set; }
        public int? Type { get; set; }
        public string AnswerString { get; set; }
        public string ImagePath { get; set; }
        public int? SortOrder { get; set; }
        public int? LinkedQuestion1Id { get; set; }
        public int? LinkedQuestion2Id { get; set; }
        public int? LinkedQuestion3Id { get; set; }
        public int? LinkedQuestion4Id { get; set; }
        public int? LinkedQuestion5Id { get; set; }
        public int? LinkedQuestion6Id { get; set; }

        public int? Status { get; set; }

        [NotMapped]
        public byte[] ImageUrlBytes
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = ImagePath.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (ImagePath.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = ImagePath.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }

      
        public virtual ICollection<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }
    }
}
