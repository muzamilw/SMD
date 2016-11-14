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
    public class SurveySharingGroupRepository : BaseRepository<SurveySharingGroup>, ISurveySharingGroupRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SurveySharingGroupRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveySharingGroup> DbSet
        {
            get { return db.SurveySharingGroup; }
        }
        #endregion

        
        #region Public


        public SurveySharingGroup Find(long id)
        {
            return DbSet.Where(c => c.SharingGroupId == id).FirstOrDefault();
        }



        public IEnumerable< SurveySharingGroup> GetUserGroups(string UserId)
        {
            return DbSet.Where(c => c.UserId == UserId);
        }



        public bool DeleteUserGroup(long SharingGroupId)
        {
            var group = this.Find(SharingGroupId);


            //deleting notifications
            db.Database.ExecuteSqlCommand("delete n from Notifications n inner join SurveySharingGroupShares s on n.SurveyQuestionShareId = s.SurveyQuestionShareId where s.SharingGroupId=" + SharingGroupId.ToString());

            //deleting shares
            db.Database.ExecuteSqlCommand("delete from SurveySharingGroupShares where SharingGroupId=" + SharingGroupId.ToString());

            //deleting members
            db.Database.ExecuteSqlCommand("delete from SurveySharingGroupMembers where SharingGroupId=" + SharingGroupId.ToString());

            //deleting the questions
            db.Database.ExecuteSqlCommand("delete from SharedSurveyQuestion where SharingGroupId=" + SharingGroupId.ToString());

            if (group != null)
            {
                this.Delete(group);
                this.SaveChanges();
                return true;
            }
            else
                return false;
        }


        #endregion
    }
}
