using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class SurveyQuestionEditorController : ApiController
    {
          #region Public
        private readonly ISurveyQuestionService _surveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SurveyQuestionEditorController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public SurveyQuestionEditorRequestResponseModel Get([FromUri] SurveySearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                if (request.SqId != 0)
                    return _surveyQuestionService.GetSurveyQuestion(request.SqId).CreateFromWithRef();
                else
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

        }
        #endregion
    }
}
