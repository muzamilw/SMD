﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetApprovalCount_Result
    {
        public Nullable<int> CouponCount { get; set; }
        public Nullable<int> AdCmpaignCount { get; set; }
        public Nullable<int> DisplayAdCount { get; set; }
        public Nullable<int> ProfileQuestionCount { get; set; }
        public Nullable<int> SurveyQuestionCount { get; set; }
    }
}
