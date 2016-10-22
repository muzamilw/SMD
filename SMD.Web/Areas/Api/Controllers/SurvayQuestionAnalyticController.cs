using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class SurvayQuestionAnalyticController : ApiController
    {
        // GET api/<controller>
        #region private
        private readonly ISurveyQuestionService _ISurveyQuestionService;
        #endregion

        public SurvayQuestionAnalyticController(ISurveyQuestionService ISurveyQuestionService) {
            _ISurveyQuestionService = ISurveyQuestionService;
        }


        public SurvayBySQIDForAnalyticsResponse getPollsBySQIDAnalytics(int SQId, int CampStatus, int dateRange, int Granularity)
        {
            List<getPollBySQIDRatioAnalytic_Result> listtbl = new List<getPollBySQIDRatioAnalytic_Result>();
            SurvayBySQIDForAnalyticsResponse data = new SurvayBySQIDForAnalyticsResponse();
            data.lineCharts = _ISurveyQuestionService.getPollsBySQIDAnalytics(SQId, CampStatus, dateRange, Granularity);
            List<getPollBySQIDRatioAnalytic_Result> list = _ISurveyQuestionService.getPollBySQIDRatioAnalytic(SQId, dateRange);
            data.pieCharts = list;
            data.tbl = _ISurveyQuestionService.getPollBySQIDtblAnalytic(SQId);
            int total = 0;
            
            if (list != null)
            {
                foreach (getPollBySQIDRatioAnalytic_Result res in list)
                {
                    total = total + (int)(res.value != null ? res.value : 0);
                }
                foreach (getPollBySQIDRatioAnalytic_Result res in list)
                {
                    getPollBySQIDRatioAnalytic_Result item = new getPollBySQIDRatioAnalytic_Result();
                    item.label = res.label;
                    item.value = (int)(((float)res.value / (float)total) * 100);
                    listtbl.Add(item);
                }
            }
            data.pieChartstbl = listtbl;
        
            return data;

            
        }
    }
}