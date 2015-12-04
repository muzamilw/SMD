using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class AdCampaignBaseController : ApiController
    {
        #region Public
        private readonly IAdvertService _advertService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdCampaignBaseController(IAdvertService advertService)
        {
            this._advertService = advertService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Adverts 
        /// 
        /// </summary>
        public List<AdCampaign> GetAdvertsByUserId()
        {
            return _advertService.GetAdverts();
        }

        #endregion
    }
}
