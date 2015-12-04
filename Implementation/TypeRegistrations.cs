using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Implementation.Services;
using SMD.Interfaces;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Services;

namespace SMD.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<ISMDLogger, SMDLogger>();
            unityContainer.RegisterType<IAuthorizationChecker, AuthorizationChecker>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();
            unityContainer.RegisterType<IProfileQuestionService, ProfileQuestionService>();
            unityContainer.RegisterType<IEmailManagerService, EmailManagerService>();
            unityContainer.RegisterType<IAdvertService, AdvertService>();
            unityContainer.RegisterType<IProfileQuestionAnswerService, ProfileQuestionAnswerService>();
        }
    }
}