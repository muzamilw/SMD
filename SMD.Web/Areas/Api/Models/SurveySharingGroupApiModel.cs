using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public partial class SurveySharingGroupApiModel
    {
        
        public long SharingGroupId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string UserId { get; set; }
        public string GroupName { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }

        public virtual ICollection<SurveySharingGroupMemberApiModel> SurveySharingGroupMembers { get; set; }

        public virtual ICollection<SurveySharingGroupMemberApiModel> SurveySharingGroupAddedMembers { get; set; }

        public virtual ICollection<SurveySharingGroupMemberApiModel> SurveySharingGroupDeletedMembers { get; set; }
    }




    public partial class SurveySharingGroupMemberApiModel
    {
        public long SharingGroupMemberId { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> MemberStatus { get; set; }
        public Nullable<long> SharingGroupId { get; set; }

        public string FullName { get; set; }

               
    }
}