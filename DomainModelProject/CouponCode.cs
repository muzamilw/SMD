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
    
    public partial class CouponCode
    {
        public long CodeId { get; set; }
        public Nullable<long> CampaignId { get; set; }
        public string Code { get; set; }
        public Nullable<bool> IsTaken { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> TakenDateTime { get; set; }
    
        public virtual AdCampaign AdCampaign { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
