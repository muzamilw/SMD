using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Profile Question Answer Repository 
    /// </summary>
    public class ProfileQuestionAnswerRepository : BaseRepository<ProfileQuestionAnswer>, IProfileQuestionAnswerRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ProfileQuestionAnswerRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestionAnswer> DbSet
        {
            get { return db.ProfileQuestionAnswers; }
        }
        #endregion
        #region Public
        public ProfileQuestionAnswer Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Answer by Profile Question Id 
        /// </summary>
        public IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerByQuestionId(long profileQuestionId)
        {
           return DbSet.Where(ans => ans.PqId == profileQuestionId).ToList();
        }
        #endregion
    }
}
