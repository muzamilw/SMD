﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AdCampaignTargetCriteria
    {
        public long CriteriaId { get; set; }
        public long? CampaignId { get; set; }
        public int? Type { get; set; }
        public int? PqId { get; set; }
        public int? PqAnswerId { get; set; }
        public long? SqId { get; set; }
        public int? SqAnswer { get; set; }
        public bool? IncludeorExclude { get; set; }
        public int? LanguageId { get; set; }

        public int? IndustryId { get; set; }

        public string QuestionString { get; set; }

        public string AnswerString { get; set; }
    }
}