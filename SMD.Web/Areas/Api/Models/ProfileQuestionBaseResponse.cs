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
    }
}