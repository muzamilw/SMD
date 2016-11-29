using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetRandomPollsController : ApiController
    {
        #region Attributes

        private readonly ISurveyQuestionService surveyQuestionService;
        #endregion

        #region Constructors
        public GetRandomPollsController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }
        #endregion

        #region Methods

        public List<GetRandomPolls_Result> Get()
        {
            var obj = surveyQuestionService.GetRandomPolls();
            return obj;
        }

        #endregion
    }
}
