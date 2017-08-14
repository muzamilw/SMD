using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class SurvayBySQIDForAnalyticsResponse
    {
        public IEnumerable<getPollsBySQID_Result> lineCharts { get; set; }
        public IEnumerable<getPollBySQIDRatioAnalytic_Result> pieCharts { get; set; }
        public IEnumerable<getPollBySQIDtblAnalytic_Result> tbl { get; set; }
        public IEnumerable<getPollBySQIDRatioAnalytic_Result> pieChartstbl { get; set; }
        public int filteredStat { get; set; }
        public long SQID { get; set; }
    }
}