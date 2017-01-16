using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class SearchCoupons_Result
    {
        public long CouponId { get; set; }
        public string CouponTitle { get; set; }
        public string CouponImage1 { get; set; }
        public string LogoUrl { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public Nullable<double> SwapCost { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CouponActiveMonth { get; set; }
        public Nullable<int> CouponActiveYear { get; set; }

        public Nullable<int> TotalItems { get; set; }

        public Nullable<DateTime> eod { get; set; }

        public Nullable<DateTime> strt { get; set; }

        public Nullable<double> distance { get; set; }

        public string CompanyName { get; set; }
        public string LocationTitle { get; set; }
        public string LocationCity { get; set; }

        public Nullable<int> DealsCount { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public double AvgRating { get; set; }

        public int UserHasRated { get; set; }

        public Nullable<int> DaysLeft { get; set; }

        public string CouponLink { get; set; }
        public int IsTwoForOneDeal { get; set; }
    }
}
