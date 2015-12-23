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
    /// Profile Question for APIs Controller 
    /// </summary>
    public class GetProfileQuestionForApiController : ApiController
    {
        #region Public
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetProfileQuestionForApiController(IProfileQuestionService profileQuestionService)
        {
            _profileQuestionService = profileQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public ProfileQuestionApiSearchResponse Get(string authenticationToken, [FromUri] GetProfileQuestionApiRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var resposne= _profileQuestionService.GetProfileQuestionsByGroupForApi(request).CreateFrom();
            return resposne;
        }

       
        #endregion
    }
}