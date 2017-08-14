
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class SurveyQuestionRequestResponseModel
    {
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }

        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<CountryDropdown> CountryDropdowns { get; set; }
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<LanguageDropdown> LanguageDropdowns { get; set; }
        /// <summary>
        /// Total Count of  Profile Questions
        /// </summary>
        public int TotalCount { get; set; }
        public UserBaseData objBaseData { get; set; }
        public Nullable<double> setupPrice { get; set; }
        /// <summary>
        /// Industory
        /// </summary>
        public IEnumerable<Industry> Professions { get; set; }
        /// <summary>
        /// Education
        /// </summary>
        public IEnumerable<Education> Educations { get; set; }
    }
    public class SurveyQuestionEditorRequestResponseModel
    {
        public SurveyQuestion SurveyQuestion { get; set; }
    }
    public class SurveyQuestionParentListRequestResponseModel
    {
        public IEnumerable<SurveyQuestionDropDown> SurveyQuestion { get; set; }
    }
}