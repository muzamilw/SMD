using System;
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
        public int? PQId { get; set; }
        public int? PQAnswerId { get; set; }
        public long? SQId { get; set; }
        public int? SQAnswer { get; set; }
        public bool? IncludeorExclude { get; set; }
        public int? LanguageId { get; set; }

        public int? IndustryId { get; set; }

        public string questionString { get; set; }

        public string answerString { get; set; }

        public string Language { get; set; } 
    }
}