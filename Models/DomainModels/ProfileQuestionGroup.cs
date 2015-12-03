using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Profile Question Group Domain Model
    /// </summary>
    public class ProfileQuestionGroup
    {
        public int ProfileGroupId { get; set; }
        public string ProfileGroupName { get; set; }
        public string ImagePath { get; set; }
        public int? LangaugeId { get; set; }
        public int? CountryId { get; set; }

        public virtual ICollection<ProfileQuestion> ProfileQuestions { get; set; }
        public virtual Country Country { get; set; }
    }
}
