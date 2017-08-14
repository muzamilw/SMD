using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Search Resposne | API 
    /// </summary>
    public class SurveyForApiSearchResponse : BaseApiResponse
    {
        /// <summary>
        /// List of Surveys
        /// </summary>
        public IEnumerable<SurveyApiModel> Surveys { get; set; }
    }
}