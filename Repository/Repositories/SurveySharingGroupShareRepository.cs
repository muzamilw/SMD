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
    public class SurveySharingGroupShareRepository : BaseRepository<SurveySharingGroupShare>, ISurveySharingGroupShareRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SurveySharingGroupShareRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveySharingGroupShare> DbSet
        {
            get { return db.SurveySharingGroupShare; }
        }
        #endregion

        
        #region Public


        public SurveySharingGroupShare Find(long id)
        {
            return DbSet.Where(c => c.SurveyQuestionShareId == id).FirstOrDefault();
        }

        
        #endregion
    }
}
