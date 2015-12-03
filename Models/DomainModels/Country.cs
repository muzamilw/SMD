using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<ProfileQuestionGroup> ProfileQuestionGroups { get; set; }
    }
}
