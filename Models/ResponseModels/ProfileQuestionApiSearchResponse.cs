using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Domain model | APi Reposne
    /// </summary>
    public class ProfileQuestionApiSearchResponse
    {
        /// <summary>
        /// List of Questions
        /// </summary>
        public IEnumerable<ProfileQuestion> ProfileQuestions { get; set; }

        /// <summary>
        /// Answered Questions's Percentage
        /// </summary>
        public double PercentageCompleted { get; set; }
    }
}
