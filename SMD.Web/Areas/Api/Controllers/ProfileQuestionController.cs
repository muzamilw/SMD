using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Profile Question Api Controller 
    /// </summary>
    [Authorize]
    public class ProfileQuestionController : ApiController
    {
        #region Public
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public ProfileQuestionController(IProfileQuestionService profileQuestionService)
        {
            _profileQuestionService = profileQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public ProfileQuestionSearchRequestResponse Get([FromUri] ProfileQuestionSearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _profileQuestionService.GetProfileQuestions(request).CraeteFrom();
        }

        /// <summary>
        /// Delete Profile Question 
        /// </summary>
        public Boolean Delete(Models.ProfileQuestion question)
        {
            if (question == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _profileQuestionService.DeleteProfileQuestion(question.CreateFrom());
        }

        /// <summary>
        /// Add/Edit Profile Question 
        /// </summary>
        public Models.ProfileQuestion Post(Models.ProfileQuestion question)
        {
            //if (question == null || !ModelState.IsValid)
            //{
            //    throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            //}
            return _profileQuestionService.SaveProfileQuestion(question.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}