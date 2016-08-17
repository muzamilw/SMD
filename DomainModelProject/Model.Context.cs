﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SMDDevEntities : DbContext
    {
        public SMDDevEntities()
            : base("name=SMDDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AdCampaign> AdCampaigns { get; set; }
        public virtual DbSet<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual DbSet<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }
        public virtual DbSet<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }
        public virtual DbSet<App> Apps { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<BranchCategory> BranchCategories { get; set; }
        public virtual DbSet<CampaignCategory> CampaignCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryLog> CategoryLogs { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyBranch> CompanyBranches { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponCode> CouponCodes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomUrl> CustomUrls { get; set; }
        public virtual DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EmailQueue> EmailQueues { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProfileQuestion> ProfileQuestions { get; set; }
        public virtual DbSet<ProfileQuestionAnswer> ProfileQuestionAnswers { get; set; }
        public virtual DbSet<ProfileQuestionGroup> ProfileQuestionGroups { get; set; }
        public virtual DbSet<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
        public virtual DbSet<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }
        public virtual DbSet<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<SystemMail> SystemMails { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }
        public virtual DbSet<UserFavouriteCoupon> UserFavouriteCoupons { get; set; }
        public virtual DbSet<CouponCategories> CouponCategories { get; set; }
        public virtual DbSet<CouponCategory1> CouponCategories1 { get; set; }
        public virtual DbSet<Phrase> Phrases { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<UserPurchasedCoupon> UserPurchasedCoupons { get; set; }
        public virtual DbSet<DamImage> DamImages { get; set; }
    
        public virtual ObjectResult<SearchCoupons_Result> SearchCoupons(Nullable<int> categoryId, Nullable<int> type, string keywords, Nullable<int> distance, string lat, string lon, string userId, Nullable<int> fromRow, Nullable<int> toRow)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            var distanceParameter = distance.HasValue ?
                new ObjectParameter("distance", distance) :
                new ObjectParameter("distance", typeof(int));
    
            var latParameter = lat != null ?
                new ObjectParameter("Lat", lat) :
                new ObjectParameter("Lat", typeof(string));
    
            var lonParameter = lon != null ?
                new ObjectParameter("Lon", lon) :
                new ObjectParameter("Lon", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            var fromRowParameter = fromRow.HasValue ?
                new ObjectParameter("FromRow", fromRow) :
                new ObjectParameter("FromRow", typeof(int));
    
            var toRowParameter = toRow.HasValue ?
                new ObjectParameter("ToRow", toRow) :
                new ObjectParameter("ToRow", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchCoupons_Result>("SearchCoupons", categoryIdParameter, typeParameter, keywordsParameter, distanceParameter, latParameter, lonParameter, userIdParameter, fromRowParameter, toRowParameter);
        }
    
        public virtual ObjectResult<SearchCampaigns_Result> SearchCampaigns(Nullable<int> status, string keyword, Nullable<int> companyId, Nullable<int> fromRow, Nullable<int> toRow, Nullable<bool> adminMode)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(int));
    
            var keywordParameter = keyword != null ?
                new ObjectParameter("keyword", keyword) :
                new ObjectParameter("keyword", typeof(string));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            var fromRowParameter = fromRow.HasValue ?
                new ObjectParameter("fromRow", fromRow) :
                new ObjectParameter("fromRow", typeof(int));
    
            var toRowParameter = toRow.HasValue ?
                new ObjectParameter("toRow", toRow) :
                new ObjectParameter("toRow", typeof(int));
    
            var adminModeParameter = adminMode.HasValue ?
                new ObjectParameter("adminMode", adminMode) :
                new ObjectParameter("adminMode", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchCampaigns_Result>("SearchCampaigns", statusParameter, keywordParameter, companyIdParameter, fromRowParameter, toRowParameter, adminModeParameter);
        }
    }
}
