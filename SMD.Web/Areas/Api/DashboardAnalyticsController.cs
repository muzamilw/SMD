﻿using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api
{
    public class DashboardAnalyticsController : ApiController
    {

        private ICompanyService _companyService;
        public DashboardAnalyticsController(ICompanyService _companyService)
        {
            this._companyService = _companyService;
        }
        public ChartsObject GetChartsData()
        {

            ChartsObject RetrunObj = new ChartsObject();

            //string UserId = "b2f0532a-4c8a-431a-85dc-c4af0c1a2b93";
            //RetrunObj.AnalyticsList = _companyService.GetDashboardAnalytics(UserId).ToList();
          
            return RetrunObj;
            
        }
        public class ChartsObject
        {
            //public  Dashboard_analytics_Result Deals;

            //public  Dashboard_analytics_Result VideoCampaign;

            //public  Dashboard_analytics_Result ProfileQuestion;

            //public List<Dashboard_analytics_Result> DraftList;
            //public List<Dashboard_analytics_Result> LiveList;
            //public List<Dashboard_analytics_Result> PendingApprList;
            //public List<Dashboard_analytics_Result> PauseApprList;
            public List<Dashboard_analytics_Result> AnalyticsList;
        }
    }
}