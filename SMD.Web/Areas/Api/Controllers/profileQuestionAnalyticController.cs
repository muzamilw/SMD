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
          //  data.pieCharts = _IProfileQuestionService.getSurveyByPQIDRatioAnalytic(PQId, dateRange);
            data.tbl = _IProfileQuestionService.getSurvayByPQIDtblAnalytic(PQId);
        
            ///

            List<getSurveyByPQIDRatioAnalytic_Result> listtbl = new List<getSurveyByPQIDRatioAnalytic_Result>();
            List<getSurveyByPQIDRatioAnalytic_Result> list = _IProfileQuestionService.getSurveyByPQIDRatioAnalytic(PQId, dateRange).ToList();
            data.pieCharts = list;
           
            int total = 0;

            if (list != null)
            {
                foreach (getSurveyByPQIDRatioAnalytic_Result res in list)
                {
                    total = total + (int)(res.value != null ? res.value : 0);
                }
                foreach (getSurveyByPQIDRatioAnalytic_Result res in list)
                {
                    getSurveyByPQIDRatioAnalytic_Result item = new getSurveyByPQIDRatioAnalytic_Result();
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