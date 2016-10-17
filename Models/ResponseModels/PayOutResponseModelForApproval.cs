using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
    public class PayOutResponseModelForApproval
    {
        /// <summary>
        ///  PayOuthistory List
        /// </summary>
        public IEnumerable<PayOutHistory> PayOutHistory { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
    }
}
