using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AddCouponsResponseModelForApproval
    {


        public AddCouponsResponseModelForApproval()
        {
            this.Coupons = new List<CouponsApproval>();
        }
        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
        public List<CouponsApproval> Coupons { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
    }
}