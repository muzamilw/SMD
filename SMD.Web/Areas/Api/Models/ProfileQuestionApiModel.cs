
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Profile Question Model For APi 
    /// </summary>
    public class ProfileQuestionApiModel
    {
        public int PqId { get; set; }
        public int? QuestionType { get; set; }
        public string Question { get; set; }
        public string ProfileGroupName { get; set; }
        /// <summary>
        /// List of Answers
        /// </summary>
        public IEnumerable<ProfileQuestionAnswerApiModel> ProfileQuestionAnswers { get; set; } 
    }
}