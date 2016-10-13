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
    public class VideoAdsCampaignAnalyticController : ApiController
    {
        // GET api/<controller>
          #region private
        private readonly IAdCampaignRepository _IAdCampaignRepository;
        #endregion
        public VideoAdsCampaignAnalyticController(IAdCampaignRepository IAdCampaignRepository)
        {
            this._IAdCampaignRepository = IAdCampaignRepository;
        }
        // GET api/<controller>
        public IEnumerable<getAdsCampaignByCampaignId_Result> getAdsCampaignByCampaignIdForAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {
            return _IAdCampaignRepository.getAdsCampaignByCampaignIdForAnalytics(compaignId, CampStatus, dateRange, Granularity);
        }
    }
}