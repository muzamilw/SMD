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
    public class ReferUsersController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;
        private IEmailManagerService emailManagerService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ReferUsersController(IWebApiUserService webApiUserService, IEmailManagerService emailManagerService)
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


        public bool Post(string email, int companyId, string mode)
        {
            try
            {

                if (mode == "business")
                {
                    emailManagerService.SendEmailInviteBusiness(email, companyId);
                }
                else if (mode == "advertiser")
                {
                    emailManagerService.SendEmailInviteAdvertiser(email, companyId);
                }
                return true;
            } catch (Exception ex)
            {
                return false;
            }
          
            
        }

        #endregion
    }
}
