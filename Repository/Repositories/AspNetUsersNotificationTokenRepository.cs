using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using SMD.Interfaces.Repository;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Tax Repository 
    /// </summary>
    public class AspNetUsersNotificationTokenRepository : BaseRepository<AspNetUsersNotificationToken>, IAspNetUsersNotificationTokenRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public AspNetUsersNotificationTokenRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUsersNotificationToken> DbSet
        {
            get { return db.AspNetUsersNotificationToken; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Get Tax By Country 
        /// </summary>
        public List<AspNetUsersNotificationToken> NotificationTokensByUserId(string UserId)
        {
           return DbSet.Where(g => g.UserId == UserId).ToList();
        }
        #endregion
    }
}
