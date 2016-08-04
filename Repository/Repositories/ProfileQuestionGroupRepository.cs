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
    /// Profile Question Group Repository 
    /// </summary>
    public class ProfileQuestionGroupRepository : BaseRepository<ProfileQuestionGroup>, IProfileQuestionGroupRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ProfileQuestionGroupRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestionGroup> DbSet
        {
            get { return db.ProfileQuestionGroups; }
        }
        #endregion
        #region Public
        public ProfileQuestionGroup Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Profile Question Groups 
        /// </summary>
        public IEnumerable<ProfileQuestionGroup> GetAllProfileQuestionGroups()
        {
            return DbSet.Select(question => question).ToList();
        }
        #endregion
    }
}
