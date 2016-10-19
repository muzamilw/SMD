using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AdsCampaignByCampaignIdForAnalyticsResponse
    {
        public IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> lineCharts { get; set; }
        public IEnumerable<getAdsCampaignByCampaignIdRatioAnalytic_Result> pieCharts { get; set; }
    }
}