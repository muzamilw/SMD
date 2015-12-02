using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Profile Question Repository 
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
       

        #endregion
    }
}
