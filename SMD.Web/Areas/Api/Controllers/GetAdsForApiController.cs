﻿using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Ads for APIs Controller 
    /// </summary>
    [Authorize]
    public class GetAdsForApiController : ApiController
    {
        #region Public

        private readonly IAdvertService advertService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetAdsForApiController(IAdvertService advertService)
        {
            this.advertService = advertService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Ads for API
        /// </summary>
        public AdCampaignApiSearchRequestResponse Post(GetAdsApiRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return advertService.GetAdCampaignsForApi(request).CreateResponseForApi();
        }
        #endregion
    }
}