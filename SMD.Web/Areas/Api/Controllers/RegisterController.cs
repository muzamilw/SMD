﻿using System.Threading.Tasks;
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
    /// Register Api Controller 
    /// </summary>
    [Authorize]
    public class RegisterCustomController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterCustomController(IWebApiUserService webApiUserService)
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
        /// Register In app user
        /// </summary>
        [ApiException]
        public async Task<WebApiUser> Post([FromUri] RegisterCustomRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // Register User IN App
            User user = await webApiUserService.RegisterCustom(request); 
            return user.CreateFrom();
        }

        #endregion
    }
}