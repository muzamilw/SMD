using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class PQResponseModelForApproval
    {

        public PQResponseModelForApproval()
        {
            this.ProfileQuestion = new List<ApproveProfileQuestion>();
        }
        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
        public List<ApproveProfileQuestion> ProfileQuestion { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }

    }
}