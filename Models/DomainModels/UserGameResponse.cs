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
    using SMD.Models.IdentityModels;
    using System;
    using System.Collections.Generic;
    
    public partial class UserGameResponse
    {
        public long UserGameResponseId { get; set; }
        public string UserId { get; set; }
        public Nullable<long> GameId { get; set; }
        public Nullable<System.DateTime> ResponseDateTime { get; set; }
        public Nullable<double> PlayTime { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<double> Accuracy { get; set; }
        public Nullable<int> AdCampaignResponseID { get; set; }
    
        public virtual AdCampaignResponse AdCampaignResponse { get; set; }
        public virtual User AspNetUser { get; set; }
        public virtual Game Game { get; set; }
    }
}
