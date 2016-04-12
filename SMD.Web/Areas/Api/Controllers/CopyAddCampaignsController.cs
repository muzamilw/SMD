using SMD.Interfaces.Services;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CopyAddCampaignsController : ApiController
    {
        private readonly IAdvertService _advertSerice;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CopyAddCampaignsController(IAdvertService advertSerice)
        {

            this._advertSerice = advertSerice;
        }

        #endregion

        #region Public

      
        [HttpGet]
        public long Get(long CampaignId)
        {

            return _advertSerice.CopyAddCampaigns(CampaignId);
        }

        #endregion
    }
}
