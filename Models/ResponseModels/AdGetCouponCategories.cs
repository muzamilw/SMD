using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
     public class AdGetCouponCategories
    {
        public long Id { get; set; }
        public long CouponId { get; set; }
        public string couponImage1 { get; set; }
        public string CouponTitle { get; set; }
    }
}
