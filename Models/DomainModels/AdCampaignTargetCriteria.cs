using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaignTargetCriteria Domain Model
    /// </summary>
    public class AdCampaignTargetCriteria
    {
        public long CriteriaId { get; set; }
        public long? CampaignId { get; set; }
        public int? Type { get; set; }
        public int? PqId { get; set; }
        public int? PqAnswerId { get; set; }
        public long? SqId { get; set; }
        public int? SqAnswer { get; set; }
        public bool? IncludeorExclude { get; set; }
        public int? LanguageId { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ProfileQuestionAnswer ProfileQuestionAnswer { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
