using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CampaignFormAnalyticController : ApiController
    {
        #region private
        private readonly IAdCampaignResponseService _IAdCampaignResponseService;
        #endregion
        // GET api/<controller>

        public CampaignFormAnalyticController(IAdCampaignResponseService IAdCampaignResponseService)
        {
            this._IAdCampaignResponseService = IAdCampaignResponseService;
        }
        public FormAnalyticResponseModel Get(long Id, int Choice, int Gender, int age, string profession, string City, int QId, int type)
        {
            FormAnalyticResponseModel data = new FormAnalyticResponseModel();
            data.QQStats = _IAdCampaignResponseService.getCampaignByIdQQFormAnalytic(Id, Choice, Gender, age, profession, City, type, QId);
            return data;
        }

        // GET api/<controller>/5
    }
}