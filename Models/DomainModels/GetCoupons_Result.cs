using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class GetCoupons_Result
    {
        public long CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponTitle { get; set; }
        public string Firstline { get; set; }
        public string SecondLine { get; set; }
        public string CouponImage { get; set; }
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public Nullable<int> CouponType { get; set; }


        public string AdvertisersLogoPath { get; set; }
        
    }
}
