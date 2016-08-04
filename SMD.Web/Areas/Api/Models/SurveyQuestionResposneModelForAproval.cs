
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model 
    /// </summary>
    public class SurveyQuestionResposneModelForAproval
    {
        #region Public
        /// <summary>
        ///  Survey Questions List
        /// </summary>
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }

        /// <summary>
        /// Total Count of  Survey Questions
        /// </summary>
        public int TotalCount { get; set; }
        #endregion
    }
}