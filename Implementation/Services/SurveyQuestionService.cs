using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMD.Implementation.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
          #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveyQuestionRepository surveyQuestionRepository;
       

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionService(ISurveyQuestionRepository _surveyQuestionRepository)
        {
            this.surveyQuestionRepository = _surveyQuestionRepository;
            
        }

        #endregion
    }
}
