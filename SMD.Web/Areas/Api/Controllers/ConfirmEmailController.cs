using System.Threading.Tasks;
using System.Web.Http.Results;
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
    /// Confirm Email Api Controller 
    /// </summary>
    public class ConfirmEmailController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfirmEmailController(IWebApiUserService webApiUserService)
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
        /// Confirm Email
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Get(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // Confirm Email
            return await webApiUserService.ConfirmEmail(userId, code);
        }

        #endregion
    }
}