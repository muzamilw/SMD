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
        public IEnumerable<getAdsCampaignByCampaignIdtblAnalytic_Result> tbl { get; set; }
        public IEnumerable<getCampaignROItblAnalytic_Result> ROItbl { get; set; }
        public List<getAdsCampaignPerCityPerGenderFormAnalytic_Result> PerGenderChart { get; set; }
        public List<getAdsCampaignPerCityPerAgeFormAnalytic_Result> PerAgeChart { get; set; }
    }
}