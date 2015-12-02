using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using SMD.Repository.Repositories;

namespace SMD.Implementation.Identity
{
    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public static class UnityConfig
    {
        private static IUnityContainer unityContainer;
        public static IUnityContainer UnityContainer { get { return unityContainer; } set { unityContainer = value; } }
    }

    /// <summary>
    /// Role Manager
    /// </summary>
    public class ApplicationRoleManager : RoleManager<Role, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationRoleManager(IRoleStore<Role, string> roleStore)
            : base(roleStore)
        {
        }

        /// <summary>
        /// Create Role
        /// </summary>
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;

            BaseDbContext db = (BaseDbContext)UnityConfig.UnityContainer.Resolve(typeof(BaseDbContext),
                new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

            return new ApplicationRoleManager(new RoleStore(db));
        }
    }

    /// <summary>
    /// Email Service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Sms Service
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
