﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CampaignSearchRequest
    {
        #region Public
        /// <summary>
        /// Search request type
        /// 
        /// </summary>
        public int RequestId { get; set; }

        public int QuestionId { get; set; }

        public long SQID { get; set; }
        #endregion
    }

    public class CampaignUpdateRequest
    {
        #region Public
        /// <summary>
        /// Search request type
        /// 
        /// </summary>
        public AdCampaignTargetCriteria criteriaObj { get; set; }

        public AdCampaignTargetLocation locationObj { get; set; }

        #endregion
    }
}