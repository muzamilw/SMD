using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using SMD.WebBase.Mvc;
using Thinktecture.IdentityModel.Tokens.Http;

namespace SMD.MIS
{
    /// <summary>
    /// Security Config
    /// </summary>
    public class SecurityConfig

    {
        /// <summary>
        /// Unity Container
        /// </summary>
        private static IUnityContainer UnityContainer { get; set; }

        #region Private

        /// <summary>
        /// Check Authentication
        /// </summary>
        private static bool CheckAuthentication(string userName, string password)
        {
            var authenticationChecker = UnityContainer.Resolve<IWebApiUserService>();
            var result = authenticationChecker.AuthenticateUser(new StandardLoginRequest { UserName = userName, Password = password });
            if (result != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(userName);
                HttpContext.Current.User = new ClaimsPrincipal(identity);
                return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Configure security
        /// </summary>
        public static void ConfigureGlobal(HttpConfiguration globalConfig, IUnityContainer container)
        {
            UnityContainer = container;
            globalConfig.MessageHandlers.Add(new AuthenticationHandler(CreateConfiguration(container)));
            globalConfig.Filters.Add(new SecurityExceptionFilter());
        }
        
        /// <summary>
        /// Create the configuration
        /// </summary>
        public static AuthenticationConfiguration CreateConfiguration(IUnityContainer container)
        {
            UnityContainer = container;
            AuthenticationConfiguration config = new AuthenticationConfiguration{ RequireSsl = false };
            config.AddBasicAuthentication(CheckAuthentication);
            return config;
        }

    }
}