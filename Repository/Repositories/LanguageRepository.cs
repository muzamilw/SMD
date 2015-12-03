using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Language Repository 
    /// </summary>
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        #region Private
      
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public LanguageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Language> DbSet
        {
            get { return db.Languages; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find Language by Id 
        /// </summary>
        public Language Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get List of Language 
        /// </summary>
        public IEnumerable<Language> GetAllLanguages()
        {
            return DbSet.Select(lang => lang).ToList();
        }
        #endregion
    }
}
