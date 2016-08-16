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
    
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AdCampaignResponses = new HashSet<AdCampaignResponse>();
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.CompaniesAspNetUsers = new HashSet<CompaniesAspNetUser>();
            this.CouponCodes = new HashSet<CouponCode>();
            this.ProfileQuestionUserAnswers = new HashSet<ProfileQuestionUserAnswer>();
            this.SurveyQuestions = new HashSet<SurveyQuestion>();
            this.SurveyQuestionResponses = new HashSet<SurveyQuestionResponse>();
            this.AspNetRoles = new HashSet<AspNetRole>();
            this.Apps = new HashSet<App>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
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
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public Nullable<long> EducationId { get; set; }
        public string ProfileImage { get; set; }
        public string UserCode { get; set; }
        public string SmsCode { get; set; }
        public string WebsiteLink { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string authenticationToken { get; set; }
    
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual Company Company { get; set; }
        public virtual Education Education { get; set; }
        public virtual Industry Industry { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }
        public virtual ICollection<CouponCode> CouponCodes { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<App> Apps { get; set; }
    }
}