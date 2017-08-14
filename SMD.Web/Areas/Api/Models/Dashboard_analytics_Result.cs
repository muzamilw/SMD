﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class Dashboard_analytics_Result
    {
        public Nullable<long> CampaignID { get; set; }
        public Nullable<long> CouponID { get; set; }
        public Nullable<int> PQID { get; set; }
        public string Question { get; set; }
        public string CampaignName { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> LandingPageConv { get; set; }
        public Nullable<int> AdViewed { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public Nullable<System.DateTime> EventDateTime { get; set; }
        public Nullable<double> Ans1Percentage { get; set; }
        public Nullable<double> Ans2Percentage { get; set; }
        public Nullable<double> Ans3Percentage { get; set; }
        public Nullable<double> LastSevenDaysPer { get; set; }
        public Nullable<double> LastfourteenDaysPer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public Nullable<double> Option1Percentage { get; set; }
        public Nullable<double> Option2Percentage { get; set; }
        public Nullable<double> Option3Percentage { get; set; }
        public Nullable<double> Option4Percentage { get; set; }
        public Nullable<double> Option5Percentage { get; set; }
        public Nullable<double> Option6Percentage { get; set; }
        public Nullable<int> AnsweredQuestion { get; set; }
        public Nullable<double> Completed { get; set; }
        public Nullable<int> Option1TAnswers { get; set; }
        public Nullable<int> Option2TAnswers { get; set; }
        public Nullable<int> Option3TAnswers { get; set; }
        public Nullable<int> Option4TAnswers { get; set; }
        public Nullable<int> Option5TAnswers { get; set; }
        public Nullable<int> Option6TAnswers { get; set; }
    }
}