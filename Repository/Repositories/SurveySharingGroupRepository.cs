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


        #endregion
    }
}
