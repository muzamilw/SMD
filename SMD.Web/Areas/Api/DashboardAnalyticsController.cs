using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api
{
    public class DashboardAnalyticsController : ApiController
    {

        private ICompanyService _companyService;
        public DashboardAnalyticsController(ICompanyService _companyService)
        {
            this._companyService = _companyService;
        }
        public void GetChartsData Get()
        {
           
        }

    }
}
