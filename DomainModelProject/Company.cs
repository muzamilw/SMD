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
    
    public partial class Company
    {
        public Company()
        {
            this.Accounts = new HashSet<Account>();
            this.AdCampaigns = new HashSet<AdCampaign>();
            this.AdCampaignResponses = new HashSet<AdCampaignResponse>();
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.BranchCategories = new HashSet<BranchCategory>();
            this.CompaniesAspNetUsers = new HashSet<CompaniesAspNetUser>();
            this.CompanyBranches = new HashSet<CompanyBranch>();
            this.Coupons = new HashSet<Coupon>();
            this.Invoices = new HashSet<Invoice>();
            this.ProfileQuestionUserAnswers = new HashSet<ProfileQuestionUserAnswer>();
            this.SurveyQuestions = new HashSet<SurveyQuestion>();
            this.SurveyQuestionResponses = new HashSet<SurveyQuestionResponse>();
            this.DamImages = new HashSet<DamImage>();
            this.ProfileQuestions = new HashSet<ProfileQuestion>();
        }
    
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ReplyEmail { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Logo { get; set; }
        public string StripeCustomerId { get; set; }
        public string ChargeBeesubscriptionID { get; set; }
        public Nullable<bool> RegisteredViaReferral { get; set; }
        public Nullable<int> ReferringCompanyID { get; set; }
        public string PaypalCustomerId { get; set; }
        public string GoogleWalletCustomerId { get; set; }
        public Nullable<int> PreferredPayoutAccount { get; set; }
        public string SalesEmail { get; set; }
        public string ReferralCode { get; set; }
        public Nullable<bool> AfilliatianStatus { get; set; }
        public string WebsiteLink { get; set; }
        public Nullable<int> CompanyType { get; set; }
        public string VoucherSecretKey { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingState { get; set; }
        public Nullable<int> BillingCountryId { get; set; }
        public Nullable<int> BillingCityId { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string TwitterHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string InstagramHandle { get; set; }
        public string PinterestHandle { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<BranchCategory> BranchCategories { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<CompanyBranch> CompanyBranches { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
        public virtual ICollection<DamImage> DamImages { get; set; }
        public virtual ICollection<ProfileQuestion> ProfileQuestions { get; set; }
    }
}
