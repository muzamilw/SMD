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
    /// Country Repository 
    /// </summary>
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public NotificationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Notification> DbSet
        {
            get { return db.Notification; }
        }
        #endregion

        
        #region Public


        public Notification Find(int id)
        {
            return DbSet.Where(c => c.ID == id).FirstOrDefault();
        }


        public IEnumerable<Notification> GetAllUnReadNotificationsByUserId(string UserId)
        {
            return DbSet.Where(c => c.UserID == UserId && c.IsRead == false);
        }

        #endregion
    }
}
