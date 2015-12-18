using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Net.Http.Formatting;
using SMD.Models.Common;
using Owin;
using Microsoft.Owin;
using SMD.Implementation;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using SMD.Implementation.Identity;
using IdentitySample.Models;
using System.Web.Http;
using SMD.WebBase.Mvc;
using SMD.WebBase.UnityConfiguration;
using SMD.WebBase.WebApi;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens.Http;
using System.Net.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace SMD.MIS
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(30));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Use Google Authentication
            app.UseGoogleAuthentication(
                clientId: "569299227032-23iqpe7ggoqdearjf83n3cid8ahhnd6r.apps.googleusercontent.com",
                clientSecret: "iCQoRPK-m9W9B8NyGO9XQbCN");

            // Use Facebook Authentication
            app.UseFacebookAuthentication(
                appId: "900194280062971",
                appSecret: "7d8a7f398bb09ca362a051f1d8e2d71e");

            app.Map("/Api_Mobile", inner =>
            {
                // Web API configuration and services
                var config = new HttpConfiguration();
                // Suppress default authentication
                config.SuppressDefaultHostAuthentication();
                // List of delegating handlers.
                var handlers = new DelegatingHandler[] {
                      new AuthenticationHandler(SecurityConfig.CreateConfiguration(UnityWebActivator.Container))
                };
                // Create a message handler chain with an end-point.
                var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);
                
                // configure route
                WebApiRouteConfig.RegisterRoutes(config, routeHandlers);
                
                // Configure Dependency Resolver
                config.DependencyResolver = new UnityDependencyResolver(UnityWebActivator.Container);
                
                // Configure Formatter
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());

                // Use Configuration for Webapi
                inner.UseWebApi(config);
            });
        }
    }
}