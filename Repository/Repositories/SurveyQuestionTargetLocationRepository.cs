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
    public class SurveyQuestionTargetLocationRepository : BaseRepository<SurveyQuestionTargetLocation>, ISurveyQuestionTargetLocationRepository
    {

        public SurveyQuestionTargetLocationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveyQuestionTargetLocation> DbSet
        {
            get
            {
                return db.SurveyQuestionTargetLocations;
            }
        }
    }
}
