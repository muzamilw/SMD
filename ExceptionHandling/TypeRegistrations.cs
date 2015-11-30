using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Interfaces.Logger;

namespace SMD.ExceptionHandling
{
    /// <summary>
    /// Type Registration for Exception Handling
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types Exception Handling module
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {            
            unityContainer.RegisterType<ISMDLogger, SMDLogger>();
        }
    }
}
