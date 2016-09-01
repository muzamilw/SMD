using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Model 
    /// </summary>
    public class ProfileQuestionBaseResponse
    {
        /// <summary>
        /// Profile Question Groups
        /// </summary>
        public IEnumerable<ProfileQuestionGroupDropdown> ProfileQuestionGroupDropdowns { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<CountryDropdown> CountryDropdowns { get; set; }
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<LanguageDropdown> LanguageDropdowns { get; set; }
        public UserBaseData objBaseData { get; set; }
        /// <summary>
        /// Profile Questions for showing in editor - linked question
        /// </summary>
        public IEnumerable<ProfileQuestionDropdown> ProfileQuestionDropdowns { get; set; }
        
    }
}