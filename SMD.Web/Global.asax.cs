using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using SMD.MIS.App_Start;
using SMD.Models.LoggerModels;
using SMD.WebBase.UnityConfiguration;
using UnityDependencyResolver = SMD.WebBase.UnityConfiguration.UnityDependencyResolver;
using System.Web;
using SMD.WebBase.WebApi;


namespace SMD.MIS
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private
        private static IUnityContainer container;
        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());
        }
        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();

            return container;
        }
        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            SMD.WebBase.TypeRegistrations.RegisterTypes(container);
            SMD.Implementation.TypeRegistrations.RegisterType(container);
            SMD.ExceptionHandling.TypeRegistrations.RegisterType(container);

        }
        /// <summary>
        /// Register unity 
        /// </summary>
        private static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }

        /// <summary>
        /// Change MVC Configuration
        /// </summary>
        private static void ChangeMvcConfiguration()
        {
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new CustomRazorViewEngine());
        }

        #endregion
        protected void Application_Start()
        {
            RegisterIoC();
            ConfigureLogger();
            ChangeMvcConfiguration();
            AreaRegistration.RegisterAllAreas();

            BundleTable.EnableOptimizations = !HttpContext.Current.IsDebuggingEnabled;
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        #region Set Session State

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/Api");
        }

        #endregion
    }
}