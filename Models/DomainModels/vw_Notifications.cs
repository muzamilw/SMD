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
    
    public partial class vw_Notifications
    {
        public long ID { get; set; }
        public Nullable<int> Type { get; set; }
        public string UserID { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<System.DateTime> GeneratedOn { get; set; }
        public string GeneratedBy { get; set; }
        public Nullable<long> SurveyQuestionShareId { get; set; }
        public string PhoneNumber { get; set; }
        public string NotificationDetails { get; set; }
        public string PollTitle { get; set; }

        public Nullable<long> SSQID { get; set; }

        public Nullable<long> CouponId { get; set; }
       public string DealTitle { get; set; }

         public double DealPrice { get; set; }
        public double DealSavings { get; set; }
        public int DealCount { get; set; }
        public string DealDescription { get; set; }
        public string DealCompany { get; set; }
        public string DealCity { get; set; }

        public string ProfileImage { get; set; }
    }
}
