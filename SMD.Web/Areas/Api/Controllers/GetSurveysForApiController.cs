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
    /// Get Surveys for APIs Controller 
    /// </summary>
   // [Authorize]
    public class GetSurveysForApiController : ApiController
    {
        #region Public

        private readonly ISurveyQuestionService surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetSurveysForApiController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Ads for API
        /// </summary>
        public SurveyForApiSearchResponse Post(GetSurveysApiRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.GetSueveysForApi(request).CreateFrom();
        }
        #endregion
    }
}