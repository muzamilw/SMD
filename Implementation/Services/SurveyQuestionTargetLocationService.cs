using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class SurveyQuestionTargetLocationService : ISurveyQuestionTargetLocationService
    {
        
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveyQuestionTargetLocationRepository sqtlRepository;
       

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionTargetLocationService(ISurveyQuestionTargetLocationRepository _sqtlRepository)
        {
            this.sqtlRepository = _sqtlRepository;
            
        }

        #endregion
    }
}
