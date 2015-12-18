using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
  
    public class SurveyQuestionResponseModel
    {
        #region Public
        /// <summary>
        ///  Profile Questions List
        /// </summary>
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<Country> Countries { get; set; }
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<Language> Languages { get; set; }
        /// <summary>
        /// Total Count of  Profile Questions
        /// </summary>
        public int TotalCount { get; set; }
        #endregion
    }
    public class SurveyQuestionEditResponseModel
    {
        public SurveyQuestion SurveyQuestionObj { get; set; }
    }
}
