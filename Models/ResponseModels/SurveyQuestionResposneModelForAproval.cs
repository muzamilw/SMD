using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Search request resposne for rejected questions
    /// </summary>
    public class SurveyQuestionResposneModelForAproval
    {
        /// <summary>
        ///  Rejected Survey Questions List
        /// </summary>
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }

        /// <summary>
        /// Total Count of  Survey Questions
        /// </summary>
        public int TotalCount { get; set; }
    }
}
