using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CopyCampaignController : ApiController
    {
        private readonly IAdvertService _advertService;
        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CopyCampaignController(IAdvertService advertService)
        {
            this._advertService = advertService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public bool Get(long CampaignId)
        {
            _advertService.CopyAddCampaigns(CampaignId);
            return true;
        }

        #endregion
    }
}
