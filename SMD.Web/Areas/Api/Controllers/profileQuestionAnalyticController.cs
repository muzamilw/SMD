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
    public class profileQuestionAnalyticController : ApiController
    {
        #region private
        private readonly IProfileQuestionService _IProfileQuestionService;
        #endregion

        #region Controller
        public profileQuestionAnalyticController(IProfileQuestionService IProfileQuestionService) {
            _IProfileQuestionService = IProfileQuestionService;
        }
        #endregion
        public PQByPQIDForAnalyticsResponse getSurvayByPQIDAnalytics(int PQId, int CampStatus, int dateRange, int Granularity)
        {
            PQByPQIDForAnalyticsResponse data = new PQByPQIDForAnalyticsResponse();
            data.lineCharts = _IProfileQuestionService.getSurvayByPQIDAnalytics(PQId, CampStatus, dateRange, Granularity);
            data.pieCharts = _IProfileQuestionService.getSurveyByPQIDRatioAnalytic(PQId, dateRange);
            data.tbl = _IProfileQuestionService.getSurvayByPQIDtblAnalytic(PQId);
        
            return data;
        }

    }
}