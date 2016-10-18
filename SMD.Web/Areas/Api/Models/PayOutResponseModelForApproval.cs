using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class PayOutResponseModelForApproval
    {
        public PayOutResponseModelForApproval()
        {
            this.PayOutHistory = new List<PayOutHistory>();
        }
        public List<PayOutHistory> PayOutHistory { get; set; }

        public int TotalCount { get; set; }

    }
}