﻿using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class BuyItController : ApiController
    {
         private readonly IWebApiUserService webApiUserService;
        private IEmailManagerService emailManagerService;
        private readonly IAdvertService _advertService;
        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BuyItController(IWebApiUserService webApiUserService, IEmailManagerService emailManagerService, IAdvertService advertService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.emailManagerService = emailManagerService;
            this._advertService = advertService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public bool Get(string UserId, long CampaignId)//string BuyItModel request
        {
            if (string.IsNullOrEmpty(UserId) || CampaignId == 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            AdCampaign oCampaignRecord = _advertService.GetAdCampaignById(CampaignId);
            if (oCampaignRecord != null)
            {
                emailManagerService.SendBuyItEmailToUser(UserId, oCampaignRecord);
                return true;
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
        }

        #endregion
    }
}
