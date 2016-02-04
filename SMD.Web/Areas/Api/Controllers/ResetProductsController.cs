using System;
using SMD.Interfaces.Services;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Products Controller 
    /// </summary>
    public class ResetProductsController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constuctor 
        /// </summary>
        public ResetProductsController(IWebApiUserService webApiUserService)
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
        /// Get Ads, Surveys, Questions
        /// </summary>
        [ApiExceptionCustom]
        public void Get()
        {
            webApiUserService.ResetProductsResponses();
        }
        
        #endregion
    }
}