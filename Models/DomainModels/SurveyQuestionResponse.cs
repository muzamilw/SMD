using System;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Survey Question Response Domain Model
    /// </summary>
    public class SurveyQuestionResponse
    {
        public long SqResponseId { get; set; }
        public long? SqId { get; set; }
        public string UserId { get; set; }
        public DateTime? ResoponseDateTime { get; set; }
        public int? UserSelection { get; set; }

        public virtual User User { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
