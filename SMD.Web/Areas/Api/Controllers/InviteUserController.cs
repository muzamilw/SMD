using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class InviteUserController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;
        private IEmailManagerService emailManagerService;
        private IManageUserService manageUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InviteUserController(IWebApiUserService webApiUserService, IEmailManagerService emailManagerService, IManageUserService manageUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.emailManagerService = emailManagerService;
            this.manageUserService = manageUserService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>
        //public bool Get(string Email,string UserId)
        //{

        //    emailManagerService.SendEmailToInviteUser(Email, UserId);
        //    return true;

        //}


        //inviting the new user
        public bool Post(InviteUserRequest request)
        {

            var User = manageUserService.ComanyUserExists(request.Email);
            if (User == null) // user does not exists
            {

                var userr = manageUserService.AddUserInvitation(request.Email, request.RoleId);

               
                return true;

            }
            else
                return false;
            
            
        }

        #endregion
    }
}
