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
    
    public partial class SurveySharingGroupMember
    {
        public long SharingGroupMemberId { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> MemberStatus { get; set; }
        public Nullable<long> SharingGroupId { get; set; }

        public string FullName { get; set; }
        
    
        public virtual SurveySharingGroup SurveySharingGroup { get; set; }

        public virtual ICollection<SurveySharingGroupShare> SurveySharingGroupShares { get; set; }
    }
}
