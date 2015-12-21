﻿using System.Net.Http;
using System.Threading.Tasks;
using SMD.Interfaces.Services;
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
        [ApiException]
        public async Task<HttpResponseMessage> Post(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            await webApiUserService.Archive(userId); 

            return new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK
                   };
        }

        #endregion
    }
}