//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModelProject
{
    using System;
    
    public partial class SearchCoupons_Result
    {
        public long CouponId { get; set; }
        public string CouponTitle { get; set; }
        public string CouponImage1 { get; set; }
        public string LogoUrl { get; set; }
        public double Price { get; set; }
        public double Savings { get; set; }
        public double SwapCost { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CouponActiveMonth { get; set; }
        public Nullable<int> CouponActiveYear { get; set; }
        public Nullable<int> TotalItems { get; set; }
        public Nullable<System.DateTime> eod { get; set; }
        public Nullable<System.DateTime> strt { get; set; }
        public Nullable<double> distance { get; set; }
        public string CompanyName { get; set; }
        public string LocationTitle { get; set; }
        public string LocationCity { get; set; }
        public Nullable<int> DealsCount { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public int AvgRating { get; set; }
    }
}
