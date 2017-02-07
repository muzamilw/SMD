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
       
        public string CouponTitle { get; set; }

        public string CouponImage1 { get; set; }
     
        public double Price { get; set; }
        public double Savings { get; set; }
        public double SwapCost { get; set; }

        public int DaysLeft { get; set; }

        public int CompanyId { get; set; }
        public string LogoUrl { get; set; }

        public Nullable<double> distance { get; set; }

        public string LocationPhone { get; set; }

        public string CompanyName { get; set; }
        public string LocationTitle { get; set; }
        public string LocationCity { get; set; }

        public Nullable<int> DealsCount { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public double AvgRating { get; set; }

        public string CouponLink { get; set; }
        public int IsTwoForOneDeal { get; set; }

        public int IsCashAMoon { get; set; }
        public string LocationLAT { get; set; }
        public string LocationLON { get; set; }
        
    }



    public class PurchasedCoupons
    {
        public PurchasedCoupons()
        {

        }

        public long CouponPurchaseId { get; set; }
        public long CouponId { get; set; }

        public string CouponTitle { get; set; }

        public string CouponImage1 { get; set; }

        public double Price { get; set; }
        public double Savings { get; set; }
        public double SwapCost { get; set; }

        public int DaysLeft { get; set; }

        public int CompanyId { get; set; }
        public string LogoUrl { get; set; }



        public string LocationPhone { get; set; }

        DateTime PurchaseDateTime { get; set; }

        DateTime ExpiryDate { get; set; }

    }
 
}
