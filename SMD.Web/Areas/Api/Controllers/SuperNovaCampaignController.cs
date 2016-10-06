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
    public class SuperNovaCampaignController : ApiController
    {
        #region Private
        private readonly IAdvertService _campaignService;

        #endregion
        #region Constructor
        public SuperNovaCampaignController(IAdvertService campaignService)
        {
            _campaignService = campaignService;
        }

        #endregion
        #region Public

        // GET api/<controller>
          public IEnumerable<getCampaignsByStatus_Result> getCampaignsByStatus()
        {
            return _campaignService.getCampaignsByStatus();

        }
          public IEnumerable<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(int CampaignType, int granuality, DateTime DateFrom, DateTime DateTo)
          {
              return _campaignService.GetLiveCampaignCountOverTime(CampaignType, DateFrom, DateTo, granuality);
          }
        #endregion
    }
}