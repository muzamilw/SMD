using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Language Repository Interface 
    /// </summary>
    public interface ILanguageRepository : IBaseRepository<Language, int>
    {
        /// <summary>
        /// Get List of Language 
        /// </summary>
        IEnumerable<Language> GetAllLanguages();

        IEnumerable<Language> GetSearchedLanguages(string searchString);
    }
}
