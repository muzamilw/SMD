using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Approve Survey Api Controller 
    /// </summary>
    public class ApproveSurveyController : ApiController
    {

        #region Private

        private readonly IWebApiUserService webApiUserService;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ApproveSurveyController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Approve Survey
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(string authenticationToken, [FromUri] ApproveSurveyRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid || 
                string.IsNullOrEmpty(request.UserId) || request.SurveyQuestionId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // Update Transactions on Approve Survey
            return await webApiUserService.UpdateTransactionOnSurveyApproval(request);
        }

        #endregion
    }
}