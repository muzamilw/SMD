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
    /// Ad Campaign Api Controller 
    /// </summary>
    public class AdCampaignController : ApiController
    {
        #region Public
        private readonly IAdvertService _advertService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdCampaignController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Add Campaigns
        /// </summary>
        public AdCampaignResposneModelForAproval Get([FromUri] AdCampaignSearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _advertService.GetAdCampaignForAproval(request).CreateFrom();
        }


        /// <summary>
        /// Update Ad Campaign 
        /// </summary>
        public AdCampaign Post(AdCampaign campaign)
        {
            if (campaign == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _advertService.UpdateAdCampaign(campaign.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}