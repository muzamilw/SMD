using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Get Surveys| API Reposne | Domain
    /// </summary>
    public class SurveyForApiSearchResponse
    {
        /// <summary>
        /// List of Surveys
        /// </summary>
        public IEnumerable<GetSurveys_Result> Surveys { get; set; }
    }
}
