using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// WebApi User Service 
    /// </summary>
    public sealed class WebApiUserService : IWebApiUserService
    {

        #region Private

        private readonly IEmailManagerService emailManagerService;
        private readonly IAdCampaignRepository adCampaignRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly ISurveyQuestionRepository surveyQuestionRepository;

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        /// <summary>
        /// Throws Exception if registreation fails
        /// </summary>
        private static LoginResponse ThrowRegisterUserErrors(IdentityResult result)
        {
            string errors = result.Errors.Aggregate("", (current, error) => current + (Environment.NewLine + error));
            return new LoginResponse
                   {
                       Message = errors
                   };
        }

        #region Ad Campaign Transactions

        /// <summary>
        /// Performs Transaction
        /// </summary>
        /// <param name="adCampaingId">If Ad Click / Viewed</param>
        /// <param name="surveyQuestionId">If Approve Survery / Download Survey Report</param>
        /// <param name="account">User's Account</param>
        /// <param name="transactionAmount">Amount to credit / debit</param>
        /// <param name="transactionSequence">Sequence this transaction is going through</param>
        /// <param name="transactionType">Type of transaction AdClick or Approve Survey</param>
        /// <param name="isCredit">Credit if true</param>
        private void PerformTransaction(long? adCampaingId, long? surveyQuestionId, Account account, double? transactionAmount,
            int transactionSequence, TransactionType transactionType, bool isCredit = true)
        {
            var transaction = new Transaction
                                             {
                                                 AccountId = account.AccountId,
                                                 Sequence = transactionSequence,
                                                 Type = (int)transactionType, 
                                                 isProcessed = true,
                                                 TransactionDate = DateTime.Now
                                             };

            // If Ad Clicked / Viewed
            if (adCampaingId.HasValue)
            {
                transaction.AdCampaignId = adCampaingId;
            }

            // If Approved Survey Question
            else if (surveyQuestionId.HasValue)
            {
                transaction.SQId = surveyQuestionId;
            }

            // Update Advertisers Account Balance
            if (!account.AccountBalance.HasValue)
            {
                account.AccountBalance = 0;
            }
            
            // If Credit Add to balance else deduct
            if (isCredit)
            {
                transaction.CreditAmount = transactionAmount;
                account.AccountBalance += Convert.ToDecimal(transactionAmount);
            }
            else
            {
                transaction.DebitAmount = transactionAmount;
                account.AccountBalance -= Convert.ToDecimal(transactionAmount);    
            } 
            
            // Add Transcation to repository
            transactionRepository.Add(transaction);
        }

        /// <summary>
        /// Perform AdCampaign Transaction
        /// </summary>
        /// <param name="request">Contains Ad Campaing Id, User Id</param>
        /// <param name="advertisersAccount">Advertisers Account Info</param>
        /// <param name="adClickRate">AdClick Rate</param>
        /// <param name="adViewersAccount">AdViewers Account Info</param>
        /// <param name="adViewersCut">AdViewers Amount to credit</param>
        /// <param name="referringUser">Affiliate</param>
        /// <param name="smdsCut">SMDs Amount to credit</param>
        /// <param name="affiliatesAccount">Affiliate Account Info</param>
        /// <param name="smdAccount">SMD Account Info</param>
        /// <param name="adCampaign">Ad Campaign</param>
        private void PerformAdCampaignTransactions(AdViewedRequest request, Account advertisersAccount, double? adClickRate,
            Account adViewersAccount, double? adViewersCut, User referringUser, double? smdsCut, Account affiliatesAccount,
            Account smdAccount, AdCampaign adCampaign)
        {
            // Debit Advertiser
            var transactionSequence = 1;
            
            // Perform the transactions
            // Debit Campaign Advertiser
            PerformTransaction(request.AdCampaignId, null, advertisersAccount, adClickRate, transactionSequence, TransactionType.AdClick, false);

            // Credit AdViewer
            transactionSequence += 1;
            PerformTransaction(request.AdCampaignId, null, adViewersAccount, adViewersCut, transactionSequence, TransactionType.AdClick);

            // Credit Affiliate
            smdsCut = PerformCreditTransactionForAffiliate(request, referringUser, smdsCut, affiliatesAccount, ref transactionSequence);

            // Credit SMD
            transactionSequence += 1;
            PerformTransaction(request.AdCampaignId, null, smdAccount, smdsCut, transactionSequence, TransactionType.AdClick);

            // Update AdCampaign Amount Spent
            adCampaign.AmountSpent += adClickRate;

            // Save Changes
            transactionRepository.SaveChanges();
        }

        /// <summary>
        /// Credit Campaing Affiliate
        /// </summary>
        private double? PerformCreditTransactionForAffiliate(AdViewedRequest request, User referringUser, double? smdsCut,
            Account affiliatesAccount, ref int transactionSequence)
        {
            if (referringUser != null)
            {
                // Credit Affiliate - if exists
                double? affiliatesCut = ((smdsCut * 20) / 100);
                smdsCut = ((smdsCut * 30) / 100);
                transactionSequence += 1;
                // Perform Transaction
                PerformTransaction(request.AdCampaignId, null, affiliatesAccount, affiliatesCut, transactionSequence, TransactionType.AdClick);
            }

            return smdsCut;
        }

        /// <summary>
        /// Sets up accounts for transcations
        /// Verifies if required accounts exist
        /// </summary>
        private void SetupAdCampaignTransaction(AdViewedRequest request, AdCampaign adCampaign,
            out Account adViewersAccount, out Account advertisersAccount, out Account smdAccount)
        {
            
            // Get business Accounts for Each Individual involved in this transaction
            adViewersAccount = accountRepository.GetByUserId(request.UserId);
            if (adViewersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Current User"));
            }

            advertisersAccount = accountRepository.GetByUserId(adCampaign.UserId);
            if (advertisersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Advertiser"));
            }

            smdAccount = accountRepository.GetByName(Accounts.Smd);
            if (smdAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "SMD"));
            }
        }

        /// <summary>
        /// Validates Ad Campaing
        /// AdCampaign Exists
        /// </summary>
        private async Task<AdCampaign> ValidateAdCampaign(AdViewedRequest request)
        {
            // Get AdCampaign against AdCampaign Id
            AdCampaign adCampaign = adCampaignRepository.Find(request.AdCampaignId);
            if (adCampaign == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AdCampaignNotFound,
                    request.AdCampaignId));
            }

            if (string.IsNullOrEmpty(adCampaign.UserId))
            {
                throw new SMDException(LanguageResources.WebApiUserService_AdvertiserNotFound);
            }

            // Get Advertiser
            User advertiser = await UserManager.FindByIdAsync(adCampaign.UserId);
            if (advertiser == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_AdvertiserNotFound);
            }

            return adCampaign;
        }

        #endregion

        #region Survery Approval Transactions


        /// <summary>
        /// Validates Survey Question
        /// Survey Question Exists
        /// </summary>
        private async Task<SurveyQuestion> ValidateSurveyQuestion(ApproveSurveyRequest request)
        {
            // Get Survey Question against Survery Question Id
            SurveyQuestion surveyQuestion = surveyQuestionRepository.Find(request.SurveyQuestionId);
            if (surveyQuestion == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_SurveyQuestionNotFound,
                    request.SurveyQuestionId));
            }

            if (string.IsNullOrEmpty(surveyQuestion.UserId))
            {
                throw new SMDException(LanguageResources.WebApiUserService_AdvertiserNotFound);
            }

            // Get Advertiser
            User advertiser = await UserManager.FindByIdAsync(surveyQuestion.UserId);
            if (advertiser == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_AdvertiserNotFound);
            }

            return surveyQuestion;
        }

        /// <summary>
        /// Sets up accounts for transcations
        /// Verifies if required accounts exist
        /// </summary>
        private void SetupSurveyApproveTransaction(SurveyQuestion surveyQuestion, out Account advertisersAccount, out Account smdAccount)
        {
            advertisersAccount = accountRepository.GetByUserId(surveyQuestion.UserId);
            if (advertisersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Advertiser"));
            }

            smdAccount = accountRepository.GetByName(Accounts.Smd);
            if (smdAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "SMD"));
            }
        }

        /// <summary>
        /// Performs Transactions on Survey Approval
        /// </summary>
        /// <param name="request">Survey Approve Request object</param>
        /// <param name="advertisersAccount">Advertisers account</param>
        /// <param name="approvedSurveyAmount">Survey Amount</param>
        /// <param name="smdsCut"></param>
        /// <param name="smdAccount"></param>
        /// <param name="surveyQuestion"></param>
        /// <param name="isApiCall"></param>
        private void PerformSurveyApproveTransactions(ApproveSurveyRequest request, Account advertisersAccount, double approvedSurveyAmount,
            double? smdsCut, Account smdAccount, SurveyQuestion surveyQuestion, bool isApiCall = true)
        {
            // Debit Advertiser
            var transactionSequence = 1;

            // Perform the transactions
            // Debit Survey Advertiser
            PerformTransaction(null, request.SurveyQuestionId, advertisersAccount, approvedSurveyAmount, transactionSequence, TransactionType.ApproveSurvey, false);

            // Credit SMD
            transactionSequence += 1;
            PerformTransaction(null, request.SurveyQuestionId, smdAccount, smdsCut, transactionSequence, TransactionType.ApproveSurvey);

            // If called locally
            if (!isApiCall)
            {
                return;
            }

            // Mark survey as approved
            surveyQuestion.Approved = true;
            surveyQuestion.ApprovedByUserId = request.UserId;

            // Save Changes
            transactionRepository.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Logs in user to system
        /// </summary>
        private static void LoginUser(string email)
        {
            // Log user in
            var identity = new ClaimsIdentity(email);
            HttpContext.Current.User = new ClaimsPrincipal(identity);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebApiUserService(IEmailManagerService emailManagerService, IAdCampaignRepository adCampaignRepository, 
            IAccountRepository accountRepository, ITransactionRepository transactionRepository, ISurveyQuestionRepository surveyQuestionRepository)
        {
            if (emailManagerService == null)
            {
                throw new ArgumentNullException("emailManagerService");
            }
            if (adCampaignRepository == null)
            {
                throw new ArgumentNullException("adCampaignRepository");
            }
            if (accountRepository == null)
            {
                throw new ArgumentNullException("accountRepository");
            }
            if (transactionRepository == null)
            {
                throw new ArgumentNullException("transactionRepository");
            }
            if (surveyQuestionRepository == null)
            {
                throw new ArgumentNullException("surveyQuestionRepository");
            }

            this.emailManagerService = emailManagerService;
            this.adCampaignRepository = adCampaignRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
        }

        #endregion

        #region Public

        #region Approve Survey

        /// <summary>
        /// Update Transactions on viewing ad
        /// </summary>
        public async Task<BaseApiResponse> UpdateTransactionOnSurveyApproval(ApproveSurveyRequest request, bool isApiCall = true)
        {
            // Get Survey Approver
            User surveyApprover = await UserManager.FindByIdAsync(request.UserId);
            if (surveyApprover == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Validates if Ad Campaing Exists and has Advertiser info as well
            var surveyQuestion = await ValidateSurveyQuestion(request);
            
            // Begin Transaction
            // SMD will get 100 %
            double approvedSurveyAmount = request.Amount;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            SetupSurveyApproveTransaction(surveyQuestion, out advertisersAccount, out smdAccount);
            
            // Perform Transactions
            PerformSurveyApproveTransactions(request, advertisersAccount, approvedSurveyAmount, approvedSurveyAmount, smdAccount, surveyQuestion);

            return new BaseApiResponse
            {
                Status = true,
                Message = "Success"
            };
        }
        
        #endregion

        /// <summary>
        /// Update Transactions on viewing ad
        /// </summary>
        public async Task<BaseApiResponse> UpdateTransactionOnViewingAd(AdViewedRequest request)
        {
            // Get Ad Viewer
            User adViewer = await UserManager.FindByIdAsync(request.UserId);
            if (adViewer == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }
            
            // Validates if Ad Campaing Exists
            var adCampaign = await ValidateAdCampaign(request);
            
            // Get Referral if any
            User referringUser = null;
            Account affiliatesAccount = null;
            if (!string.IsNullOrEmpty(adViewer.ReferringUserId))
            {
                referringUser = await UserManager.FindByIdAsync(adViewer.ReferringUserId);
                if (referringUser == null)
                {
                    throw new SMDException(LanguageResources.WebApiUserService_ReferrerNotFound);
                }

                affiliatesAccount = accountRepository.GetByUserId(adViewer.ReferringUserId);
                if (affiliatesAccount == null)
                {
                    throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.WebApiUserService_AccountNotFound, "Affiliate"));
                }
            }

            // Begin Transaction
            // Ad Viewer will get 50% and other 50% will be divided b/w SMD (30%), Affiliate(20%) (Referrer) if exists
            double? adClickRate = adCampaign.ClickRate ?? 0;
            double? adViewersCut = ((adClickRate * 50) / 100);
            double? smdsCut = adViewersCut;
            Account adViewersAccount;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            SetupAdCampaignTransaction(request, adCampaign, out adViewersAccount, out advertisersAccount, out smdAccount);

            // Perform Transactions
            PerformAdCampaignTransactions(request, advertisersAccount, adClickRate, adViewersAccount, adViewersCut, referringUser, smdsCut, 
                affiliatesAccount, smdAccount, adCampaign);

            return new BaseApiResponse
                   {
                       Status = true,
                       Message = "Success"
                   };
        }
        
        /// <summary>
        /// Archive Account
        /// </summary>
        public async Task<BaseApiResponse> Archive(string userId)
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

            return new BaseApiResponse
                   {
                       Status = true,
                       Message = "Success"
                   };
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        public async Task<BaseApiResponse> UpdateProfile(UpdateUserProfileRequest request)
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

            return new BaseApiResponse
            {
                Status = true,
                Message = "Success"
            };
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        public async Task<BaseApiResponse> ConfirmEmail(string userId, string code)
        {
            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (!result.Succeeded)
            {
                return ThrowRegisterUserErrors(result);
            }

            await emailManagerService.SendRegisrationSuccessEmail(userId);

            // Login user
            LoginUser(userId);

            return new BaseApiResponse
                   {
                       Status = true,
                       Message = "Success"
                   };
        }

        /// <summary>
        /// Perform Custom Registration
        /// </summary>
        public async Task<LoginResponse> RegisterCustom(RegisterCustomRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName };
            var result = await UserManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return ThrowRegisterUserErrors(result);
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

            return new LoginResponse
            {
                Status = true,
                Message = "Success",
                User = user
            };
        }

        /// <summary>
        /// Perform External Registration
        /// </summary>
        public async Task<LoginResponse> RegisterExternal(RegisterExternalRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName };
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return ThrowRegisterUserErrors(result);
            }

            var login = new UserLoginInfo(request.LoginProvider, request.LoginProviderKey);
            result = await UserManager.AddLoginAsync(user.Id, login);
            if (!result.Succeeded)
            {
                return ThrowRegisterUserErrors(result);
            }

            // Login user
            LoginUser(request.Email);

            return new LoginResponse
            {
                Status = true,
                Message = "Success",
                User = user
            };
        }
        
        /// <summary>
        /// Perform External Login
        /// </summary>
        public async Task<LoginResponse> ExternalLogin(ExternalLoginRequest request)
        {
            User user = await UserManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                if (user.UserLogins == null)
                {
                    return new LoginResponse
                    {
                        Message = LanguageResources.WebApiUserService_LoginInfoNotFound
                    };
                }

                UserLogin userLoginInfo = user.UserLogins.FirstOrDefault(
                    u => u.LoginProvider == request.LoginProvider && u.ProviderKey == request.LoginProviderKey);

                if (userLoginInfo == null)
                {
                    return new LoginResponse
                    {
                        Message = LanguageResources.WebApiUserService_ProviderKeyInvalid
                    };
                }

                if (user.Status == (int)UserStatus.InActive)
                {
                    return new LoginResponse
                    {
                        Message = LanguageResources.WebApiUserService_InactiveUser
                    };
                }

                // Login user
                LoginUser(request.Email);

                return new LoginResponse
                {
                    Status = true,
                    Message = "Success",
                    User = user
                };
            }

            return await RegisterExternal(new RegisterExternalRequest{ Email = request.Email, FullName = request.FullName, 
                LoginProvider = request.LoginProvider, LoginProviderKey = request.LoginProviderKey});
        }

        /// <summary>
        /// Standard Login
        /// </summary>
        public async Task<LoginResponse> StandardLogin(StandardLoginRequest request)
        {
            User user = await UserManager.FindAsync(request.UserName, request.Password);
            if (user == null)
            {
                return new LoginResponse
                       {
                           Message = LanguageResources.WebApiUserService_InvalidCredentials
                       };
            }

            if (!user.EmailConfirmed)
            {
                return new LoginResponse
                {
                    Message = LanguageResources.WebApiUserService_EmailNotVerified
                };    
            }

            if (user.Status == (int)UserStatus.InActive)
            {
                return new LoginResponse
                {
                    Message = LanguageResources.WebApiUserService_InactiveUser
                };
            }

            // Login user
            LoginUser(request.UserName);

            return new LoginResponse
                       {
                           Status = true,
                           Message = "Success",
                           User = user
                       };
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


        /// <summary>
        /// Get Stripe Customer by Email
        /// </summary>
        public async Task<string> GetStripeCustomerIdByEmail(string email)
        {
            User user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new SMDException("No such user with provided email address!");
            }

            return user.StripeCustomerId; 
        }
        #endregion
    }
}
