using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;
using System;
using AutoMapper;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using AutoMapper;
using SMD.WebBase.Mvc;
using SMD.Implementation.Services;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class UserFeedbackController : ApiController
    {
        #region Public

        private readonly IEmailManagerService emailManagerService;
        private readonly IWebApiUserService _userService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserFeedbackController(IEmailManagerService emailManagerService, IWebApiUserService _userService)
        {

            this.emailManagerService = emailManagerService;
            this._userService = _userService;
        }

        #endregion
        #region Public


        public bool Get(string authenticationToken, string UserId)
        {
            return true;
        }



        /// <summary>
        /// User Payout
        /// </summary>
        
          public BaseApiResponse Post(string authenticationToken, string UserId, string feedback, string City, string Country)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {
                 var user = _userService.GetUserByUserId(UserId);

                 emailManagerService.SendAppFeedback(UserId, feedback, City, Country, user.FullName, user.Email, user.Phone1);

           
                response.Status = true;
                response.Message = "Success";
                return response;
              
            }
            catch (Exception e)
            {

                response.Status = false;
                response.Message = "Failure : " + e.ToString();
                return response;
            }
        }


        #endregion
    }
}
