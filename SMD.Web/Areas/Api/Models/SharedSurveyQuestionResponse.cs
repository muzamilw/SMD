﻿using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class SharedSurveyQuestionResponse : BaseApiResponse
    {

        public GetSharedSurveyQuestion_Result GetSharedSurveyQuestion { get; set; }
    }
}