using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// External Login Api Controller 
    /// </summary>
    public class ExternalLoginController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalLoginController(IWebApiUserService webApiUserService)
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
        /// External Login
        /// </summary>
        public async Task<WebApiUser> Post(ExternalLoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // Check For Extenal Login 
            User user = await webApiUserService.ExternalLogin(request); 
            return user.CreateFrom();
        }

        #endregion
    }
}