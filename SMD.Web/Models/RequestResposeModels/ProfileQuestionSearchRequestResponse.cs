using SMD.MIS.Models.WebModels;
using System.Collections.Generic;

namespace SMD.MIS.Models.RequestResposeModels
{
    /// <summary>
    /// Search Request Web Response Model 
    /// </summary>
    public class ProfileQuestionSearchRequestResponse
    {
        #region Public
        /// <summary>
        ///  Profile Questions List
        /// </summary>
        public IEnumerable<ProfileQuestion> ProfileQuestions { get; set; }

        /// <summary>
        /// Total Count of  Profile Questions
        /// </summary>
        public int TotalCount { get; set; }
        #endregion
    }
}