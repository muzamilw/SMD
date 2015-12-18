using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using SMD.Models.Common;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using SMD.Repository.Repositories;

namespace SMD.Implementation.Identity
{
    /// <summary>
    /// Application User Manager
    /// </summary>
    public class ApplicationUserManager : UserManager<User, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationUserManager(IUserStore<User, string> store)
            : base(store)
        {
        }

        public string LoggedInUserId { get { return HttpContext.Current.User.Identity.GetUserId(); } }

        public string LoggedInUserRole {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated ||
                    string.IsNullOrEmpty(HttpContext.Current.User.Identity.GetUserId()))
                {
                    return string.Empty;
                }

                if (HttpContext.Current.User.IsInRole(Roles.User))
                {
                    return Roles.User;
                }
                if (HttpContext.Current.User.IsInRole(Roles.Adminstrator))
                {
                    return Roles.Adminstrator;
                }
                if (HttpContext.Current.User.IsInRole(Roles.Approver))
                {
                    return Roles.Approver;
                }
                if (HttpContext.Current.User.IsInRole(Roles.Editor))
                {
                    return Roles.Editor;
                }

                return Roles.User;
            } 
        }

        /// <summary>
        /// Send Email
        /// </summary>
        public override Task SendEmailAsync(string email, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromUser = ConfigurationManager.AppSettings["FromUser"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];

            //Getting the file from config, to send
            MailMessage oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            SmtpClient client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromUser, fromPwd)
            };

            return client.SendMailAsync(oEmail);

        }

        /// <summary>
        /// Create User
        /// </summary>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;


            BaseDbContext db = (BaseDbContext)UnityConfig.UnityContainer.Resolve(typeof(BaseDbContext),
                new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

            var manager = new ApplicationUserManager(new UserStore(db));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, string>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, string>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, string>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, string>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
