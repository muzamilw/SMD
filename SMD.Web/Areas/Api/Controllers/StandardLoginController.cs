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
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Standard Login Api Controller 
    /// </summary>
    [Authorize]
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

        public string Get()
        {
            return "I am from Get method!";
        }


        /// <summary>
        /// Login
        /// </summary>
        [ApiException]
        public async Task<WebApiUser> Post(StandardLoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            User user = await webApiUserService.StandardLogin(request);

            return user.CreateFrom();
        }

        #endregion
    }
}