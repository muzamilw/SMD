using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Api Model 
    /// </summary>
    public class ProfileQuestionApiSearchResponse
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