using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;

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

        public ObjectResult<GetAds_Result> GetAds(int? age, int? gender, int? countryId, int? cityId, int? languageId, int? industryId)
        // ReSharper restore InconsistentNaming
        {
            var ageParameter = age.HasValue ?
                new ObjectParameter("age", industryId) :
                new ObjectParameter("age", typeof(int));

            var genderParameter = gender.HasValue ?
                new ObjectParameter("gender", gender) :
                new ObjectParameter("gender", typeof(int));

            var countryIdParameter = countryId.HasValue ?
                new ObjectParameter("countryId", countryId) :
                new ObjectParameter("countryId", typeof(int));

            var cityIdParameter = cityId.HasValue ?
               new ObjectParameter("countryId", cityId) :
               new ObjectParameter("countryId", typeof(int));

            var languageIdParameter = languageId.HasValue ?
               new ObjectParameter("languageId", languageId) :
               new ObjectParameter("languageId", typeof(int));

            var industryIdParameter = industryId.HasValue ?
               new ObjectParameter("industryId", industryId) :
               new ObjectParameter("industryId", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAds_Result>("GetAds", ageParameter, genderParameter, countryIdParameter, cityIdParameter, languageIdParameter, industryIdParameter);
        }

        #endregion
    }
}
