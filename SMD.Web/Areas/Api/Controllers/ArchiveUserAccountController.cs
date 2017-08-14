using System.Threading.Tasks;
using SMD.Interfaces.Services;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Update User Profile Api Controller 
    /// </summary>
    public class ArchiveUserAccountController : ApiController
    {
        #region Private

        private readonly IWebApiUserService webApiUserService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ArchiveUserAccountController(IWebApiUserService webApiUserService)
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
        /// Archive User Account
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(string authenticationToken, string userId)
        {
            try
            {


                if (string.IsNullOrEmpty(authenticationToken) || string.IsNullOrEmpty(userId))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                }

                var user = webApiUserService.GetUserByUserId(userId);

                string token = Guid.NewGuid().ToString();

                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                var callbackUrl = baseUrl + "/account/DeleteAccount/?UserId=" + userId + "&code=" + token;

                return await webApiUserService.ArchiveRequestConfirmation(userId, token, callbackUrl);

            }
            catch (Exception e)
            {

                return new BaseApiResponse { Message = e.ToString(), Status = false };
            }
        }

        #endregion
    }
}