using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using System.Web;

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
        public CampaignRequestResponseModel Get([FromUri] AdCampaignSearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                return _campaignService.GetCampaigns(request).CreateCampaignFrom();
            }
           
        }


        public void Post(SMD.Models.DomainModels.AdCampaign campaignModel)
        {
            campaignModel.Status = (int)AdCampaignStatus.Draft;

            _campaignService.CreateCampaign(campaignModel);

        }
        #endregion
    }
}
