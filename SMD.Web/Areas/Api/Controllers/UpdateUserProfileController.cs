﻿using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.Areas.Api.ModelMappers;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Update User Profile Api Controller 
    /// </summary>
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




        public WebApiUser Get(string authenticationToken, string UserId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            var response = webApiUserService.GetLoggedInUser(UserId);
            WebApiUser apiuser = response.CreateFromUserForMobile();

            return apiuser;
             


        }
        
        /// <summary>
        /// Update User Profile
        /// </summary>
       
        public async Task<BaseApiResponse> Post(string authenticationToken, [FromUri] UpdateUserProfileRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            try
            {


            return await webApiUserService.UpdateProfile(request);


            }
            catch (Exception e)
            {

                return new BaseApiResponse { Status = false, Message = e.ToString() };
            }

        }

        #endregion
    }
}