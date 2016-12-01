using System.Data.Entity.Core.Objects;
using Microsoft.Practices.Unity;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using SMD.Models.RequestModels;

namespace SMD.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    [Serializable]
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private IUnityContainer container;
        #endregion

        #region Protected


        #endregion

        #region Constructor
        public BaseDbContext()
        {
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty(object entity, string propertyName, bool isCollection = false)
        {
            if (!isCollection)
            {
                Entry(entity).Reference(propertyName).Load();
            }
            else
            {
                Entry(entity).Collection(propertyName).Load();
            }
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            string propertyName = PropertyReference.GetPropertyName(propertyExpression);
            LoadProperty(entity, propertyName, isCollection);
        }

        #endregion

        #region Public

        public BaseDbContext(IUnityContainer container, string connectionString)
            : base(connectionString)
        {
            this.container = container;
        }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// User Logins
        /// </summary>
        public DbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// User Claims
        /// </summary>
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriteria { get; set; }
        public DbSet<ProfileQuestionTargetLocation> ProfileQuestionTargetLocation { get; set; }
        /// <summary>
        /// Profile Questions
        /// </summary>
        public DbSet<ProfileQuestion> ProfileQuestions { get; set; }

        /// <summary>
        /// Profile Questions Groups
        /// </summary>
        public DbSet<ProfileQuestionGroup> ProfileQuestionGroups { get; set; }

        /// <summary>
        /// Countries
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Languages
        /// </summary>
        public DbSet<Language> Languages { get; set; }
        /// <summary>
        /// Companies
        /// </summary>
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyBranch> CompanyBranches { get; set; }
        /// <summary>
        /// Educations
        /// </summary>
        public DbSet<Education> Educations { get; set; }
        /// <summary>
        /// Ad Campaign Response
        /// </summary>
        public DbSet<AdCampaignResponse> AdCampaignResponses { get; set; }


        public DbSet<AdCampaignClickRateHistory> AdCampaignClickRateHistory { get; set; }

        /// <summary>
        /// AdCampaign Target Criterias
        /// </summary>
        public DbSet<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }

        /// <summary>
        /// AdCampaign Target Locations
        /// </summary>
        public DbSet<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }


        public DbSet<CampaignEventHistory> CampaignEventHistory { get; set; }


        /// <summary>
        /// Survey Question
        /// </summary>
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        /// <summary>
        /// Survery Question Target Criterias
        /// </summary>
        public DbSet<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }

        /// <summary>
        /// Survey Question Target Locations
        /// </summary>
        public DbSet<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }

        /// <summary>
        /// System Mails
        /// </summary>
        public DbSet<SystemMail> SystemMails { get; set; }

        /// <summary>
        /// System Mails
        /// </summary>
        public DbSet<EmailQueue> EmailQueues { get; set; }

        /// <summary>
        /// Ad Campaigns
        /// </summary>
        public DbSet<AdCampaign> AdCampaigns { get; set; }
        public DbSet<Coupon> Coupons { get; set; }


        public DbSet<vw_Coupons> vw_Coupons { get; set; }
        public DbSet<CouponRatingReview> CouponRatingReview { get; set; }



        public DbSet<vw_ReferringCompanies> vwvw_ReferringCompanies { get; set; }

        /// <summary>
        /// Profile Question Answers
        /// </summary>
        public DbSet<ProfileQuestionAnswer> ProfileQuestionAnswers { get; set; }

        /// <summary>
        /// Suvery Question Responses
        /// </summary>
        public DbSet<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }

        /// <summary>
        /// Audit Logs
        /// </summary>
        public DbSet<AuditLog> AuditLogs { get; set; }

        /// <summary>
        /// cities
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Profile Question User Answers
        /// </summary>
        public DbSet<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }

        /// <summary>
        /// Industry
        /// </summary>
        public DbSet<Industry> Industries { get; set; }
        /// <summary>
        /// Product
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Account
        /// </summary>
        public DbSet<Account> Accounts { get; set; }
        /// <summary>
        /// Transaction
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Invoice 
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<PayOutHistory> PayOutHistory { get; set; }



        /// <summary>
        /// Invoice Details
        /// </summary>
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        /// <summary>
        /// Taxes
        /// </summary>
        public DbSet<Tax> Taxes { get; set; }

        /// <summary>
        /// Taxes
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Taxes
        /// </summary>
        public DbSet<CustomUrl> CustomUrls { get; set; }
        //public DbSet<CouponCode> CouponCodes { get; set; }

        /// <summary>
        /// Transaction
        /// </summary>
        public DbSet<CampaignCategory> CampaignCategories { get; set; }

        /// <summary>
        /// Transaction Log
        /// </summary>
        public DbSet<TransactionLog> TransactionLogs { get; set; }

        public DbSet<CouponCategory> CouponCategory { get; set; }


        public DbSet<CouponCategories> CouponCategories { get; set; }
        public DbSet<UserFavouriteCoupon> UserFavouriteCoupons { get; set; }

        public DbSet<UserPurchasedCoupon> UserPurchasedCoupon { get; set; }

        public DbSet<UserCouponView> UserCouponView { get; set; }

        public DbSet<vw_GetUserTransactions> vw_GetUserTransactions { get; set; }
        public DbSet<vw_PublisherTransaction> vw_PublisherTransaction { get; set; }
        public DbSet<vw_Cash4AdsReport> vw_Cash4AdsReport { get; set; }


        public DbSet<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }

        public DbSet<vw_CompanyUsers> vw_CompanyUsers { get; set; }

        public DbSet<Phrase> Phrase { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<BranchCategory> BranchCategories { get; set; }

        public DbSet<DamImage> DamImage { get; set; }



        public DbSet<CouponPriceOption> CouponPriceOption { get; set; }


        public DbSet<SurveySharingGroup> SurveySharingGroup { get; set; }
        public DbSet<SurveySharingGroupMember> SurveySharingGroupMember { get; set; }
        public DbSet<Notification> Notification { get; set; }


        public DbSet<vw_Notifications> vw_Notifications { get; set; }
        public DbSet<SurveySharingGroupShare> SurveySharingGroupShare { get; set; }
        public DbSet<SharedSurveyQuestion> SharedSurveyQuestion { get; set; }

        public DbSet<AspNetUsersNotificationToken> AspNetUsersNotificationToken { get; set; }



        /// <summary>
        /// Get Ad-Campaigns for APIs | baqer
        /// </summary>
        public ObjectResult<GetActiveVSNewUsers_Result> GetActiveVSNewUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetActiveVSNewUsers_Result>("GetActiveVSNewUsers");
        }
        public ObjectResult<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAdminDashBoardInsights_Result>("GetAdminDashBoardInsights");
        }
        public ObjectResult<getCampaignsByStatus_Result> getCampaignsByStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCampaignsByStatus_Result>("getCampaignsByStatus");
        }
        public ObjectResult<GetUserCounts_Result> GetUserCounts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserCounts_Result>("GetUserCounts");
        }
        //
        public ObjectResult<getPayoutVSRevenueOverTime_Result> getPayoutVSRevenueOverTime(DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            var DateFrm = new ObjectParameter("DateFrom", DateFrom);
            var DatTo = new ObjectParameter("DateTo", DateTo);
            var Granulrty = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPayoutVSRevenueOverTime_Result>("getPayoutVSRevenueOverTime", DateFrm, DatTo, Granulrty);
        }
        public ObjectResult<getUserActivitiesOverTime_Result> getUserActivitiesOverTime(DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            var DateFrm = new ObjectParameter("DateFrom", DateFrom);
            var DatTo = new ObjectParameter("DateTo", DateTo);
            var Granulrty = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getUserActivitiesOverTime_Result>("getUserActivitiesOverTime", DateFrm, DatTo, Granulrty);
        }
        public ObjectResult<GetRevenueOverTime_Result> GetRevenueOverTime(int CompanyId, DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            var CID = new ObjectParameter("CompanyId", CompanyId);
            var DateFrm = new ObjectParameter("DateFrom", DateFrom);
            var DatTo = new ObjectParameter("DateTo", DateTo);
            var Granulrty = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRevenueOverTime_Result>("GetRevenueOverTime", CID, DateFrm, DatTo, Granulrty);
        }
        public ObjectResult<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            var CID = new ObjectParameter("CampaignType", CampaignType);
            var DateFrm = new ObjectParameter("DateFrom", DateFrom);
            var DatTo = new ObjectParameter("DateTo", DateTo);
            var Granulrty = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLiveCampaignCountOverTime_Result>("GetLiveCampaignCountOverTime", CID, DateFrm, DatTo, Granulrty);
        }
        public ObjectResult<GetRevenueByCampaignOverTime_Result> GetRevenueByCampaignOverTime(int CompanyId, int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            var CID = new ObjectParameter("CompanyId", CompanyId);
            var Ctype = new ObjectParameter("CampaignType", CampaignType);
            var DateFrm = new ObjectParameter("DateFrom", DateFrom);
            var DatTo = new ObjectParameter("DateTo", DateTo);
            var Granulrty = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRevenueByCampaignOverTime_Result>("GetRevenueByCampaignOverTime", Ctype, CID, DateFrm, DatTo, Granulrty);
        }

        /// <summary>
        /// Get Ad-Campaigns for APIs | baqer
        /// </summary>
        public ObjectResult<GetAds_Result> GetAdCompaignForApi(string userId, int fromRow, int toRow)
        {
            var uId = new ObjectParameter("UserID", userId);
            var fRow = new ObjectParameter("FromRow", fromRow);
            var tRow = new ObjectParameter("ToRow", toRow);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAds_Result>("GetAds", uId, fRow, tRow);
        }


        /// <summary>
        /// Get Surveys for APIs | baqer
        /// </summary>
        public ObjectResult<GetSurveys_Result> GetSurveysForApi(string userId, int fromRow, int toRow)
        {
            var uId = new ObjectParameter("UserID", userId);
            var fRow = new ObjectParameter("FromRow", fromRow);
            var tRow = new ObjectParameter("ToRow", toRow);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSurveys_Result>("GetSurveys", uId, fRow, tRow);
        }


        /// <summary>
        /// Get Surveys matching count for APIs | baqer
        /// </summary>
        public ObjectResult<Int32> GetAudienceSurveyCount(GetAudienceSurveyRequest request)
        {
            var countryId = new ObjectParameter("countryId", request.CountryId);
            var cityId = new ObjectParameter("cityId", request.CityId);
            var languageId = new ObjectParameter("languageId", request.LanguageId);
            var industryId = new ObjectParameter("industryId", request.IndustryId);
            var gender = new ObjectParameter("gender", request.Gender);
            var age = new ObjectParameter("age", request.Age);
            var pqIds = new ObjectParameter("profileQuestionIds", request.IdsList);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>
                ("GetAudienceSurvey", age, gender, countryId, cityId, languageId, industryId, pqIds);
        }


        /// <summary>
        /// Get AdCampaign matching count for APIs | baqer
        /// </summary>
        public ObjectResult<Int32> GetAudienceAdCampaignCount(GetAudienceSurveyRequest request)
        {
            var countryId = new ObjectParameter("countryId", request.CountryId);
            var cityId = new ObjectParameter("cityId", request.CityId);
            var languageId = new ObjectParameter("languageId", request.LanguageId);
            var industryId = new ObjectParameter("industryId", request.IndustryId);
            var gender = new ObjectParameter("gender", request.Gender);
            var age = new ObjectParameter("age", request.Age);
            var pqIds = new ObjectParameter("profileQuestionIds", request.IdsList);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>
               ("GetAudienceAdCampaign", age, gender, countryId, cityId, languageId, industryId, pqIds);
        }

        public ObjectResult<GetAudience_Result> GetAudienceCampaignAndSurveyCounts(int ageFrom, int ageTo, int gender, string countryIds, string cityIds, string languageIds,
            string industryIds, string profileQuestionIds, string profileAnswerIds, string surveyQuestionIds
            , string surveyAnswerIds, string countryIdsExcluded, string cityIdsExcluded,
            string languageIdsExcluded, string industryIdsExcluded
            , string educationIds
            , string educationIdsExcluded
            , string profileQuestionIdsExcluded
            , string surveyQuestionIdsExcluded
            , string CampaignQuizIds
            , string CampaignQuizAnswerIds
            , string CampaignQuizIdsExcluded)
        {
            var ageFromParameter = new ObjectParameter("ageFrom", ageFrom);
            var ageToParameter = new ObjectParameter("ageTo", ageTo);
            var genderParameter = new ObjectParameter("gender", gender);
            var countryIdsParameter = new ObjectParameter("countryIds", countryIds);
            var cityIdsParameter = new ObjectParameter("cityIds", cityIds);
            var languageIdsParameter = new ObjectParameter("languageIds", languageIds);
            var industryIdsParameter = new ObjectParameter("industryIds", industryIds);
            var profileQuestionIdsParameter = new ObjectParameter("profileQuestionIds", profileQuestionIds);
            var profileAnswerIdsParameter = new ObjectParameter("profileAnswerIds", profileAnswerIds);
            var surveyQuestionIdsParameter = new ObjectParameter("surveyQuestionIds", surveyQuestionIds);
            var surveyAnswerIdsParameter = new ObjectParameter("surveyAnswerIds", surveyAnswerIds);
            var countryIdsExcludedParameter = new ObjectParameter("countryIdsExcluded", countryIdsExcluded);
            var cityIdsExcludedParameter = new ObjectParameter("cityIdsExcluded", cityIdsExcluded);
            var languageIdsExcludedParameter = new ObjectParameter("languageIdsExcluded", languageIdsExcluded);
            var industryIdsExcludedParameter = new ObjectParameter("industryIdsExcluded", industryIdsExcluded);
            var educationIdsParameter = new ObjectParameter("educationIds", educationIds);
            var educationIdsExcludedParameter = new ObjectParameter("educationIdsExcluded", educationIdsExcluded);
            var profileQuestionIdsExcludedParameter = new ObjectParameter("profileQuestionIdsExcluded", profileQuestionIdsExcluded);
            var surveyQuestionIdsExcludedParameter = new ObjectParameter("surveyQuestionIdsExcluded", surveyQuestionIdsExcluded);

            var CampaignQuizIdsParameter = new ObjectParameter("CampaignQuizIds", CampaignQuizIds);

            var CampaignQuizAnswerIdsParameter = new ObjectParameter("CampaignQuizAnswerIds", CampaignQuizAnswerIds);

            var CampaignQuizIdsExcludedParameter = new ObjectParameter("CampaignQuizIdsExcluded", CampaignQuizIdsExcluded);


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAudience_Result>("GetAudience",
                ageFromParameter, ageToParameter, genderParameter, countryIdsParameter, cityIdsParameter, languageIdsParameter,
                industryIdsParameter, profileQuestionIdsParameter, profileAnswerIdsParameter, surveyQuestionIdsParameter
                , surveyAnswerIdsParameter, countryIdsExcludedParameter, cityIdsExcludedParameter, languageIdsExcludedParameter
                , industryIdsExcludedParameter
                , educationIdsParameter
                , educationIdsExcludedParameter
                , profileQuestionIdsExcludedParameter
                , surveyQuestionIdsExcludedParameter, CampaignQuizIdsParameter, CampaignQuizAnswerIdsParameter, CampaignQuizIdsExcludedParameter);
        }

        /// <summary>
        /// Gets Combination of Ads, Surveys, Questions as paged view
        /// </summary>
        public ObjectResult<GetProducts_Result> GetProducts(string userId, int? fromRow, int? toRow)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserID", userId) :
                new ObjectParameter("UserID", typeof(string));

            var fromRowParameter = fromRow.HasValue ?
                new ObjectParameter("FromRow", fromRow) :
                new ObjectParameter("FromRow", typeof(int));

            var toRowParameter = toRow.HasValue ?
                new ObjectParameter("ToRow", toRow) :
                new ObjectParameter("ToRow", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProducts_Result>("GetProducts", userIdParameter, fromRowParameter, toRowParameter);


        }

        /// <summary>
        /// Resets User Responses For Ads, Surveys and Questions
        /// </summary>
        public int ResetProductsUserResponses()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResetProductsUserResponses");
        }
        /// <summary>
        /// Gets Coupons
        /// </summary>
        public ObjectResult<GetCoupons_Result> GetCoupons(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserID", userId) :
                new ObjectParameter("UserID", typeof(string));



            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCoupons_Result>("GetCoupons", userIdParameter);
        }



        public ObjectResult<GetCouponsByCompanyId_Result> GetCouponsByCompanyId(string CompanyId)
        {
            var userIdParameter = CompanyId != null ?
                new ObjectParameter("CompanyId", CompanyId) :
                new ObjectParameter("CompanyId", typeof(string));



            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCouponsByCompanyId_Result>("GetCouponsByCompanyId", userIdParameter);
        }




        public ObjectResult<SearchCoupons_Result> SearchCoupons(int categoryId, int type, string keywords, int distance, string Lat, string Lon, string userId, int? fromRow, int? toRow)
        {
            var categoryIdParameter = categoryId != null ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));


            var typeIdParameter = type != null ?
               new ObjectParameter("type", type) :
               new ObjectParameter("type", typeof(int));

            var keywordsParameter = keywords != null ?
            new ObjectParameter("keywords", keywords) :
            new ObjectParameter("keywords", typeof(string));

            var distanceParameter = distance != null ?
          new ObjectParameter("distance", distance) :
          new ObjectParameter("distance", typeof(int));


            var LatParameter = Lat != null ?
      new ObjectParameter("Lat", Lat) :
      new ObjectParameter("Lat", typeof(string));


            var LonParameter = Lon != null ?
      new ObjectParameter("Lon", Lon) :
      new ObjectParameter("Lon", typeof(string));


            var userIdParameter = userId != null ?
             new ObjectParameter("userId", userId) :
             new ObjectParameter("userId", typeof(string));


            var fromRowParameter = fromRow.HasValue ?
                new ObjectParameter("FromRow", fromRow) :
                new ObjectParameter("FromRow", typeof(int));

            var toRowParameter = toRow.HasValue ?
                new ObjectParameter("ToRow", toRow) :
                new ObjectParameter("ToRow", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchCoupons_Result>("SearchCoupons", categoryIdParameter, typeIdParameter, keywordsParameter, distanceParameter, LatParameter, LonParameter, userIdParameter, fromRowParameter, toRowParameter);
        }




        public ObjectResult<GetCouponByID_Result> GetCouponByID(long CouponId, string UserId, string Lat, string Lon)
        {
            var CouponIdIdParameter = CouponId != null ?
                new ObjectParameter("CouponId", CouponId) :
                new ObjectParameter("CouponId", typeof(long));


            var userIdParameter = UserId != null ?
         new ObjectParameter("UserId", UserId) :
         new ObjectParameter("UserId", typeof(string));

            var LatParameter = Lat != null ?
      new ObjectParameter("Lat", Lat) :
      new ObjectParameter("Lat", typeof(string));


            var LonParameter = Lon != null ?
      new ObjectParameter("Lon", Lon) :
      new ObjectParameter("Lon", typeof(string));



            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCouponByID_Result>("GetCouponByID", CouponIdIdParameter, userIdParameter, LatParameter, LonParameter);
        }


        public ObjectResult<SearchCampaigns_Result> SearchCampaigns(int status, string keyword, int companyId, int fromRow, int toRow, bool adminMode)
        {
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));

            var keywordParameter = keyword != null ?
               new ObjectParameter("keyword", keyword) :
               new ObjectParameter("keyword", typeof(string));

            var companyIdParameter = status != null ?
               new ObjectParameter("companyId", companyId) :
               new ObjectParameter("companyId", typeof(int));


            var fromRowParameter = new ObjectParameter("fromRoww", fromRow);


            var toRowParameter = new ObjectParameter("toRow", toRow);


            var adminModeParameter = new ObjectParameter("adminMode", adminMode);


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchCampaigns_Result>("SearchCampaigns", statusParameter, keywordParameter, companyIdParameter, fromRowParameter, toRowParameter, adminModeParameter);
        }

        public ObjectResult<GetRegisteredUserData_Result> GetRegisteredUserData(int status, string keyword, int fromRow, int toRow)
        {
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));

            var keywordParameter = keyword != null ?
               new ObjectParameter("keyword", keyword) :
               new ObjectParameter("keyword", typeof(string));



            var fromRowParameter = new ObjectParameter("fromRoww", fromRow);


            var toRowParameter = new ObjectParameter("toRow", toRow);




            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRegisteredUserData_Result>("GetRegisteredUserData", statusParameter, keywordParameter, fromRowParameter, toRowParameter);
        }

        public void UpdateCompanyStatus(int status, string userId, string comments, int companyId)
        {
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));

            var userIdParameter = userId != null ?
               new ObjectParameter("userId", userId) :
               new ObjectParameter("userId", typeof(string));



            var commentsParameter = new ObjectParameter("comments", comments);


            var companyIdParameter = companyId != null ?
              new ObjectParameter("companyId", companyId) :
              new ObjectParameter("companyId", typeof(int));

            ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateCompanyStatus", statusParameter, userIdParameter, commentsParameter, companyIdParameter);
        }

        /// <summary>
        /// Returns the transactiopns agaisnt a companyid's account
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="AccountType"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public ObjectResult<GetTransactions_Result> GetTransactions(int CompanyId, int AccountType, int Rows)
        {
            var RowsParameter = Rows != null ?
                new ObjectParameter("Rows", Rows) :
                new ObjectParameter("Rows", typeof(int));

            var AccountTypeParameter = AccountType != null ?
               new ObjectParameter("AccountType", AccountType) :
               new ObjectParameter("AccountType", typeof(int));

            var CompanyIdParameter = CompanyId != null ?
               new ObjectParameter("CompanyId", CompanyId) :
               new ObjectParameter("CompanyId", typeof(int));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTransactions_Result>("GetTransactions", CompanyIdParameter, AccountTypeParameter, RowsParameter);
        }



        /// <summary>
        /// Returns the transactiopns agaisnt a companyid's account
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="AccountType"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public ObjectResult<Int32> GetUserProfileCompletness(string UserId)
        {
            var UserIdParam = new ObjectParameter("UserId", UserId);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>("GetUserProfileCompletness", UserIdParam);
        }

        public ObjectResult<Dashboard_analytics_Result> GetMainDashboardAnalytics(string UserId)
        {
            var UserIdParam = new ObjectParameter("UderID", UserId);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Dashboard_analytics_Result>("Dashboard_analytics", UserIdParam);

        }

        public ObjectResult<GetApprovalCount_Result> GetApprovalCount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetApprovalCount_Result>("GetApprovalCount");
        }
        public ObjectResult<GetRandom3Deal_Result> GetRandom3Deal()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRandom3Deal_Result>("GetRandom3Deal");
        }
        public ObjectResult<GetRandomPolls_Result> GetRandomPolls()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRandomPolls_Result>("GetRandomPolls");
        }
        public ObjectResult<GetRandomAdCampaign_Result> GetRandomAdCampaign(int Type)
        {
            var type = new ObjectParameter("type", Type);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRandomAdCampaign_Result>("GetRandomAdCampaign", type);
        }

        //public ObjectResult<getAdsCampaignByCampaignId_Result> getAdsCampaignByCampaignId(int compaignId, int CampStatus, int dateRange, int Granularity)
        //{
        //    var _compaignId = new ObjectParameter("CampaignId", compaignId);
        //    var _CampStatus = new ObjectParameter("status", CampStatus);
        //    var _dateRange = new ObjectParameter("DateRange", dateRange);
        //    var _Granularity = new ObjectParameter("Granularity", Granularity);
        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAdsCampaignByCampaignId_Result>("getAdsCampaignByCampaignId", _compaignId, _CampStatus, _dateRange, _Granularity);
        //}
        public ObjectResult<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {
            var _compaignId = new ObjectParameter("CampaignId", compaignId);
            var _CampStatus = new ObjectParameter("status", CampStatus);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            var _Granularity = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDisplayAdsCampaignByCampaignIdAnalytics_Result>("getDisplayAdsCampaignByCampaignIdAnalytics", _compaignId, _CampStatus, _dateRange, _Granularity);
        }

        public ObjectResult<getSurvayByPQID_Result> getSurvayByPQIDAnalytics(int PQId, int CampStatus, int dateRange, int Granularity)
        {
            var _PQId = new ObjectParameter("PQId", PQId);
            // var _CampStatus = new ObjectParameter("status", CampStatus);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            var _Granularity = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSurvayByPQID_Result>("getSurvayByPQID", _PQId, _dateRange, _Granularity);
        }
        public ObjectResult<getPollsBySQID_Result> getPollsBySQIDAnalytics(int SQId, int CampStatus, int dateRange, int Granularity)
        {
            var _SQId = new ObjectParameter("SQID", SQId);
            var _CampStatus = new ObjectParameter("status", CampStatus);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            var _Granularity = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPollsBySQID_Result>("getPollsBySQID", _SQId, _dateRange, _Granularity);
        }
        public ObjectResult<getDealByCouponID_Result> getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity)
        {
            var _CouponID = new ObjectParameter("CouponID", CouponID);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            var _Granularity = new ObjectParameter("Granularity", Granularity);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDealByCouponID_Result>("getDealByCouponID", _CouponID, _dateRange, _Granularity);
        }
        public ObjectResult<getAdsCampaignByCampaignIdRatioAnalytic_Result> getAdsCampaignByCampaignIdRatioAnalytic(int ID, int dateRange)
        {
            var _ID = new ObjectParameter("Id", ID);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAdsCampaignByCampaignIdRatioAnalytic_Result>("getAdsCampaignByCampaignIdRatioAnalytic", _ID, _dateRange);
        }
        public ObjectResult<getSurveyByPQIDRatioAnalytic_Result> getSurveyByPQIDRatioAnalytic(int ID, int dateRange)
        {
            var _ID = new ObjectParameter("Id", ID);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSurveyByPQIDRatioAnalytic_Result>("getSurveyByPQIDRatioAnalytic", _ID, _dateRange);
        }
        public ObjectResult<getPollBySQIDRatioAnalytic_Result> getPollBySQIDRatioAnalytic(int ID, int dateRange)
        {
            var _ID = new ObjectParameter("Id", ID);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPollBySQIDRatioAnalytic_Result>("getPollBySQIDRatioAnalytic", _ID, _dateRange);
        }
        public ObjectResult<getDealByCouponIdRatioAnalytic_Result> getDealByCouponIdRatioAnalytic(int ID, int dateRange)
        {
            var _ID = new ObjectParameter("Id", ID);
            var _dateRange = new ObjectParameter("DateRange", dateRange);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDealByCouponIdRatioAnalytic_Result>("getDealByCouponIdRatioAnalytic", _ID, _dateRange);
        }
        public ObjectResult<getAdsCampaignByCampaignIdtblAnalytic_Result> getAdsCampaignByCampaignIdtblAnalytic(int ID)
        {
            var _ID = new ObjectParameter("Id", ID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAdsCampaignByCampaignIdtblAnalytic_Result>("getAdsCampaignByCampaignIdtblAnalytic", _ID);
        }
        public ObjectResult<getSurvayByPQIDtblAnalytic_Result> getSurvayByPQIDtblAnalytic(int ID)
        {
            var _ID = new ObjectParameter("Id", ID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSurvayByPQIDtblAnalytic_Result>("getSurvayByPQIDtblAnalytic", _ID);
        }
        public ObjectResult<getPollBySQIDtblAnalytic_Result> getPollBySQIDtblAnalytic(int ID)
        {
            var _ID = new ObjectParameter("Id", ID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPollBySQIDtblAnalytic_Result>("getPollBySQIDtblAnalytic", _ID);
        }
        public ObjectResult<getCampaignROItblAnalytic_Result> getCampaignROItblAnalytic(int ID)
        {
            var _ID = new ObjectParameter("Id", ID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCampaignROItblAnalytic_Result>("getCampaignROItblAnalytic", _ID);
        }



        /// <summary>
        /// Get Shared survey question details
        /// </summary>
        public ObjectResult<GetSharedSurveyQuestion_Result> GetSharedSurveyQuestion(long SSQID)
        {
            var oSSQID = new ObjectParameter("SSQID", SSQID);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSharedSurveyQuestion_Result>("GetSharedSurveyQuestion", oSSQID);
        }


        /// <summary>
        /// Get Shared survey question details
        /// </summary>
        public ObjectResult<GetSharedSurveyQuestionsByUserId_Result> GetSharedSurveyQuestionsByUserId(string UserId)
        {
            var oUserId = new ObjectParameter("UserId", UserId);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSharedSurveyQuestionsByUserId_Result>("GetSharedSurveyQuestionsByUserId", oUserId);
        }

        public ObjectResult<Int32> getCampaignByIdQQFormAnalytic(long CampaignId, int Choice, int Gender, int age, String Profession, String City, int type, int QuestionId)
        {


            var Id = new ObjectParameter("Id", CampaignId);
            var chioce = new ObjectParameter("coice", Choice);
            var gender = new ObjectParameter("gender", Gender);
            var ageRange = new ObjectParameter("ageRange", age);
            var profession = new ObjectParameter("Profession", Profession);
            var city = new ObjectParameter("city", City);
            var _type = new ObjectParameter("type", type);
            var _QuestionId = new ObjectParameter("QId", QuestionId);

            ObjectResult<Int32> res = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>("getCampaignByIdQQFormAnalytic", Id, chioce, gender, ageRange, profession, city, _type, _QuestionId);
            return res;
        }
        public ObjectResult<getCampaignByIdFormDataAnalytic_Result> getCampaignByIdFormDataAnalytic(long CampaignId)
        {
            var Id = new ObjectParameter("Id", CampaignId);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCampaignByIdFormDataAnalytic_Result>("getCampaignByIdFormDataAnalytic", Id);
        }

        #endregion
    }
}
