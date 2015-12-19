using System;
using System.Globalization;
using System.Linq;
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

        #region Ad Campaign Transactions

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
            var transactionSequence = PerformDebitTransactionForAdvertiser(request, advertisersAccount, adClickRate);

            // Credit AdViewer
            transactionSequence += 1;
            PerformCreditTransactionForAdViewer(request, adViewersAccount, adViewersCut, transactionSequence);

            // Credit Affiliate
            smdsCut = PerformCreditTransactionForAffiliate(request, referringUser, smdsCut, affiliatesAccount, ref transactionSequence);

            // Credit SMD
            transactionSequence += 1;
            var creditSmdsTransaction = PerformCreditTransactionForSmd(request, smdsCut, smdAccount, transactionSequence);

            // Update AdCampaign Amount Spent
            adCampaign.AmountSpent += adClickRate;

            // Add Transcation to repository
            transactionRepository.Add(creditSmdsTransaction);

            // Save Changes
            transactionRepository.SaveChanges();
        }

        /// <summary>
        /// Credit SMD for Ad Campaing
        /// </summary>
        private static Transaction PerformCreditTransactionForSmd(AdViewedRequest request, double? smdsCut, Account smdAccount,
            int transactionSequence)
        {
            var creditSmdsTransaction = new Transaction
            {
                AccountId = smdAccount.AccountId,
                AdCampaignId = request.AdCampaignId,
                CreditAmount = smdsCut,
                Sequence = transactionSequence,
                Type = (int)TransactionType.AdClick,
                isProcessed = true,
                TransactionDate = DateTime.Now
            };

            // Update SMD Account Balance
            if (!smdAccount.AccountBalance.HasValue)
            {
                smdAccount.AccountBalance = 0;
            }
            smdAccount.AccountBalance += Convert.ToDecimal(smdsCut);
            return creditSmdsTransaction;
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
                var creditAffiliatesTransaction = new Transaction
                {
                    AccountId = affiliatesAccount.AccountId,
                    AdCampaignId = request.AdCampaignId,
                    CreditAmount = affiliatesCut,
                    Sequence = transactionSequence,
                    Type = (int)TransactionType.AdClick,
                    isProcessed = true,
                    TransactionDate = DateTime.Now
                };
                // Update Affiliates Account Balance
                if (!affiliatesAccount.AccountBalance.HasValue)
                {
                    affiliatesAccount.AccountBalance = 0;
                }
                affiliatesAccount.AccountBalance += Convert.ToDecimal(affiliatesCut);

                // Add Transcation to repository
                transactionRepository.Add(creditAffiliatesTransaction);
            }
            return smdsCut;
        }

        /// <summary>
        /// Credit Campaing Ad Viewer
        /// </summary>
        private void PerformCreditTransactionForAdViewer(AdViewedRequest request, Account adViewersAccount, double? adViewersCut,
            int transactionSequence)
        {
            var creditAdViewerTransaction = new Transaction
            {
                AccountId = adViewersAccount.AccountId,
                AdCampaignId = request.AdCampaignId,
                CreditAmount = adViewersCut,
                Sequence = transactionSequence,
                Type = (int)TransactionType.AdClick,
                isProcessed = true,
                TransactionDate = DateTime.Now
            };
            // Update Adviewers Account Balance
            if (!adViewersAccount.AccountBalance.HasValue)
            {
                adViewersAccount.AccountBalance = 0;
            }
            adViewersAccount.AccountBalance += Convert.ToDecimal(adViewersCut);

            // Add Transcation to repository
            transactionRepository.Add(creditAdViewerTransaction);
        }

        /// <summary>
        /// Debit Campaing Advertiser
        /// </summary>
        private int PerformDebitTransactionForAdvertiser(AdViewedRequest request, Account advertisersAccount,
            double? adClickRate)
        {
            // Debit Advertiser
            const int transactionSequence = 1;
            var debitAdvertiserTransaction = new Transaction
            {
                AccountId = advertisersAccount.AccountId,
                AdCampaignId = request.AdCampaignId,
                DebitAmount = adClickRate,
                Sequence = transactionSequence,
                Type = (int)TransactionType.AdClick,
                isProcessed = true,
                TransactionDate = DateTime.Now
            };

            // Update Advertisers Account Balance
            if (!advertisersAccount.AccountBalance.HasValue)
            {
                advertisersAccount.AccountBalance = 0;
            }
            advertisersAccount.AccountBalance -= Convert.ToDecimal(adClickRate);

            // Add Transcation to repository
            transactionRepository.Add(debitAdvertiserTransaction);

            return transactionSequence;
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

        #endregion
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebApiUserService(IEmailManagerService emailManagerService, IAdCampaignRepository adCampaignRepository, 
            IAccountRepository accountRepository, ITransactionRepository transactionRepository)
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

            this.emailManagerService = emailManagerService;
            this.adCampaignRepository = adCampaignRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        #endregion

        #region Public

        /// <summary>
        /// Update Transactions on viewing ad
        /// </summary>
        public async Task UpdateTransactionOnViewingAd(AdViewedRequest request)
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
            if (user != null)
            {
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

                if (user.Status == (int)UserStatus.InActive)
                {
                    throw new SMDException(LanguageResources.WebApiUserService_InactiveUser);
                }

                return user;
            }

            user = await RegisterExternal(new RegisterExternalRequest{ Email = request.Email, FullName = request.FullName, 
                LoginProvider = request.LoginProvider, LoginProviderKey = request.LoginProviderKey});

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
