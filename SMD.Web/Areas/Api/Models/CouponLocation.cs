using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CouponLocation
    {
        public long CouponId { get; set; }
        public string LocationLAT { get; set; }
        public string LocationLON { get; set; }
    }
}