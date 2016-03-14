
using System.Collections.Generic;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Request Model For API
    /// </summary>
    public class UpdateProfileQuestionUserAnswerApiRequest
    {
        /// <summary>
        /// Question Id 
        /// </summary>
        public int ProfileQuestionId { get; set; }

        /// <summary>
        /// Selected Answer list of Answers IDs
        /// </summary>
        public List<int> ProfileQuestionAnswerIds { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        public int companyId { get; set; }
    }
}
