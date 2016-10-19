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
            SurvayBySQIDForAnalyticsResponse data = new SurvayBySQIDForAnalyticsResponse();
            data.lineCharts = _ISurveyQuestionService.getPollsBySQIDAnalytics(SQId, CampStatus, dateRange, Granularity);
            data.pieCharts = _ISurveyQuestionService.getPollBySQIDRatioAnalytic(SQId, dateRange);
            data.tbl = _ISurveyQuestionService.getPollBySQIDtblAnalytic(SQId);
        
            return data;

            
        }
    }
}