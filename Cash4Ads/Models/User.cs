using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Cash4Ads.Models
{
    public partial class User
    {
        #region Persisted Properties
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AlternateEmail { get; set; }
        public string IsEmailVerified { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Jobtitle { get; set; }
        public string ContactNotes { get; set; }
        public bool IsSubscribed { get; set; }
        public Nullable<int> AppID { get; set; }
        public Nullable<bool> IsCompanyRepresentative { get; set; }

        public string UserTimeZone { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public Nullable<int> IndustryId { get; set; }
        public Nullable<long> EducationId { get; set; }
        public string ProfileImage { get; set; }
        public string UserCode { get; set; }
        public string SmsCode { get; set; }
        public string WebsiteLink { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string AuthenticationToken { get; set; }

        
        #endregion

    }
    public partial class User : IUser<string>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, string> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}