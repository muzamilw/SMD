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
     
        public double Price { get; set; }
        public double Savings { get; set; }
        public double SwapCost { get; set; }

        public int DaysLeft { get; set; }

        public int CompanyId { get; set; }
        public string AdvertisersLogoPath { get; set; }
        
    }
 
}
