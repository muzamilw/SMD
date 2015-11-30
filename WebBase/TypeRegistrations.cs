using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SMD.WebBase.Mvc;
using SMD.WebBase.UnityConfiguration;

namespace SMD.WebBase
{
    public static class TypeRegistrations
    {
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType(typeof(IControllerActivator), typeof(UnityControllerActivator));
            unityContainer.RegisterType<IExceptionFilter, LogExceptionFilterAttribute>();
            unityContainer.RegisterType<System.Web.Http.Filters.IExceptionFilter, LogExceptionFilterAttribute>();
        }
    }
}
