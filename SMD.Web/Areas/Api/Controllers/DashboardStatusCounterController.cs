using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class DashboardStatusCounterController : ApiController
    {
        private ICompanyService _companyService;
        public DashboardStatusCounterController(ICompanyService _companyService)
        {
            this._companyService = _companyService;
        }

        public Counters GetCounters()
        {
            Counters obj = new Counters();
            obj.LiveVideoCampaign = _companyService.GetStatusesCounters().Where(i => i.Key == "liveVidCamp").FirstOrDefault().Value;
            obj.LiveDisplayCampaign = _companyService.GetStatusesCounters().Where(i => i.Key == "liveDisplayCamp").FirstOrDefault().Value;
            obj.LiveDeals = _companyService.GetStatusesCounters().Where(i => i.Key == "Deals").FirstOrDefault().Value;
            obj.LivePolls = _companyService.GetStatusesCounters().Where(i => i.Key == "Polls").FirstOrDefault().Value;
            return obj;
        }

    }

    public class Counters
    {
      public int LiveVideoCampaign;
      public int LiveDisplayCampaign;
      public int LiveDeals;
      public int LivePolls;
    }
}
