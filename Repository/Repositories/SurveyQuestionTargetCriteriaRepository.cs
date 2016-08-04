using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class SurveyQuestionTargetCriteriaRepository : BaseRepository<SurveyQuestionTargetCriteria>, ISurveyQuestionTargetCriteriaRepository
    {

        public SurveyQuestionTargetCriteriaRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveyQuestionTargetCriteria> DbSet
        {
            get
            {
                return db.SurveyQuestionTargetCriterias;
            }
        }
    }
}
