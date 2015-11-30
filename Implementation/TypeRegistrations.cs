using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Interfaces.Logger;
using SMD.Interfaces;

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
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<ISMDLogger, SMDLogger>();
            unityContainer.RegisterType<IAuthorizationChecker, AuthorizationChecker>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();

        }
    }
}