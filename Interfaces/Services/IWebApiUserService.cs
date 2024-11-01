﻿using System.Threading.Tasks;
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
        /// Gets Ads, Surveys, Questions as paged view
        /// </summary>
        GetProductsResponse GetProducts(GetProductsRequest request);
        
        /// <summary>
        /// Approve Survey
        /// </summary>
        Task<BaseApiResponse> UpdateTransactionOnSurveyApproval(ApproveSurveyRequest request, bool isApiCall = true);

        /// <summary>
        /// Ad Viewed
        /// </summary>
        Task<BaseApiResponse> UpdateTransactionOnViewingAd(AdViewedRequest request);

        /// <summary>
        /// Archive Account
        /// </summary>
        Task<BaseApiResponse> Archive(string userId);
        
        /// <summary>
        /// Update Profile
        /// </summary>
        Task<BaseApiResponse> UpdateProfile(UpdateUserProfileRequest request);
        
        /// <summary>
        /// Confirm Email
        /// </summary>
        Task<BaseApiResponse> ConfirmEmail(string userId, string code);
        
        /// <summary>
        /// Register Custom
        /// </summary>
        Task<LoginResponse> RegisterCustom(RegisterCustomRequest request);
        
        /// <summary>
        /// Register External 
        /// </summary>
        Task<LoginResponse> RegisterExternal(RegisterExternalRequest request);
        
        /// <summary>
        /// External Login 
        /// </summary>
        Task<LoginResponse> ExternalLogin(ExternalLoginRequest request);

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

        /// <summary>
        /// Get Stripe Customer by Email
        /// </summary>
        Task<string> GetStripeCustomerIdByEmail(string email);

        /// <summary>
        /// Get User using usermanager  For Stripe Work 
        /// </summary>
        User GetUserByUserId(string userId);
    }
}
