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
    public class SharedSurveyQuestionRepository : BaseRepository<SharedSurveyQuestion>, ISharedSurveyQuestionRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SharedSurveyQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SharedSurveyQuestion> DbSet
        {
            get { return db.SharedSurveyQuestion; }
        }
        #endregion

        
        #region Public


        public SharedSurveyQuestion Find(long id)
        {
            return DbSet.Where(c => c.SSQID == id).FirstOrDefault();
        }

        #endregion
    }
}
