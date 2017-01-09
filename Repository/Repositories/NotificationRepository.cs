using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;
using System.Text.RegularExpressions;

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


        public IEnumerable<vw_Notifications> GetAllUnReadNotificationsByUserId(string UserId, string PhoneNumber)
        {
            PhoneNumber = Regex.Replace(PhoneNumber, @"\s+", "");
            PhoneNumber = PhoneNumber.Substring(PhoneNumber.Length - 9, PhoneNumber.Length - (PhoneNumber.Length - 9));

            return db.vw_Notifications.Where(c => (c.UserID == UserId || c.PhoneNumber.EndsWith(PhoneNumber)) && (c.IsRead == false || c.IsRead == null) );
        }

        public bool UserHasNotifications(string UserId)
        {
            var PhoneNumber = db.Users.Where(g => g.Id == UserId).SingleOrDefault().Phone1;
            PhoneNumber = Regex.Replace(PhoneNumber, @"\s+", "");
            PhoneNumber = PhoneNumber.Substring(PhoneNumber.Length - 9, PhoneNumber.Length - (PhoneNumber.Length - 9));


            if (DbSet.Where(c => (c.UserID == UserId || c.PhoneNumber.EndsWith(PhoneNumber) ) && (c.IsRead == false || c.IsRead == null)).Count() > 0)
                return true;
            else
                return false;
        }


        public Notification GetNotificationBySurveyQuestionShareId(long SurveyQuestionShareId)
        {
            return DbSet.Where(g => g.SurveyQuestionShareId == SurveyQuestionShareId).SingleOrDefault();
        }
        #endregion
    }
}
