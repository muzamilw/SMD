using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class DisplayAdsCampaignAnalyticController : ApiController
    {

        #region private
        private readonly IAdCampaignRepository _IAdCampaignRepository;
        #endregion
        public DisplayAdsCampaignAnalyticController(IAdCampaignRepository IAdCampaignRepository)
        {
            this._IAdCampaignRepository = IAdCampaignRepository;
        }
        // GET api/<controller>
        public IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getDisplayAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {
            return _IAdCampaignRepository.getDisplayAdsCampaignByCampaignIdAnalytics(compaignId, CampStatus, dateRange, Granularity);
        }

        // GET api/<controller>/5
        
    }
}