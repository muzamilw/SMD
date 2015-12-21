using System.Threading.Tasks;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// WebApi User Service Interface 
    /// </summary>
    public interface IWebApiUserService
    {
        /// <summary>
        /// Ad Viewed
        /// </summary>
        Task UpdateTransactionOnViewingAd(AdViewedRequest request);

        /// <summary>
        /// Archive Account
        /// </summary>
        Task Archive(string userId);
        
        /// <summary>
        /// Update Profile
        /// </summary>
        Task UpdateProfile(UpdateUserProfileRequest request);
        
        /// <summary>
        /// Confirm Email
        /// </summary>
        Task<bool> ConfirmEmail(string userId, string code);
        
        /// <summary>
        /// Register Custom
        /// </summary>
        Task<User> RegisterCustom(RegisterCustomRequest request);
        
        /// <summary>
        /// Register External 
        /// </summary>
        Task<User> RegisterExternal(RegisterExternalRequest request);
        
        /// <summary>
        /// External Login 
        /// </summary>
        Task<User> ExternalLogin(ExternalLoginRequest request);

        /// <summary>
        /// Standard Login 
        /// </summary>
        Task<LoginResponse> StandardLogin(StandardLoginRequest request);

        /// <summary>
        /// Standard Login 
        /// </summary>
        User AuthenticateUser(StandardLoginRequest request);

        /// <summary>
        /// Save Stripe Customer 
        /// </summary>
        Task SaveStripeCustomerId(string customerId);

        /// <summary>
        /// Get Stripe Customer
        /// </summary>
        /// <returns></returns>
        Task<string> GetStripeCustomerId();
    }
}
