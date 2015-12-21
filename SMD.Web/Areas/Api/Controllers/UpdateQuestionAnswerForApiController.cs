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
    /// Update Profile Question's Ans for APIs Controller 
    /// </summary>
    [Authorize]
    public class UpdateQuestionAnswerForApiController : ApiController
    {
        #region Public
        private readonly IProfileQuestionUserAnswerService profileQuestionUserAnswerService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UpdateQuestionAnswerForApiController(IProfileQuestionUserAnswerService profileQuestionUserAnswerService)
        {
            this.profileQuestionUserAnswerService = profileQuestionUserAnswerService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Update Profile Question's Answer
        /// </summary>
        [ApiExceptionCustom]
        public UpdateProfileQuestionUserAnswerResponse Post(UpdateProfileQuestionUserAnswerApiRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return profileQuestionUserAnswerService.UpdateProfileQuestionUserAnswer(request).CreateFrom();
        }
        #endregion
    }
}