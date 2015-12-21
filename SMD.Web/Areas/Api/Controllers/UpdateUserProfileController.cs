﻿using System.Net.Http;
using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Update User Profile Api Controller 
    /// </summary>
    //[Authorize]
    public class UpdateUserProfileController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateUserProfileController(IWebApiUserService webApiUserService)
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
        [ApiException]
        public async Task<HttpResponseMessage> Post([FromUri] UpdateUserProfileRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            await webApiUserService.UpdateProfile(request); 

            return new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK
                   };
        }

        #endregion
    }
}