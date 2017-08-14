using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Survey Question Response Repository
    /// </summary>
    public class SurveyQuestionResponseRepository : BaseRepository<SurveyQuestionResponse>, ISurveyQuestionResponseRepository
    {

        #region Private

        #endregion

        #region Constructor

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveyQuestionResponse> DbSet
        {
            get
            {
                return db.SurveyQuestionResponses;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SurveyQuestionResponseRepository(IUnityContainer container)
            : base(container)
        {

        }

        #endregion
        
        #region Public

        #endregion

        /// <summary>
        /// Get Response by user Id
        /// </summary>
        public SurveyQuestionResponse GetByUserId(long sqId, string userId)
        {
            return DbSet.FirstOrDefault(sqr => sqr.SqId == sqId && sqr.UserId == userId);
        }
    }
}
