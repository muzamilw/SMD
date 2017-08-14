using System;
using System.Collections.Generic;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Profile Question User Answer Domain Model
    /// </summary>
    public class ProfileQuestionUserAnswer
    {
        public long PQUAnswerID { get; set; }
        public int PQID { get; set; }
        public string UserID { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int? PQAnswerID { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> ResponseType { get; set; }
        public virtual User AspNetUser { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ProfileQuestionAnswer ProfileQuestionAnswer { get; set; }
        public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual Company Company { get; set; }
    }
}
