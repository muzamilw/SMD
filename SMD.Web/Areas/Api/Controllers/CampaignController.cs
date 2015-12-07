using SMD.Interfaces.Services;
using SMD.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CampaignController : ApiController
    {
          #region Public
        private readonly IAdvertService _campaignService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CampaignController(IAdvertService campaignService)
        {
            this._campaignService = campaignService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public List<CampaignGridModel> Get()
        {
            return _campaignService.GetCampaignByUserId();
        }

        #endregion
    }
}
