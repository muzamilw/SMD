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
    
    public partial class CompaniesAspNetUser
    {
        public long Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> Status { get; set; }
        public string InvitationCode { get; set; }
        public string RoleId { get; set; }
        public string InvitationEmail { get; set; }
    
        public virtual AspNetRole AspNetRole { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Company Company { get; set; }
    }
}
