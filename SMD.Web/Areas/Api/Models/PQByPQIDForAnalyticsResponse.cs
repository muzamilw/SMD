using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class PQByPQIDForAnalyticsResponse
    {

        
        public IEnumerable<getSurvayByPQID_Result> lineCharts { get; set; }
        public IEnumerable<getSurveyByPQIDRatioAnalytic_Result> pieCharts { get; set; }
        public IEnumerable<getSurveyByPQIDRatioAnalytic_Result> pieChartstbl { get; set; }
        public IEnumerable<getSurvayByPQIDtblAnalytic_Result> tbl { get; set; }
    }
}