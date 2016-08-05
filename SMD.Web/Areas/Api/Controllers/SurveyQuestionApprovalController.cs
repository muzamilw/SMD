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
    /// Survey Question Approval Api Controller 
    /// </summary>
    public class SurveyQuestionApprovalController : ApiController
    {
        #region Public

        private readonly ISurveyQuestionService surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SurveyQuestionApprovalController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Survey Questions
        /// </summary>
        public SurveyQuestionResposneModelForAproval Get([FromUri] SurveySearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.GetRejectedSurveyQuestionsForAproval(request).CreateFrom();
        }


        /// <summary>
        /// Edit Survey Question 
        /// </summary>
        public SurveyQuestion Post(SurveyQuestion question)
        {
            if (question == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return surveyQuestionService.EditSurveyQuestion(question.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}