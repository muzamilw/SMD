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
    
    public partial class AdCampaignClickRateHistory
    {
        public long ClickRateId { get; set; }
        public Nullable<long> CampaignID { get; set; }
        public Nullable<double> ClickRate { get; set; }
        public Nullable<System.DateTime> RateChangeDateTime { get; set; }
    
        public virtual AdCampaign AdCampaign { get; set; }
    }
}
