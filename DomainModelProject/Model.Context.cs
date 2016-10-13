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
        public virtual DbSet<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }
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
        public virtual DbSet<UserCouponView> UserCouponViews { get; set; }
        public virtual DbSet<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }
        public virtual DbSet<ProfileQuestionTargetLocation> ProfileQuestionTargetLocations { get; set; }
        public virtual DbSet<CouponPriceOption> CouponPriceOptions { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PayOutHistory> PayOutHistories { get; set; }
        public virtual DbSet<vw_Coupons> vw_Coupons { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CampaignEventHistory> CampaignEventHistories { get; set; }
        public virtual DbSet<EventStatus> EventStatuses { get; set; }
        public virtual DbSet<vw_ReferringCompanies> vw_ReferringCompanies { get; set; }
        public virtual DbSet<AdCampaignClickRateHistory> AdCampaignClickRateHistories { get; set; }
    
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
    
        public virtual ObjectResult<GetCouponByID_Result> GetCouponByID(Nullable<long> couponId, string lat, string lon, string userId)
        {
            var couponIdParameter = couponId.HasValue ?
                new ObjectParameter("CouponId", couponId) :
                new ObjectParameter("CouponId", typeof(long));
    
            var latParameter = lat != null ?
                new ObjectParameter("Lat", lat) :
                new ObjectParameter("Lat", typeof(string));
    
            var lonParameter = lon != null ?
                new ObjectParameter("Lon", lon) :
                new ObjectParameter("Lon", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCouponByID_Result>("GetCouponByID", couponIdParameter, latParameter, lonParameter, userIdParameter);
        }
    
        public virtual ObjectResult<GetProducts_Result> GetProducts(string userID, Nullable<int> fromRow, Nullable<int> toRow)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var fromRowParameter = fromRow.HasValue ?
                new ObjectParameter("FromRow", fromRow) :
                new ObjectParameter("FromRow", typeof(int));
    
            var toRowParameter = toRow.HasValue ?
                new ObjectParameter("ToRow", toRow) :
                new ObjectParameter("ToRow", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProducts_Result>("GetProducts", userIDParameter, fromRowParameter, toRowParameter);
        }
    
        public virtual ObjectResult<GetTransactions_Result> GetTransactions(Nullable<int> companyID, Nullable<int> accountType, Nullable<int> rows)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var accountTypeParameter = accountType.HasValue ?
                new ObjectParameter("AccountType", accountType) :
                new ObjectParameter("AccountType", typeof(int));
    
            var rowsParameter = rows.HasValue ?
                new ObjectParameter("Rows", rows) :
                new ObjectParameter("Rows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTransactions_Result>("GetTransactions", companyIDParameter, accountTypeParameter, rowsParameter);
        }
    
        public virtual ObjectResult<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAdminDashBoardInsights_Result>("GetAdminDashBoardInsights");
        }
    
        public virtual ObjectResult<GetRevenueOverTime_Result> GetRevenueOverTime(Nullable<int> companyId, Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRevenueOverTime_Result>("GetRevenueOverTime", companyIdParameter, dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<GetSurvayQestionsAnswered_Result> GetSurvayQestionsAnswered(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSurvayQestionsAnswered_Result>("GetSurvayQestionsAnswered", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual int getApprovedCampaignsOverTime(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getApprovedCampaignsOverTime", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<GetDealMetric_Result> GetDealMetric()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDealMetric_Result>("GetDealMetric");
        }
    
        public virtual ObjectResult<GetSurvayCardsAnswered_Result> GetSurvayCardsAnswered(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSurvayCardsAnswered_Result>("GetSurvayCardsAnswered", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getActiveCampaignsOverTime_Result> getActiveCampaignsOverTime(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getActiveCampaignsOverTime_Result>("getActiveCampaignsOverTime", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getCampaignsByStatus_Result> getCampaignsByStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCampaignsByStatus_Result>("getCampaignsByStatus");
        }
    
        public virtual ObjectResult<GetUserCounts_Result> GetUserCounts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserCounts_Result>("GetUserCounts");
        }
    
        public virtual ObjectResult<getUserActivitiesOverTime_Result> getUserActivitiesOverTime(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getUserActivitiesOverTime_Result>("getUserActivitiesOverTime", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(Nullable<int> campaignType, Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var campaignTypeParameter = campaignType.HasValue ?
                new ObjectParameter("CampaignType", campaignType) :
                new ObjectParameter("CampaignType", typeof(int));
    
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLiveCampaignCountOverTime_Result>("GetLiveCampaignCountOverTime", campaignTypeParameter, dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getPayoutVSRevenueOverTime_Result> getPayoutVSRevenueOverTime(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPayoutVSRevenueOverTime_Result>("getPayoutVSRevenueOverTime", dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<GetRevenueByCampaignOverTime_Result> GetRevenueByCampaignOverTime(Nullable<int> companyId, Nullable<int> campaignType, Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> granularity)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var campaignTypeParameter = campaignType.HasValue ?
                new ObjectParameter("CampaignType", campaignType) :
                new ObjectParameter("CampaignType", typeof(int));
    
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRevenueByCampaignOverTime_Result>("GetRevenueByCampaignOverTime", companyIdParameter, campaignTypeParameter, dateFromParameter, dateToParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getAdsCampaignByCampaignId_Result> getAdsCampaignByCampaignId(Nullable<int> campaignId, Nullable<int> status, Nullable<int> dateRange, Nullable<int> granularity)
        {
            var campaignIdParameter = campaignId.HasValue ?
                new ObjectParameter("CampaignId", campaignId) :
                new ObjectParameter("CampaignId", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var dateRangeParameter = dateRange.HasValue ?
                new ObjectParameter("DateRange", dateRange) :
                new ObjectParameter("DateRange", typeof(int));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAdsCampaignByCampaignId_Result>("getAdsCampaignByCampaignId", campaignIdParameter, statusParameter, dateRangeParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getDealByCouponID_Result> getDealByCouponID(Nullable<int> couponID, Nullable<int> dateRange, Nullable<int> granularity)
        {
            var couponIDParameter = couponID.HasValue ?
                new ObjectParameter("CouponID", couponID) :
                new ObjectParameter("CouponID", typeof(int));
    
            var dateRangeParameter = dateRange.HasValue ?
                new ObjectParameter("DateRange", dateRange) :
                new ObjectParameter("DateRange", typeof(int));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDealByCouponID_Result>("getDealByCouponID", couponIDParameter, dateRangeParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getPollsBySQID_Result> getPollsBySQID(Nullable<int> sQID, Nullable<int> status, Nullable<int> dateRange, Nullable<int> granularity)
        {
            var sQIDParameter = sQID.HasValue ?
                new ObjectParameter("SQID", sQID) :
                new ObjectParameter("SQID", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var dateRangeParameter = dateRange.HasValue ?
                new ObjectParameter("DateRange", dateRange) :
                new ObjectParameter("DateRange", typeof(int));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPollsBySQID_Result>("getPollsBySQID", sQIDParameter, statusParameter, dateRangeParameter, granularityParameter);
        }
    
        public virtual ObjectResult<getSurvayByPQID_Result> getSurvayByPQID(Nullable<int> pQId, Nullable<int> status, Nullable<int> dateRange, Nullable<int> granularity)
        {
            var pQIdParameter = pQId.HasValue ?
                new ObjectParameter("PQId", pQId) :
                new ObjectParameter("PQId", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var dateRangeParameter = dateRange.HasValue ?
                new ObjectParameter("DateRange", dateRange) :
                new ObjectParameter("DateRange", typeof(int));
    
            var granularityParameter = granularity.HasValue ?
                new ObjectParameter("Granularity", granularity) :
                new ObjectParameter("Granularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSurvayByPQID_Result>("getSurvayByPQID", pQIdParameter, statusParameter, dateRangeParameter, granularityParameter);
        }
    }
}
