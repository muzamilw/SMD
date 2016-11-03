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
    public class SurveySharingGroupMemberRepository : BaseRepository<SurveySharingGroupMember>, ISurveySharingGroupMemberRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SurveySharingGroupMemberRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveySharingGroupMember> DbSet
        {
            get { return db.SurveySharingGroupMember; }
        }
        #endregion

        
        #region Public


        public SurveySharingGroupMember Find(int id)
        {
            return DbSet.Where(c => c.SharingGroupMemberId == id).FirstOrDefault();
        }

        #endregion
    }
}
