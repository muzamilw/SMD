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
        #endregion
        public AdsCampaignAnalyticController(IAdvertService IAdvertService)
        {
            this._IAdvertService = IAdvertService;
        }
        // GET api/<controller>
        //public IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getDisplayAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        //{
        //    return _IAdCampaignRepository.getDisplayAdsCampaignByCampaignIdAnalytics(compaignId, CampStatus, dateRange, Granularity);
        //}
        public AdsCampaignByCampaignIdForAnalyticsResponse getAdsCampaignByCampaignIdForAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {
            AdsCampaignByCampaignIdForAnalyticsResponse data = new AdsCampaignByCampaignIdForAnalyticsResponse();
            data.lineCharts = _IAdvertService.getAdsCampaignByCampaignIdAnalytics(compaignId, CampStatus, dateRange, Granularity);
            data.pieCharts = _IAdvertService.getAdsCampaignByCampaignIdRatioAnalytic(compaignId, dateRange);
            data.tbl = _IAdvertService.getAdsCampaignByCampaignIdtblAnalytic(compaignId);
            data.ROItbl = _IAdvertService.getCampaignROItblAnalytic(compaignId);
            return data;
        }

        // GET api/<controller>/5
        
    }
}