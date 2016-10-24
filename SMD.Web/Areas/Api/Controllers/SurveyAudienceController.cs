using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class SurveyAudienceController : ApiController
    {

        #region Public
        private readonly ISurveyQuestionService _surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SurveyAudienceController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Audience count
        /// </summary>
        public GetAudience_Result Post(GetAudienceCountRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                //if (request.CampaignQuizIds == null)
                //{
                //    request.CampaignQuizIds = string.Empty;
                //}
                //if (request.CampaignQuizAnswerIds == null)
                //{
                //    request.CampaignQuizAnswerIds = string.Empty;
                //}
                //if(request.CampaignQuizIdsExcluded == null)
                //{
                //    request.CampaignQuizIdsExcluded = string.Empty;
                //}
                return _surveyQuestionService.GetAudienceCount(request);
            }

        }
        #endregion
    }
}
