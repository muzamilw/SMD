using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AddCouponsResponseModelForApproval
    {

        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
        public IEnumerable<CouponsApproval> Coupons { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
    }
}