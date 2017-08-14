using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
   public class CouponsResponseModelForApproval
    {
        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
       public IEnumerable<vw_Coupons> Coupons { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
    }
}
