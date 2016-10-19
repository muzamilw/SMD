using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class DealByCouponIdForAnalyticsResponse
    {
        public IEnumerable<getDealByCouponID_Result> lineCharts { get; set; }
        public IEnumerable<getDealByCouponIdRatioAnalytic_Result> pieCharts { get; set; }

    }
}