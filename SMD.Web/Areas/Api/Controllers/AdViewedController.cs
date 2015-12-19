using System.Net.Http;
using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Ad Viewed Api Controller 
    /// </summary>
    [Authorize]
    public class AdViewedController : ApiController
    {

        #region Private

        private readonly IWebApiUserService webApiUserService;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AdViewedController(IWebApiUserService webApiUserService)
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
        /// Ad Viewed
        /// </summary>
        [ApiException]
        public async Task<HttpResponseMessage> Post([FromUri] AdViewedRequest request)
        {
            if (request == null || !ModelState.IsValid || string.IsNullOrEmpty(request.UserId) || request.AdCampaignId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // Update Transactions on Ad View
            await webApiUserService.UpdateTransactionOnViewingAd(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        #endregion
    }
}