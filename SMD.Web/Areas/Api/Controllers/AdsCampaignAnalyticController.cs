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
    public class AdsCampaignAnalyticController : ApiController
    {

        #region private
        private readonly IAdvertService _IAdvertService;
        private readonly IAdCampaignResponseService _IAdCampaignResponseService;
        #endregion
        public AdsCampaignAnalyticController(IAdvertService IAdvertService, IAdCampaignResponseService IAdCampaignResponseService)
        {
            this._IAdvertService = IAdvertService;
            this._IAdCampaignResponseService = IAdCampaignResponseService;
        }
       
        public AdsCampaignByCampaignIdForAnalyticsResponse getAdsCampaignByCampaignIdForAnalytics(int campaignId, int CampStatus, int dateRange, int Granularity)
        {
            AdsCampaignByCampaignIdForAnalyticsResponse data = new AdsCampaignByCampaignIdForAnalyticsResponse();
            data.lineCharts = _IAdvertService.getAdsCampaignByCampaignIdAnalytics(campaignId, CampStatus, dateRange, Granularity);
            data.pieCharts = _IAdvertService.getAdsCampaignByCampaignIdRatioAnalytic(campaignId, dateRange);
            data.tbl = _IAdvertService.getAdsCampaignByCampaignIdtblAnalytic(campaignId);
            data.ROItbl = _IAdvertService.getCampaignROItblAnalytic(campaignId);
            data.PerGenderChart = _IAdvertService.getAdsCampaignPerCityPerGenderFormAnalytic(campaignId);
            data.PerAgeChart =  _IAdvertService.getAdsCampaignPerCityPerAgeFormAnalytic(campaignId);
            data.UserLocation = _IAdCampaignResponseService.getCampaignUserLocationByCId(campaignId);
            data.ByAgeStats = _IAdvertService.getCampaignImpressionByAgeByCId(campaignId);
            data.ByProfessionStats = _IAdvertService.getCampaignImpressionByProfessionByCId(campaignId);
            data.ByGenderStats = _IAdvertService.getCampaignImpressionByGenderByCId(campaignId);
            return data;
        }
              
    }
}