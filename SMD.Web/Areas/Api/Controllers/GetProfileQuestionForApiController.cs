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
    /// Profile Question for APIs Controller 
    /// </summary>
    [Authorize]
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
        public ProfileQuestionApiSearchResponse Get([FromUri] GetProfileQuestionApiRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var resposne= _profileQuestionService.GetProfileQuestionsByGroupForApi(request).CreateFrom();
            return resposne;
        }

       
        #endregion
    }
}