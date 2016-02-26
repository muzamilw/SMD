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
        /// <summary>
        /// Educations
        /// </summary>
        public DbSet<Education> Educations { get; set; }
        /// <summary>
        /// Ad Campaign Response
        /// </summary>
        public DbSet<AdCampaignResponse> AdCampaignResponses { get; set; }

        /// <summary>
        /// AdCampaign Target Criterias
        /// </summary>
        public DbSet<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }

        /// <summary>
        /// AdCampaign Target Locations
        /// </summary>
        public DbSet<AdCampaignTargetLocation> AdCampaignTargetLocations { get; set; }

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
        /// Ad Campaigns
        /// </summary>
        public DbSet<AdCampaign> AdCampaigns { get; set; }

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

        /// <summary>
        /// Transaction Log
        /// </summary>
        public DbSet<TransactionLog> TransactionLogs { get; set; }

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
            , string surveyQuestionIdsExcluded)
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
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAudience_Result>("GetAudience",
                ageFromParameter, ageToParameter, genderParameter, countryIdsParameter, cityIdsParameter, languageIdsParameter,
                industryIdsParameter, profileQuestionIdsParameter, profileAnswerIdsParameter, surveyQuestionIdsParameter
                , surveyAnswerIdsParameter, countryIdsExcludedParameter, cityIdsExcludedParameter, languageIdsExcludedParameter
                , industryIdsExcludedParameter
                , educationIdsParameter
                , educationIdsExcludedParameter
                , profileQuestionIdsExcludedParameter
                , surveyQuestionIdsExcludedParameter);
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

        #endregion
    }
}
