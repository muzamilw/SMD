using System;
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Models.IdentityModels
{
    /// <summary>
    /// User Domain Model
    /// </summary>
    public partial class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AlternateEmail { get; set; }
        public string IsEmailVerified { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Jobtitle { get; set; }
        public string ContactNotes { get; set; }
        public bool IsSubscribed { get; set; }
        public int? AppId { get; set; }
        public string CompanyName { get; set; }
        public string SalesEmail { get; set; } // till here 
        public string CompanyRepresentative { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserTimeZone { get; set; }
        public string ReferralCode { get; set; }
        public bool? AfilliatianStatus { get; set; }
        public string ChargeBeeCustomerId { get; set; }
        public string ChargeBeesubscriptionId { get; set; }
        public bool? RegisteredViaReferral { get; set; }
        public string ReferringUserId { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
    }
}
