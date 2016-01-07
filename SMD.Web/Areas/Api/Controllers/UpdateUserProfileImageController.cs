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
    /// Update User Profile Image Api Controller 
    /// </summary>
    public class UpdateUserProfileImageController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateUserProfileImageController(IWebApiUserService webApiUserService)
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
        /// Update User Profile
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(string authenticationToken, UpdateUserProfileRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return await webApiUserService.UpdateProfile(request); 
        }

        #endregion
    }
}