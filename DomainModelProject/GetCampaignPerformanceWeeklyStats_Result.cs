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
    
    public partial class GetCampaignPerformanceWeeklyStats_Result
    {
        public string userid { get; set; }
        public Nullable<int> companyid { get; set; }
        public Nullable<int> type { get; set; }
        public long campaignid { get; set; }
        public string CampaignName { get; set; }
        public Nullable<int> ClickThroughsLastWeek { get; set; }
        public Nullable<int> ClickThroughsPreviousWeek { get; set; }
        public Nullable<int> ProgressPercentage { get; set; }
        public Nullable<int> AnsweredLastWeek { get; set; }
        public Nullable<int> AnsweredPreviousWeek { get; set; }
    }
}
