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
    /// Tax Repository 
    /// </summary>
    public class UserGameResponseRepository : BaseRepository<UserGameResponse>, IUserGameResponseRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public UserGameResponseRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserGameResponse> DbSet
        {
            get { return db.UserGameResponse; }
        }
        #endregion
        #region Public

      
        #endregion
    }
}
