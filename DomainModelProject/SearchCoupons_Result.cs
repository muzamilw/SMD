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
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public Nullable<double> SwapCost { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CouponActiveMonth { get; set; }
        public Nullable<int> CouponActiveYear { get; set; }
        public Nullable<int> TotalItems { get; set; }
    }
}
