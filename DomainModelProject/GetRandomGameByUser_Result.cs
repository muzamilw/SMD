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
    
    public partial class GetRandomGameByUser_Result
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> AgeRangeStart { get; set; }
        public Nullable<int> AgeRangeEnd { get; set; }
        public Nullable<int> GameType { get; set; }
        public Nullable<int> Complexity { get; set; }
        public string GameUrl { get; set; }
        public string GameInstructions { get; set; }
        public string GameSmallImage { get; set; }
        public string GameLargeImage { get; set; }
        public Nullable<double> PlayTime { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<double> Accuracy { get; set; }
    }
}
