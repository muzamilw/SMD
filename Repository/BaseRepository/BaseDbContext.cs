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
        /// Get Ad-Campaigns for APIs | baqer
        /// </summary>
        public System.Data.Entity.Core.Objects.ObjectResult<GetAds_Result> GetAdCompaignForApi(string userId, int fromRow, int toRow)
        {
            var uId = new System.Data.Entity.Core.Objects.ObjectParameter("UserID", userId);
            var fRow = new System.Data.Entity.Core.Objects.ObjectParameter("FromRow", fromRow);
            var tRow = new System.Data.Entity.Core.Objects.ObjectParameter("ToRow", toRow);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAds_Result>("GetAds", uId, fRow, tRow);
        }


        /// <summary>
        /// Get Surveys for APIs | baqer
        /// </summary>
        public System.Data.Entity.Core.Objects.ObjectResult<GetSurveys_Result> GetSurveysForApi(string userId, int fromRow, int toRow)
        {
            var uId = new System.Data.Entity.Core.Objects.ObjectParameter("UserID", userId);
            var fRow = new System.Data.Entity.Core.Objects.ObjectParameter("FromRow", fromRow);
            var tRow = new System.Data.Entity.Core.Objects.ObjectParameter("ToRow", toRow);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSurveys_Result>("GetSurveys", uId, fRow, tRow);
        }


        /// <summary>
        /// Get Surveys matching count for APIs | baqer
        /// </summary>
        public System.Data.Entity.Core.Objects.ObjectResult<Int32> GetAudienceSurveyCount(GetAudienceSurveyRequest request)
        {
            var countryId = new System.Data.Entity.Core.Objects.ObjectParameter("countryId", request.CountryId);
            var cityId = new System.Data.Entity.Core.Objects.ObjectParameter("cityId", request.CityId);
            var languageId = new System.Data.Entity.Core.Objects.ObjectParameter("languageId", request.LanguageId);
            var industryId = new System.Data.Entity.Core.Objects.ObjectParameter("industryId", request.IndustryId);
            var gender = new System.Data.Entity.Core.Objects.ObjectParameter("gender", request.Gender);
            var age = new System.Data.Entity.Core.Objects.ObjectParameter("age", request.Age);
            var pqIds = new System.Data.Entity.Core.Objects.ObjectParameter("profileQuestionIds", request.IdsList);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>
                ("GetAudienceSurvey", age, gender, countryId, cityId, languageId, industryId, pqIds);
        }


        /// <summary>
        /// Get AdCampaign matching count for APIs | baqer
        /// </summary>
        public System.Data.Entity.Core.Objects.ObjectResult<Int32> GetAudienceAdCampaignCount(GetAudienceSurveyRequest request)
        {
            var countryId = new System.Data.Entity.Core.Objects.ObjectParameter("countryId", request.CountryId);
            var cityId = new System.Data.Entity.Core.Objects.ObjectParameter("cityId", request.CityId);
            var languageId = new System.Data.Entity.Core.Objects.ObjectParameter("languageId", request.LanguageId);
            var industryId = new System.Data.Entity.Core.Objects.ObjectParameter("industryId", request.IndustryId);
            var gender = new System.Data.Entity.Core.Objects.ObjectParameter("gender", request.Gender);
            var age = new System.Data.Entity.Core.Objects.ObjectParameter("age", request.Age);
            var pqIds = new System.Data.Entity.Core.Objects.ObjectParameter("profileQuestionIds", request.IdsList);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Int32>
               ("GetAudienceAdCampaign", age, gender, countryId, cityId, languageId, industryId, pqIds);
        }
        #endregion
    }
}
