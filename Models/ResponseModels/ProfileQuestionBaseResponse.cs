using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Base Data model 
    /// </summary>
    public class ProfileQuestionBaseResponse
    {
        /// <summary>
        /// Profile Question Groups
        /// </summary>
        public IEnumerable<ProfileQuestionGroup> ProfileQuestionGroups { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<Country> Countries { get; set; }
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<Language> Languages { get; set; }
    }
}
