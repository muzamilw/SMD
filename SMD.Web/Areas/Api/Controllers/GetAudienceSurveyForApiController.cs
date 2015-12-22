using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Audience Survey For Api Controller
    /// </summary>
    public class GetAudienceSurveyForApiController : ApiController
    {
        #region Public

        private readonly ISurveyQuestionService surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetAudienceSurveyForApiController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Survey Matching Count for API
        /// </summary>
        [ApiExceptionCustom]
        public AudienceSurveyForApiResponse Post(string authenticationToken,[FromUri] GetAudienceSurveyRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.GetAudienceSurveyCount(request).CreateFrom();
        }
        #endregion
    }
}