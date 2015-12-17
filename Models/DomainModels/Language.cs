
using SMD.Models.IdentityModels;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string NativeName { get; set; }

        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        public virtual ICollection<User> AspNetUsers { get; set; }
    }
}
