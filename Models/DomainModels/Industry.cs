using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Industry
    {
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual ICollection<User> AspNetUsers { get; set; }
        public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
    }
}
