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
    public class SendInviteUsingWebController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;
        private IEmailManagerService emailManagerService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SendInviteUsingWebController(IWebApiUserService webApiUserService, IEmailManagerService emailManagerService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.emailManagerService = emailManagerService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public bool Get(string Email, int CompanyId)
        {
            try
            {
                emailManagerService.SendEmailToInviteUser(Email, CompanyId);
                return true;
            } catch (Exception ex)
            {
                return false;
            }
          
            
        }

        #endregion
    }
}
