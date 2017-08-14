using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class SurveyQuestionTargetCriteriaService : ISurveyQuestionTargetCriteriaService
    {
          #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveyQuestionTargetCriteriaRepository sqtcRepository;
       

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionTargetCriteriaService(ISurveyQuestionTargetCriteriaRepository _sqtcRepository)
        {
            this.sqtcRepository = _sqtcRepository;
            
        }

        #endregion
    }
}
