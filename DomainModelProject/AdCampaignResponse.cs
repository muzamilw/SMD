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
    
    public partial class AdCampaignResponse
    {
        public int ResponseID { get; set; }
        public Nullable<long> CampaignID { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UserID { get; set; }
        public Nullable<double> EndUserDollarAmount { get; set; }
        public Nullable<int> SkipCount { get; set; }
        public Nullable<int> UserSelection { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string GameTime { get; set; }
        public Nullable<int> GameScore { get; set; }
        public Nullable<int> GameLevel { get; set; }
        public Nullable<int> GameId { get; set; }
        public Nullable<int> UserQuestionResponse { get; set; }
        public string UserLocationLat { get; set; }
        public string UserLocationLong { get; set; }
        public string UserLocationCity { get; set; }
        public string UserLocationCountry { get; set; }
        public string UserLocationAddress { get; set; }
        public Nullable<int> ResponseType { get; set; }
    
        public virtual AdCampaign AdCampaign { get; set; }
        public virtual AdCampaignResponse AdCampaignResponse1 { get; set; }
        public virtual AdCampaignResponse AdCampaignResponse2 { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Company Company { get; set; }
    }
}
