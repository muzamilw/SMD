using SMD.Models.Common;
using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Profile question search request response model 
    /// </summary>
    public class ProfileQuestionSearchRequestResponse
    {
        #region Public
        /// <summary>
        ///  Profile Questions List
        /// </summary>
        public IEnumerable<ProfileQuestion> ProfileQuestions { get; set; }
        public UserBaseData objBaseData { get; set; }
        public IEnumerable<Industry> Professions { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<Language> Languages { get; set; }
        /// <summary>

        public IEnumerable<Industry> Industry { get; set; }
        /// <summary>
        /// Education
        /// </summary>
        /// 
        public IEnumerable<Education> Education { get; set; }
        /// <summary>
        /// Total Count of  Profile Questions
        /// </summary>
        /// 
        public int TotalCount { get; set; }
        #endregion
    }
}
