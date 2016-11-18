using SMD.ExceptionHandling;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Charge Customer Api Controller 
    /// </summary>
    public class NotificationsController : ApiController
    {
        
        #region Private

      
        private readonly INotificationService notificationService;
        private readonly IWebApiUserService userService;

      

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationsController(INotificationService notificationService, IWebApiUserService userService)
        {
            this.notificationService = notificationService;
            this.userService = userService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get user's notifications
        /// </summary>
        [ApiException]
        public List<vw_Notifications> Get(string authenticationToken,string UserId)
        {
            var user = userService.GetUserByUserId(UserId);

            return notificationService.GetNotificationsByUserId(UserId,user.Phone1);
        }
        
        #endregion
    }
}