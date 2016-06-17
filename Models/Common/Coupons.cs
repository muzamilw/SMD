using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.Common
{
    public class Coupons
    {
        public long CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponTitle { get; set; }
        public string Firstline { get; set; }
        public string SecondLine { get; set; }
        public string CouponImage { get; set; }
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public int CouponTakenValue { get; set; }
        public double CouponDiscountedValue { get; set; }
    }
 
}
