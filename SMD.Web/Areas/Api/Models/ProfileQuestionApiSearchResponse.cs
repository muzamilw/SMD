using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Api Model 
    /// </summary>
    public class ProfileQuestionApiSearchResponse : BaseApiResponse
    {
        /// <summary>
        /// Profile Questions List 
        /// </summary>
        public IEnumerable<ProfileQuestionApiModel> ProfileQuestionApiModels { get; set; }

        /// <summary>
        /// Answered Questions's Percentage
        /// </summary>
        public double PercentageCompleted { get; set; }
    }
}