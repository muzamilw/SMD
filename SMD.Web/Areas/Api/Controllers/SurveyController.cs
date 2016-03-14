using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Survey APi Controller 
    /// </summary>
    public class SurveyController : ApiController
    {
        #region Public
        private readonly ISurveyQuestionService _surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SurveyController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public SurveyQuestionRequestResponseModel Get([FromUri] SurveySearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                if (request.FirstLoad == true)
                    return _surveyQuestionService.GetSurveyQuestions().CreateFrom();
                else
                    return _surveyQuestionService.GetSurveyQuestions(request).CreateFrom();
            }
           
        }


        /// <summary>
        /// Update Survey Questions
        /// </summary>
        public bool Post(SMD.Models.DomainModels.SurveyQuestion surveyModel)
        {
            if (surveyModel.SqId == 0)
                return _surveyQuestionService.Create(surveyModel);
            else
                return _surveyQuestionService.Update(surveyModel);
        }
        #endregion
    }
}