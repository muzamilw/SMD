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
    using System.Collections.Generic;
    
    public partial class CouponCategories
    {
        public long Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<long> CouponId { get; set; }
    
        public virtual Coupon Coupon { get; set; }
        public virtual CouponCategory1 CouponCategory1 { get; set; }
    }
}
