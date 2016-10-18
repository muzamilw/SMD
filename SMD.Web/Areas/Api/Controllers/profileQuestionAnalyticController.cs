using SMD.Interfaces.Services;
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
        public IEnumerable<getSurvayByPQID_Result> getSurvayByPQIDAnalytics(int PQId, int CampStatus, int dateRange, int Granularity)
        {

            return _IProfileQuestionService.getSurvayByPQIDAnalytics(PQId, CampStatus, dateRange, Granularity);
        }

    }
}