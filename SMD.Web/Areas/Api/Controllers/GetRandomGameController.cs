using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetRandomGameController : ApiController
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
        public GetRandomGameController(IWebApiUserService webApiUserService, IEmailManagerService emailManagerService, IAdvertService advertService)
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

            SMD.Models.DomainModels.AdCampaign oCampaignRecord = _advertService.GetAdCampaignById(CampaignId);
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
        public SMD.MIS.Areas.Api.Models.AdCampaign Post(SMD.MIS.Areas.Api.Models.AdCampaign campaign)
        {
            if (campaign == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _advertService.SendApprovalRejectionEmail(campaign.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}
