using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SMD.MIS.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;

namespace SMD.MIS.Areas.Api.Controllers
{
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
        public bool Post(SMD.Models.DomainModels.SurveyQuestion surveyModel)
        {
           // surveyModel.Status = (int)SurveyQuestionStatus.Draft; already assigned in ko based on stripe

            return _surveyQuestionService.Create(surveyModel);

        }
        #endregion
    }
}