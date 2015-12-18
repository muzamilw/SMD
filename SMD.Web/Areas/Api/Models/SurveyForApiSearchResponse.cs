using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Search Resposne | API 
    /// </summary>
    public class SurveyForApiSearchResponse
    {
        /// <summary>
        /// List of Surveys
        /// </summary>
        public IEnumerable<SurveyApiModel> Surveys { get; set; }
    }
}