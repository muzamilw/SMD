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
    
    public partial class CampaignCategory
    {
        public long CampaignId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
    
        public virtual AdCampaign AdCampaign { get; set; }
        public virtual CouponCategory1 CouponCategory { get; set; }
    }
}