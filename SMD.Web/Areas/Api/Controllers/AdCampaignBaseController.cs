﻿using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class AdCampaignBaseController : ApiController
    {
        #region Public
        private readonly IAdvertService _campaignService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdCampaignBaseController(IAdvertService campaignService)
        {
            this._campaignService = campaignService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public AdCampaignBaseResponse getBaseData()
        {
            return new AdCampaignBaseResponse { 
                Languages = _campaignService.GetCampaignBaseData().Languages.Select(lang => lang.CreateFrom()),
                countries = _campaignService.GetCampaignBaseData().countries.Select(coun => coun.CreateFrom())
            };
        }

        #endregion
    }
}
