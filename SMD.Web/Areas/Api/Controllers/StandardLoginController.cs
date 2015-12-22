using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Standard Login Api Controller 
    /// </summary>
    public class StandardLoginController : ApiController
    {
        #region Private

        private readonly IWebApiUserService webApiUserService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StandardLoginController(IWebApiUserService webApiUserService)
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
        /// Login
        /// </summary>
        [ApiExceptionCustom]
        public async Task<LoginResponse> Get([FromUri] StandardLoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return new LoginResponse
                       {
                           Message = LanguageResources.InvalidRequest
                       };
            }

            SMD.Models.ResponseModels.LoginResponse response = await webApiUserService.StandardLogin(request);
            return response.CreateFrom();
        }

        #endregion
    }
}