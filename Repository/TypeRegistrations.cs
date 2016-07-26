using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Repository.BaseRepository;
using SMD.Repository.Repositories;

namespace SMD.Repository
{
    /// <summary>
    /// Repository Type Registration
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Repositories
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IProfileQuestionRepository, ProfileQuestionRepository>();
            unityContainer.RegisterType<IAdCampaignRepository, AdCampaignRepository>();
            unityContainer.RegisterType<IProfileQuestionGroupRepository, ProfileQuestionGroupRepository>();
            unityContainer.RegisterType<IAdCampaignTargetCriteriaRepository, AdCampaignTargetCriteriaRepository>();
            unityContainer.RegisterType<IAdCampaignTargetLocationRepository, AdCampaignTargetLocationRepository>();
            unityContainer.RegisterType<ICountryRepository, CountryRepository>();
            unityContainer.RegisterType<ILanguageRepository, LanguageRepository>();
            unityContainer.RegisterType<IProfileQuestionAnswerRepository, ProfileQuestionAnswerRepository>();
            unityContainer.RegisterType<ISystemMailsRepository, SystemMailsRepository>();
            unityContainer.RegisterType<ISurveyQuestionRepository, SurveyQuestionRepository>();
            unityContainer.RegisterType<IAuditLogRepository, AuditLogRepository>();
            unityContainer.RegisterType<ICityRepository, CityRepository>();
            unityContainer.RegisterType<IProfileQuestionUserAnswerRepository, ProfileQuestionUserAnswerRepository>();
            unityContainer.RegisterType<IAccountRepository, AccountRepository>();
            unityContainer.RegisterType<ITransactionRepository, TransactionRepository>();
            unityContainer.RegisterType<ISurveyQuestionTargetCriteriaRepository, SurveyQuestionTargetCriteriaRepository>();
            unityContainer.RegisterType<ISurveyQuestionTargetLocationRepository, SurveyQuestionTargetLocationRepository>();
            unityContainer.RegisterType<IInvoiceRepository, InvoiceRepository>();
            unityContainer.RegisterType<IIndustryRepository, IndustryRepository>();
            unityContainer.RegisterType<IProductRepository, ProductRepository>();
            unityContainer.RegisterType<ITaxRepository, TaxRepository>();
            unityContainer.RegisterType<IInvoiceDetailRepository, InvoiceDetailRepository>();
            unityContainer.RegisterType<IEducationRepository, EducationRepository>();
            unityContainer.RegisterType<ITransactionLogRepository, TransactionLogRepository>();
            unityContainer.RegisterType<ISurveyQuestionResponseRepository, SurveyQuestionResponseRepository>();
            unityContainer.RegisterType<IAdCampaignResponseRepository, AdCampaignResponseRepository>();
            unityContainer.RegisterType<ICompanyRepository, CompanyRepository>();
            unityContainer.RegisterType<IManageUserRepository, ManageUserRepository>();
            unityContainer.RegisterType<ICouponCategoryRepository, CouponCategoryRepository>();
            unityContainer.RegisterType<ICampaignCategoriesRepository, CampaignCategoriesRepository>();
            unityContainer.RegisterType<ICouponCodeRepository, CouponCodeRepository>();
            unityContainer.RegisterType<IUserFavouriteCouponRepository, UserFavouriteCouponRepository>();
            unityContainer.RegisterType<ICompanyAspNetUsersRepository, CompanyAspNetUsersRepository>();
        }
    }
}