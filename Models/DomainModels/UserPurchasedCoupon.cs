//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMD.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPurchasedCoupon
    {
        public long CouponPurchaseId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public Nullable<System.DateTime> PurchaseDateTime { get; set; }
        public Nullable<double> PurchaseAmount { get; set; }
        public string UserId { get; set; }
        public Nullable<bool> IsRedeemed { get; set; }
        public Nullable<System.DateTime> RedemptionDateTime { get; set; }
        public string RedemptionOperator { get; set; }
    
        public virtual Coupon Coupon { get; set; }
    }
}
