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
    /// Get Audience Ad Campaign For Api Controller
    /// </summary>
    [Authorize]
    public class GetAudienceAdCampaignForApiController : ApiController
    {
        #region Public

        private readonly ISurveyQuestionService surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetAudienceAdCampaignForApiController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Ad Campaign Matching Count for API
        /// </summary>
        [ApiExceptionCustom]
        public AudienceAdCampaignForApiResponse Post(GetAudienceSurveyRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.GetAudienceAdCampaignCount(request).CreateCountFrom();
        }
        #endregion
    }
}