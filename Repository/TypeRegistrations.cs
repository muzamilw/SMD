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
            unityContainer.RegisterType<IAdCampaignResponseRepository, IAdCampaignResponseRepository>();
            unityContainer.RegisterType<IAdCampaignTargetCriteriaRepository, IAdCampaignTargetCriteriaRepository>();
            unityContainer.RegisterType<IAdCampaignTargetLocationRepository, AdCampaignTargetLocationRepository>();
            unityContainer.RegisterType<ICountryRepository, CountryRepository>();
            unityContainer.RegisterType<ILanguageRepository, LanguageRepository>();
        }
    }
}