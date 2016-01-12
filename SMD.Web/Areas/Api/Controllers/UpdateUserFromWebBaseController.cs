using System.Collections.ObjectModel;
using SMD.Interfaces.Services;
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
    /// Update Web User Profile Api Controller  | BASE DATA
    /// </summary>
    public class UpdateUserFromWebBaseController : ApiController
    {
        #region Private
        private readonly IWebApiUserService webApiUserService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateUserFromWebBaseController(IWebApiUserService webApiUserService)
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
        public UserProfileBaseResponse Get()
        {
            var response=webApiUserService.GetBaseDataForUserProfile();
            return response.CreateFrom();
        }
        #endregion
    }
}