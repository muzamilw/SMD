using System;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Profile Question User Answer Domain Model
    /// </summary>
    public class ProfileQuestionUserAnswer
    {
        public long PquAnswerId { get; set; }
        public int PqId { get; set; }
        public string UserId { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int PqAnswerId { get; set; }

        public virtual User User { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ProfileQuestionAnswer ProfileQuestionAnswer { get; set; }
    }
}
