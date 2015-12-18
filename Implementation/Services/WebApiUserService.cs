using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// WebApi User Service 
    /// </summary>
    public sealed class WebApiUserService : IWebApiUserService
    {

        #region Private

        private readonly IEmailManagerService emailManagerService;

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        /// <summary>
        /// Throws Exception if registreation fails
        /// </summary>
        private static void ThrowRegisterUserErrors(IdentityResult result)
        {
            string errors = result.Errors.Aggregate("", (current, error) => current + (Environment.NewLine + error));
            throw new SMDException(errors);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebApiUserService(IEmailManagerService emailManagerService)
        {
            if (emailManagerService == null)
            {
                throw new ArgumentNullException("emailManagerService");
            }

            this.emailManagerService = emailManagerService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Archive Account
        /// </summary>
        public async Task Archive(string userId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Archive Account
            user.Status = (int)UserStatus.InActive;

            // Save Changes
            await UserManager.UpdateAsync(user);
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        public async Task UpdateProfile(UpdateUserProfileRequest request)
        {
            User user = await UserManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Update User
            user.Update(request);

            // Save Changes
            await UserManager.UpdateAsync(user);
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        public async Task<bool> ConfirmEmail(string userId, string code)
        {
            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (!result.Succeeded)
            {
                ThrowRegisterUserErrors(result);
            }

            await emailManagerService.SendRegisrationSuccessEmail(userId);

            return true;
        }

        /// <summary>
        /// Perform Custom Registration
        /// </summary>
        public async Task<User> RegisterCustom(RegisterCustomRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName };
            var result = await UserManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                ThrowRegisterUserErrors(result);
            }

            var addUserToRoleResult = await UserManager.AddToRoleAsync(user.Id, Roles.User); // Only Type 'User' Role will be registered from app
            if (!addUserToRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Format("Failed to add user to role {0}", Roles.User));
            }

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                              "/Api_Mobile/Register/Confirm/?UserId=" + user.Id + "&Code=" + HttpUtility.UrlEncode(code);
            await
                emailManagerService.SendAccountVerificationEmail(user, callbackUrl);
            
            return user;
        }

        /// <summary>
        /// Perform External Registration
        /// </summary>
        public async Task<User> RegisterExternal(RegisterExternalRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName };
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                ThrowRegisterUserErrors(result);
            }

            var login = new UserLoginInfo(request.LoginProvider, request.LoginProviderKey);
            result = await UserManager.AddLoginAsync(user.Id, login);
            if (!result.Succeeded)
            {
                ThrowRegisterUserErrors(result);
            }

            return user;
        }
        
        /// <summary>
        /// Perform External Login
        /// </summary>
        public async Task<User> ExternalLogin(ExternalLoginRequest request)
        {
            User user = await UserManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidEmail);
            }

            if (user.Status == (int)UserStatus.InActive)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InactiveUser);
            }

            if (user.UserLogins == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_LoginInfoNotFound);
            }

            UserLogin userLoginInfo = user.UserLogins.FirstOrDefault(
                u => u.LoginProvider == request.LoginProvider && u.ProviderKey == request.LoginProviderKey);
            
            if (userLoginInfo == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_ProviderKeyInvalid);        
            }

            return user;
        }

        /// <summary>
        /// Standard Login
        /// </summary>
        public async Task<User> StandardLogin(StandardLoginRequest request)
        {
            User user = await UserManager.FindAsync(request.UserName, request.Password);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidCredentials);
            }

            if (!user.EmailConfirmed)
            {
                throw new SMDException(LanguageResources.WebApiUserService_EmailNotVerified);    
            }

            if (user.Status == (int)UserStatus.InActive)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InactiveUser);
            }

            return user;
        }

        /// <summary>
        /// Standard Login
        /// </summary>
        public User AuthenticateUser(StandardLoginRequest request)
        {
            User user = UserManager.FindAsync(request.UserName, request.Password).Result;
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidCredentials);
            }

            return user;
        }

        /// <summary>
        /// Save Stripe Customer
        /// </summary>
        public async Task SaveStripeCustomerId(string customerId)
        {
            User user = await UserManager.FindByIdAsync(UserManager.LoggedInUserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_LoginInfoNotFound);
            }

            user.StripeCustomerId = customerId;
            await UserManager.UpdateAsync(user);
        }

        /// <summary>
        /// Get Stripe Customer
        /// </summary>
        public async Task<string> GetStripeCustomerId()
        {
            User user = await UserManager.FindByIdAsync(UserManager.LoggedInUserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_LoginInfoNotFound);
            }

            return user.StripeCustomerId;
        }

        #endregion
    }
}
