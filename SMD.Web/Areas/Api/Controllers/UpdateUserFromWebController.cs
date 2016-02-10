﻿using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Update Web User Profile Api Controller 
    /// </summary>
    public class UpdateUserFromWebController : ApiController
    {
        #region Private
        private readonly IWebApiUserService webApiUserService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateUserFromWebController(IWebApiUserService webApiUserService)
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
        /// Get User's Profile 
        /// </summary>
        public WebApiUser Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            var response=  webApiUserService.GetLoggedInUser();
            return  response.CreateFrom();
        }


        /// <summary>
        /// Update User Profile
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(UpdateUserProfileRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return await webApiUserService.UpdateProfile(request); 
        }

        #endregion
    }
}