using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

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
        public long Post(GetAudienceSurveyRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.GetAudienceAdCampaignCount(request);
        }
        #endregion
    }
}