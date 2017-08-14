
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

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }

        public virtual ICollection<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }
    }
}
