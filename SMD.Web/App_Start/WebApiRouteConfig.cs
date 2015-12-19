using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS
{
    /// <summary>
    /// WebApi configurations 
    /// </summary>
    public class WebApiRouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config, HttpMessageHandler routeHandlers)
        {
            // Standard Login route
            config.Routes.MapHttpRoute(
                "StandardLogin",
                "Login/Standard/",
                new { controller = "StandardLogin" },
                null,
                routeHandlers);

            // External Login route
            config.Routes.MapHttpRoute(
                "ExternalLogin",
                "Login/External/",
                new { controller = "ExternalLogin" },
                null,
                routeHandlers);

            // Register External route
            config.Routes.MapHttpRoute(
                "RegisterExternal",
                "Register/External/",
                new { controller = "RegisterExternal" },
                null,
                routeHandlers);

            // Register Custom route
            config.Routes.MapHttpRoute(
                "RegisterCustom",
                "Register/Custom/",
                new { controller = "RegisterCustom" },
                null,
                routeHandlers);

            // Confirm Email - Register Custom route
            config.Routes.MapHttpRoute(
                "ConfirmEmail",
                "Register/Confirm/",
                new { controller = "ConfirmEmail" },
                null,
                routeHandlers);

            // Update User Profile Custom route
            config.Routes.MapHttpRoute(
                "UpdateUserProfile",
                "Update/User/",
                new { controller = "UpdateUserProfile" },
                null,
                routeHandlers);

            // Archive User route
            config.Routes.MapHttpRoute(
                "ArchiveUserAccount",
                "User/Archive/",
                new { controller = "ArchiveUserAccount" },
                null,
                routeHandlers);

            // Ad Viewed route
            config.Routes.MapHttpRoute(
                "AdViewed",
                "Ad/Viewed/",
                new { controller = "AdViewed" },
                null,
                routeHandlers);
        }
    }
}