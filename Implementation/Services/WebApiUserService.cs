using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMD.Common;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using com.esendex.sdk.messaging;
using System.Text.RegularExpressions;
using System.Security.Policy;

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
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly ITaxRepository taxRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IIndustryRepository industryRepository;
        private readonly IEducationRepository educationRepository;
        private readonly ICityRepository cityRepository;
        private readonly IProfileQuestionUserAnswerService profileQuestionAnswerService;
        private readonly IProfileQuestionService profileQuestionService;
        private readonly IAdCampaignResponseRepository adCampaignResponseRepository;
        private readonly ISurveyQuestionResponseRepository surveyQuestionResponseRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IManageUserRepository manageUserRepository;
        private readonly IAccountService accountService;
        private readonly IProfileQuestionRepository profileQuestionRepository;
        private readonly ISmsServiceCustom smsService;
        private readonly IGameRepository gameRepositry;
        private readonly ICouponRatingReviewRepository couponRatingReviewRepository;

        private readonly IAspnetUsersRepository aspnetUsersRepository;

        private readonly IAspNetUsersNotificationTokenRepository aspNetUsersNotificationTokenRepository;

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>(); }
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
        /// <param name="isProcessed">Tranation status </param>
        private void PerformTransaction(long? adCampaingId, long? surveyQuestionId, Account account, double? transactionAmount,
            int transactionSequence, TransactionType transactionType, bool isCredit = true, bool isProcessed = false, int PQID = 0)
        {
            var transaction = new Transaction
                                             {
                                                 AccountId = account.AccountId,
                                                 Sequence = transactionSequence,
                                                 Type = (int)transactionType,
                                                 isProcessed = isProcessed,
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
                transaction.AccountBalance = (account.AccountBalance ?? 0) + transactionAmount;
                account.AccountBalance += transactionAmount;
              
            }
            else
            {
                transaction.DebitAmount = transactionAmount;
                transaction.AccountBalance = (account.AccountBalance ?? 0) - transactionAmount;
                account.AccountBalance -= transactionAmount;
                
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
        /// <param name="adViewerUsedVoucher">If Ad Viewer Uses Voucher</param>
        private async Task PerformAdCampaignClickTransactions(long AdCampaignId, Account advertisersAccount, double? adClickRate,
            Account adViewersAccount, double? adViewersCut, Company referringCompany, double? smdsCut, Account affiliatesAccount,
            Account smdAccount, AdCampaign adCampaign, bool ReferralScenario, double? referralCut)
        {
            // Debit Advertiser
            var transactionSequence = 1;

            // Perform the transactions
            // Debit Campaign Advertiser
            PerformTransaction(AdCampaignId, null, advertisersAccount, adClickRate, transactionSequence, TransactionType.AdClick, false);

           
            transactionSequence += 1;
            PerformTransaction(AdCampaignId, null, adViewersAccount, adViewersCut, transactionSequence, TransactionType.AdClick,true);

  
            // Credit Affiliate
            //todo pilot: Commenting Smd Transaction
            if (ReferralScenario)
            {
                transactionSequence += 1;
                PerformTransaction(AdCampaignId, null, affiliatesAccount, referralCut, transactionSequence, TransactionType.AdClick);
            }
            
            
            // Credit SMD
            //todo pilot: Commenting Smd Transaction
            transactionSequence += 1;
            PerformTransaction(AdCampaignId, null, smdAccount, smdsCut, transactionSequence, TransactionType.AdClick, true, true);

            // Update AdCampaign Amount Spent
            if (!adCampaign.AmountSpent.HasValue)
            {
                adCampaign.AmountSpent = 0;
            }
            adCampaign.AmountSpent += adClickRate;

          

            // Save Changes
            transactionRepository.SaveChanges();
          
        }


        /// <summary>
        /// Sets up accounts for transcations
        /// Verifies if required accounts exist
        /// </summary>
        private void SetupAdCampaignTransaction(ProductActionRequest request, AdCampaign adCampaign,
            out Account adViewersAccount, out Account advertisersAccount, out Account smdAccount, string systemUserId)
        {

            // Get business Accounts for Each Individual involved in this transaction
            adViewersAccount = accountRepository.GetByUserId(request.UserId, AccountType.VirtualAccount);
            if (adViewersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Current User"));
            }

            advertisersAccount = accountRepository.GetByUserId(adCampaign.UserId, AccountType.VirtualAccount);
            if (advertisersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Advertiser"));
            }

            smdAccount = accountRepository.GetByUserId(systemUserId, AccountType.VirtualAccount);
            if (smdAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Cash4Ads"));
            }
        }

        /// <summary>
        /// Validates Ad Campaing
        /// AdCampaign Exists
        /// </summary>
        private async Task<AdCampaign> ValidateAdCampaign(long AdCampaignId)
        {
            // Get AdCampaign against AdCampaign Id
            AdCampaign adCampaign = adCampaignRepository.Find(AdCampaignId);
            if (adCampaign == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AdCampaignNotFound,
                    AdCampaignId));
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
        /// Invoice + Details work 
        /// </summary>
        private void AddInvoiceDetails(SurveyQuestion source, double amount, string stripeResponse, int? productId,
            double taxValue, bool isApiCall = true)
        {
            #region Add Invoice

            // Add invoice data
            var invoice = new Invoice
            {
                Country = source.Country.CountryName,
                Total = amount,
                NetTotal = amount,
                InvoiceDate = DateTime.Now,
                InvoiceDueDate = DateTime.Now.AddDays(7),
                Address1 = source.Country.CountryName,
                CompanyId = source.CompanyId,
                CompanyName = "Cash4Ads",
                CreditCardRef = stripeResponse
            };
            invoiceRepository.Add(invoice);

            #endregion
            #region Add Invoice Detail

            // Add Invoice Detail Data 
            var invoiceDetail = new InvoiceDetail
            {
                InvoiceId = invoice.InvoiceId,
                SqId = source.SqId,
                ProductId = productId,
                ItemName = "Survey Question",
                ItemAmount = amount,
                ItemTax = taxValue,
                ItemDescription = "This is description!",
                ItemGrossAmount = amount,
                CampaignId = null,

            };
            invoiceDetailRepository.Add(invoiceDetail);
            if (!isApiCall)
            {
                invoiceDetailRepository.SaveChanges();
            }
            #endregion
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

        ///// <summary>
        ///// Updates Profile Image
        ///// </summary>
        //private static void UpdateProfileImage(UpdateUserProfileRequest request, User user)
        //{
        //    string smdContentPath = ConfigurationManager.AppSettings["SMD_Content"];
        //    HttpServerUtility server = HttpContext.Current.Server;
        //    string mapPath = server.MapPath(smdContentPath + "/Users/" + user.CompanyId);

        //    // Create directory if not there
        //    if (!Directory.Exists(mapPath))
        //    {
        //        Directory.CreateDirectory(mapPath);
        //    }

        //    user.ProfileImage = ImageHelper.Save(mapPath, user.ProfileImage, string.Empty, request.ProfileImageName,
        //        request.ProfileImage, request.ProfileImageBytes);
        //     string imgExt = Path.GetExtension(user.ProfileImage);
        //     string sourcePath = HttpContext.Current.Server.MapPath("~/"+user.ProfileImage);
        //     string[] results = sourcePath.Split(new string[] { imgExt }, StringSplitOptions.None);
        //     string res = results[0];
        //     string destPath = res + "_thumb" + imgExt;
        //     ImageHelper.GenerateThumbNail(sourcePath, sourcePath, 200);
        //}
        
        #region Product Response Actions

        /// <summary>
        /// Update Profile Question User Answer
        /// </summary>
        /// <param name="response"></param>
        private void ExecuteProfileQuestionEvent(ProductActionRequest response)
        {


            if (response.PqAnswerIds == null)
            {
                response.PqAnswerIds = new List<int>();
            }

            //recording response
            profileQuestionAnswerService.SaveProfileQuestionUserResponse(
               new UpdateProfileQuestionUserAnswerApiRequest
               {
                   ProfileQuestionAnswerIds = response.PqAnswerIds,
                   ProfileQuestionId = (int)response.ItemId.Value,
                   UserId = response.UserId,
                   companyId = response.companyId.Value,
                   ResponeEventType = response.ResponeEventType
               });

            var profileQuestion = profileQuestionRepository.Find((int)response.ItemId.Value);
            //rewarding and increasing count happens only when they answer it.
            if (response.ResponeEventType == CampaignResponseEventType.Answered)
            {


                //updating answer count
                profileQuestion.AsnswerCount = (profileQuestion.AsnswerCount.HasValue ? profileQuestion.AsnswerCount.Value : 0) + 1;
                profileQuestionRepository.Update(profileQuestion);
                profileQuestionRepository.SaveChanges();


                //since its a purchased ProfileQuestion hence reward the end user
                if (profileQuestion.CompanyId != null)
                {

                    UpdateTransactionOnPaidProfileQuestionAnswer(new AdViewedRequest
                  {
                      AdCampaignId = response.ItemId.Value,
                      UserId = response.UserId,
                      companyId = response.companyId.Value,
                      userQuizSelection = response.UserQuestionResponse,

                  }, response, 2, profileQuestion.CompanyId.Value);

                }
            }
            else if (response.ResponeEventType == CampaignResponseEventType.Skip)
            {
                profileQuestion.SkippedCount = (profileQuestion.SkippedCount.HasValue ? profileQuestion.SkippedCount.Value : 0) + 1;
                profileQuestionRepository.Update(profileQuestion);
                profileQuestionRepository.SaveChanges();
            }


        }

        /// <summary>
        /// Execute AdClicked / Viewed
        /// </summary>
        private async Task<BaseApiResponse> ExecuteVideoOrGameEvent(ProductActionRequest request)
        {

            double? adViewersCut = 0;
            // Get Ad Viewer
            User adViewer = await UserManager.FindByIdAsync(request.UserId);
            if (adViewer == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            User cash4Ads = await UserManager.FindByEmailAsync(SystemUsers.Cash4Ads);
            if (cash4Ads == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            var adCampaign = await ValidateAdCampaign(request.ItemId.Value);

            if (request.ResponeEventType == CampaignResponseEventType.Answered)
            {

                // Validates if Ad Campaing Exists
               
                bool ReferralScenario = false;
                Company referringCompany = null;
                Account referringCompanyAccount = null;
                var advertiserCompany = companyRepository.Find(adCampaign.CompanyId.Value);
               


                // Get Referral if any
                if (advertiserCompany.ReferringCompanyID.HasValue)
                {
                    referringCompany = companyRepository.Find(advertiserCompany.ReferringCompanyID.Value);
                    ReferralScenario = true;

                    if (referringCompany == null)
                    {
                        throw new SMDException(LanguageResources.WebApiUserService_ReferrerNotFound);
                    }

                    referringCompanyAccount = accountRepository.GetByCompanyId(referringCompany.CompanyId, AccountType.VirtualAccount);
                    if (referringCompanyAccount == null)
                    {
                        throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                            LanguageResources.WebApiUserService_AccountNotFound, "Affiliate"));
                    }
                }


                // Ad Viewer will get 50% and other 50% will be divided b/w SMD (30%), Affiliate(20%) (Referrer) if exists
                double? adClickRate = adCampaign.ClickRate ?? 0;

                adClickRate = adClickRate * 100;

                adViewersCut = adClickRate / 2; // commenting for pilot launch, now smd hace no percentage and all the money will go to user account from advertiser account. So there will be 2 transactions now advertiser debit transaction and user credit transaction ((adClickRate * 50) / 100); 
                double? smdsCut = 0;
                double? referralCut = 0;


                if (ReferralScenario)
                {
                    smdsCut = adClickRate / 4; //adViewersCut;
                    referralCut = adClickRate / 4;
                }
                else
                {
                    smdsCut = adClickRate / 2;
                    referralCut = 0;
                }

                Account adViewersAccount;
                Account advertisersAccount;
                Account smdAccount;

                // Sets up transaction 
                // Gets Accounts required
                SetupAdCampaignTransaction(request, adCampaign, out adViewersAccount, out advertisersAccount, out smdAccount, cash4Ads.Id);

                // Perform financial Transactions
                await PerformAdCampaignClickTransactions(request.ItemId.Value, advertisersAccount, adClickRate, adViewersAccount, adViewersCut, referringCompany, smdsCut,
                    referringCompanyAccount, smdAccount, adCampaign, ReferralScenario, referralCut);

            }


            // Add Campaign Response recording

            AdCampaignResponse adCampaignResponse = adCampaignResponseRepository.Create();
            adCampaignResponse.CampaignId = request.ItemId.Value;
            adCampaignResponse.UserId = request.UserId;
            adCampaignResponse.UserLocationLat = request.UserLocationLat;
            adCampaignResponse.UserLocationLong = request.UserLocationLong;
            adCampaignResponse.UserLocationCity = request.City;
            adCampaignResponse.UserLocationCountry = request.Country;
            adCampaignResponse.CompanyId = request.companyId;
            adCampaignResponse.UserQuestionResponse = request.UserQuestionResponse;
            adCampaignResponse.UserSelection = request.UserQuestionResponse;
            adCampaignResponse.ResponseType = (int)request.ResponeEventType;
            adCampaignResponse.EndUserDollarAmount = adViewersCut;
            adCampaignResponse.CreatedDateTime = DateTime.Now;

            adCampaignResponseRepository.Add(adCampaignResponse);
            adCampaign.AdCampaignResponses.Add(adCampaignResponse);
            adCampaignRepository.SaveChanges();



            return new BaseApiResponse
            {
                Status = true,
                Message = LanguageResources.Success
            };




        }

               



        /// <summary>
        /// Skips Survey Question
        /// </summary>
        private void ExecuteSurveyQuestionEvent(int sqId, string userId, int? userSelection, bool? isSkipped,int companyId, ProductActionRequest request)
        {
            SurveyQuestion surveyQuestion = surveyQuestionRepository.Find(sqId);
            if (surveyQuestion == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_SurveyQuestionNotFound, sqId));
            }

            //adding response
            SurveyQuestionResponse sqResponse = surveyQuestionResponseRepository.Create();
            sqResponse.SqId = sqId;
            sqResponse.UserId = userId;
            sqResponse.ResponseType = (int)request.ResponeEventType;
            if (userSelection.HasValue)
            {
                sqResponse.UserSelection = userSelection;
            }
            sqResponse.CompanyId = companyId;
            sqResponse.ResoponseDateTime = DateTime.Now.Add(-(surveyQuestionRepository.UserTimezoneOffSet));

            surveyQuestionResponseRepository.Add(sqResponse);
            surveyQuestion.SurveyQuestionResponses.Add(sqResponse);

            //increment counter
            if ( request.ResponeEventType != CampaignResponseEventType.Skip)
                surveyQuestion.ResultClicks = (surveyQuestion.ResultClicks.HasValue  ? surveyQuestion.ResultClicks.Value : 0) +  1;

            surveyQuestionRepository.Update(surveyQuestion);
            surveyQuestionRepository.SaveChanges();


            //since its a purchased ProfileQuestion hence reward the end user and its an answered event

            if (surveyQuestion.CompanyId != null && request.ResponeEventType == CampaignResponseEventType.Answered)
            {

                UpdateTransactionOnPaidSurveyAnswer(new AdViewedRequest
                {
                    AdCampaignId = request.ItemId.Value,
                    UserId = request.UserId,
                    companyId = request.companyId.Value,
                    userQuizSelection = request.UserQuestionResponse,

                }, request, 2, surveyQuestion.CompanyId.Value);

            }
        }

      

       



        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebApiUserService(IEmailManagerService emailManagerService, IAdCampaignRepository adCampaignRepository,
            IAccountRepository accountRepository, ITransactionRepository transactionRepository,
            ISurveyQuestionRepository surveyQuestionRepository, IInvoiceRepository invoiceRepository,
            IInvoiceDetailRepository invoiceDetailRepository, IProductRepository productRepository,
            ITaxRepository taxRepository, IProfileQuestionUserAnswerService profileQuestionAnswerService,
            ICountryRepository countryRepository, IIndustryRepository industryRepository,
            IProfileQuestionService profileQuestionService, IAdCampaignResponseRepository adCampaignResponseRepository,
            ISurveyQuestionResponseRepository surveyQuestionResponseRepository, IEducationRepository educationRepository, ICityRepository cityRepository, ICompanyRepository companyRepository, IManageUserRepository manageUserRepository, IAccountService accountService, IProfileQuestionRepository profileQuestionRepository, IAspnetUsersRepository aspnetUsersRepository, ISmsServiceCustom smsService, IGameRepository gameRepositry, ICouponRatingReviewRepository couponRatingReviewRepository, IAspNetUsersNotificationTokenRepository aspNetUsersNotificationTokenRepository)
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
            if (productRepository == null)
            {
                throw new ArgumentNullException("productRepository");
            }
            if (taxRepository == null)
            {
                throw new ArgumentNullException("taxRepository");
            }
            if (profileQuestionAnswerService == null)
            {
                throw new ArgumentNullException("profileQuestionAnswerService");
            }
            if (profileQuestionService == null)
            {
                throw new ArgumentNullException("profileQuestionService");
            }
            if (adCampaignResponseRepository == null)
            {
                throw new ArgumentNullException("adCampaignResponseRepository");
            }
            if (surveyQuestionResponseRepository == null)
            {
                throw new ArgumentNullException("surveyQuestionResponseRepository");
            }

            this.emailManagerService = emailManagerService;
            this.adCampaignRepository = adCampaignRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.invoiceRepository = invoiceRepository;
            this.invoiceDetailRepository = invoiceDetailRepository;
            this.productRepository = productRepository;
            this.taxRepository = taxRepository;
            this.countryRepository = countryRepository;
            this.industryRepository = industryRepository;
            this.profileQuestionAnswerService = profileQuestionAnswerService;
            this.profileQuestionService = profileQuestionService;
            this.adCampaignResponseRepository = adCampaignResponseRepository;
            this.surveyQuestionResponseRepository = surveyQuestionResponseRepository;
            this.educationRepository = educationRepository;
            this.cityRepository = cityRepository;
            this.companyRepository = companyRepository;
            this.manageUserRepository = manageUserRepository;
            this.accountService = accountService;
            this.profileQuestionRepository = profileQuestionRepository;
            this.aspnetUsersRepository = aspnetUsersRepository;
            this.smsService = smsService;
            this.gameRepositry = gameRepositry;
            this.couponRatingReviewRepository = couponRatingReviewRepository;
            this.aspNetUsersNotificationTokenRepository = aspNetUsersNotificationTokenRepository;
        }


        #endregion
        #region Public

      

        #region Ad Approve

      


        public async Task<BaseApiResponse> UpdateTransactionOnPaidSurveyAnswer(AdViewedRequest request, ProductActionRequest pRequest, double rewardCentz, int AdvertiserCompanyId)
        {
            // Get Ad Viewer
            User adViewer = await UserManager.FindByIdAsync(request.UserId);
            if (adViewer == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            User cash4Ads = await UserManager.FindByEmailAsync(SystemUsers.Cash4Ads);
            if (cash4Ads == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            // Validates if Ad Campaing Exists
          

            // Begin Transaction
            // Ad Viewer will get 50% and other 50% will be divided b/w SMD (30%), Affiliate(20%) (Referrer) if exists
            double? adClickRate = rewardCentz;
            double? adViewersCut = adClickRate; // commenting for pilot launch, now smd hace no percentage and all the money will go to user account from advertiser account. So there will be 2 transactions now advertiser debit transaction and user credit transaction ((adClickRate * 50) / 100); 
            
         
            double? smdsCut = 0; //adViewersCut;
            Account adViewersAccount;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            // Get business Accounts for Each Individual involved in this transaction
                adViewersAccount = accountRepository.GetByUserId(request.UserId, AccountType.VirtualAccount);
                if (adViewersAccount == null)
                {
                    throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.WebApiUserService_AccountNotFound, "Current User"));
                }

                advertisersAccount = accountRepository.GetByCompanyId(AdvertiserCompanyId, AccountType.VirtualAccount);
                if (advertisersAccount == null)
                {
                    throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.WebApiUserService_AccountNotFound, "Advertiser"));
                }

                smdAccount = accountRepository.GetByUserId(cash4Ads.Id, AccountType.VirtualAccount);
                if (smdAccount == null)
                {
                    throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                        LanguageResources.WebApiUserService_AccountNotFound, "Cash4Ads"));
                }

            // Perform Transactions

            PerformTransaction(request.AdCampaignId, null, advertisersAccount, adClickRate, 1, TransactionType.SurveyWatched, false);

            // Credit AdViewer
            PerformTransaction(request.AdCampaignId, null, adViewersAccount, adClickRate, 2, TransactionType.SurveyWatched);
            transactionRepository.SaveChanges();

            return new BaseApiResponse
            {
                Status = true,
                Message = LanguageResources.Success
            };
        }


        public async Task<BaseApiResponse> UpdateTransactionOnPaidProfileQuestionAnswer(AdViewedRequest request, ProductActionRequest pRequest, double rewardCentz, int AdvertiserCompanyId)
        {
            // Get Ad Viewer
            User adViewer = await UserManager.FindByIdAsync(request.UserId);
            if (adViewer == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            User cash4Ads = await UserManager.FindByEmailAsync(SystemUsers.Cash4Ads);
            if (cash4Ads == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            // Validates if Ad Campaing Exists


            // Begin Transaction
            // Ad Viewer will get 50% and other 50% will be divided b/w SMD (30%), Affiliate(20%) (Referrer) if exists
            double? adClickRate = rewardCentz;
            double? adViewersCut = adClickRate; // commenting for pilot launch, now smd hace no percentage and all the money will go to user account from advertiser account. So there will be 2 transactions now advertiser debit transaction and user credit transaction ((adClickRate * 50) / 100); 
            double? smdsCut = 0; //adViewersCut;
            Account adViewersAccount;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            // Get business Accounts for Each Individual involved in this transaction
            adViewersAccount = accountRepository.GetByUserId(request.UserId, AccountType.VirtualAccount);
            if (adViewersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Current User"));
            }

            advertisersAccount = accountRepository.GetByCompanyId(AdvertiserCompanyId, AccountType.VirtualAccount);
            if (advertisersAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Advertiser"));
            }

            smdAccount = accountRepository.GetByUserId(cash4Ads.Id, AccountType.VirtualAccount);
            if (smdAccount == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AccountNotFound, "Cash4Ads"));
            }

            // Perform Transactions
          

            PerformTransaction(request.AdCampaignId, null, advertisersAccount, adClickRate, 1, TransactionType.ProfileQuestionAnswered, false);

            // Credit AdViewer


            PerformTransaction(request.AdCampaignId, null, adViewersAccount, adViewersCut, 2, TransactionType.ProfileQuestionAnswered);


            transactionRepository.SaveChanges();

            return new BaseApiResponse
            {
                Status = true,
                Message = LanguageResources.Success
            };
        }
        


        #endregion

        #region Other




        /// <summary>
        /// Get User By Id Async
        /// </summary>
        public async Task<LoginResponse> GetById(string userId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            return new LoginResponse
                   {
                       Status = true,
                       Message = LanguageResources.Success,
                       User = user
                   };
        }

        /// <summary>
        /// Resets User Responses for Products
        /// </summary>
        public void ResetProductsResponses()
        {
            adCampaignRepository.ResetUserProductsResponses();
        }

        /// <summary>
        /// Handles Response for Products
        /// Ad Clicked/Viewed, Question Answered, Survey Selection
        /// </summary>
        public async Task<BaseApiResponse> ExecuteActionOnProductsResponse(ProductActionRequest response)
        {
            //if (request.Type)
            //{
            //    throw new SMDException(LanguageResources.WebApiUserService_ProductTypeNotProvided);
            //}

            // Check if user exists
            User user = await UserManager.FindByIdAsync(response.UserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Profile Question  Event
            // (int?)ProductType.Question 3
            if ( response.Type ==  CampaignType.ProfileQuestion)
                ExecuteProfileQuestionEvent(response);

            // Ad Clicked / Viewed
            if (response.Type == CampaignType.GameAd || response.Type == CampaignType.VideoAd)
                await ExecuteVideoOrGameEvent(response);

            // Update Survey User Selection
            if (response.Type == CampaignType.SurveyQuestion)
            {
                ExecuteSurveyQuestionEvent((int)response.ItemId.Value, response.UserId, response.SqUserSelection, null, response.companyId.Value, response);
            }


            return new BaseApiResponse
            {
                Status = true,
                Message = LanguageResources.Success
            };
        }

        /// <summary>
        /// Gets Combination of Ads, Surveys, Questions as paged view
        /// </summary>
        public GetProductsResponse GetProducts(GetProductsRequest request)
        {
            List<GetProducts_Result> products = adCampaignRepository.GetProducts(request).ToList();

            //no ads found in this page then insert the manual pages
            if (products.FindAll( g=> g.Type == "Ad").Count == 0)
            {
                

                string companylogo = "";

                var specialads = adCampaignRepository.GetSpecialAdCampaigns(out companylogo);
                var gamead = specialads.Where(g => g.Type == 4).FirstOrDefault();
                var videoad = specialads.Where(g => g.Type == 1).FirstOrDefault();

                if (gamead != null)
                {
                    var game = gameRepositry.GetRandomGame();
                    var freeGame = new GetProducts_Result { ItemId = gamead.CampaignId, ItemName = gamead.CampaignName, Type = "freeGame", ItemType = 3, AdImagePath = "http://manage.cash4ads.com/" + gamead.LogoUrl, AdClickRate = 0, AdvertisersLogoPath = companylogo, BuyItImageUrl = "http://manage.cash4ads.com/" + gamead.BuyItImageUrl, GameId = game.GameId, GameUrl = game.GameUrl, AdVerifyQuestion = videoad.VerifyQuestion, AdAnswer1 = videoad.Answer1, AdAnswer2 = videoad.Answer2, AdAnswer3 = videoad.Answer3, AdCorrectAnswer = videoad.CorrectAnswer };
                    products.Insert(0, freeGame);
                }

                if (videoad != null)
                {
                    var freevideoAd = new GetProducts_Result { ItemId = videoad.CampaignId, ItemName = videoad.CampaignName, Type = "freeAd", ItemType = 1, AdImagePath = "http://manage.cash4ads.com/" + videoad.LogoUrl, AdClickRate = 0, AdvertisersLogoPath = companylogo, BuyItImageUrl = "http://manage.cash4ads.com/" + videoad.BuyItImageUrl, GameId = 1, GameUrl = "", AdVideoLink = videoad.LandingPageVideoLink, VideoLink2 = videoad.VideoLink2, AdVerifyQuestion = videoad.VerifyQuestion, AdAnswer1 = videoad.Answer1, AdAnswer2 = videoad.Answer2, AdAnswer3 = videoad.Answer3, AdCorrectAnswer = videoad.CorrectAnswer };
                    products.Insert(3, freevideoAd);
                }
            }

            return new GetProductsResponse
            {
                Status = true,
                Message = LanguageResources.Success,
                Products = products,
                TotalCount = products.Any() && products[0].TotalItems.HasValue ? products[0].TotalItems.Value : 0
            };
        }

        /// <summary>
        /// Archive Account
        /// </summary>
        public async Task<BaseApiResponse> ArchiveRequestConfirmation(string userId,string  token, string confirmationLink)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            user.DeleteConfirmationToken = token;
            UserManager.Update(user);

            await emailManagerService.SendDeleteAccountConfirmationEmail(user, confirmationLink);

            return new BaseApiResponse
                   {
                       Status = true,
                       Message = "Success"
                   };
        }


        /// <summary>
        /// Archive Account
        /// </summary>
        public bool Archive(string userId, string confirmationToken)
        {
            User user =  UserManager.FindById(userId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            if (user.DeleteConfirmationToken != confirmationToken)
            {
                throw new SMDException("Invalid confiramtion token or token expired");
            }

            Random rnd = new Random();
            int numb = rnd.Next(100);


            user.ContactNotes = "Archived user on app request=" + DateTime.Now.ToLongDateString() + ", old username=" + user.UserName + ", old email=" + user.Email;

            // Archive Account
            user.Status = (int)UserStatus.InActive;
            user.ModifiedDateTime = DateTime.Now;
            user.UserName = user.UserName + "_archived" + numb.ToString();
            user.Email = user.Email + "_archived" + numb.ToString();
            user.DeleteConfirmationToken = "";

            var company = companyRepository.Find(user.CompanyId.Value);
            if (company != null)
            {
                company.IsDeleted = true;
                company.DeleteDate = DateTime.Now;
                company.AboutUsDescription = user.ContactNotes;

                companyRepository.Update(company);
                companyRepository.SaveChanges();

            }


            //removing any provider logins.
            var logins = UserManager.GetLogins(userId);
            if (logins != null && logins.Count > 0)
                foreach (var item in logins)
                {
                    UserManager.RemoveLogin(userId, item);
                }


            // Save Changes
            UserManager.Update(user);

            //reset virtual accounts balance and transferring it back to Cash4ads virtual account
            TransactionManager.ArchiveUserResetVirtualAccountBalance(user.CompanyId.Value);

            return true;
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


            if (!string.IsNullOrEmpty(request.Title))
            {
                user.Title = request.Title;
            }

            if (request.DOB.HasValue)
            {
                user.DOB = request.DOB;
            }

            if (request.Gender.HasValue)
            {
                user.Gender = request.Gender;
            }
                user.IndustryId = request.ProfessionID;
                user.FullName= request.FullName;


            if (! string.IsNullOrEmpty( request.Phone1))
            {

                var phone = Regex.Replace(request.Phone1, @"\s+", "");
                phone = phone.Replace ("-","");
                user.Phone1 = phone;
            }

            user.Phone1CodeCountryID = request.Phone1CountryID;

        

            // Save Changes
           await UserManager.UpdateAsync(user);

            return new BaseApiResponse
            {
                Status = true,
                Message = "Success"
            };
        }

        ///// <summary>
        ///// Update Profile Image
        ///// </summary>
        //public async Task<UpdateProfileImageResponse> UpdateProfileImage(UpdateUserProfileRequest request)
        //{
        //    User user = await UserManager.FindByIdAsync(request.UserId);
            
        //    if (user == null)
        //    {
        //        throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
        //    }

        //    // Update Profile Image
        //    UpdateProfileImage(request, user);
        //    companyRepository.updateCompanyLogo(user.ProfileImage, user.CompanyId.Value);
        //    // Save Changes
        //   // await UserManager.UpdateAsync(user); // because now we will save profile image in company 

        //    return new UpdateProfileImageResponse
        //           {
        //               ImageUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + user.ProfileImage,
        //               Status = true,
        //               Message = "Success"
        //           };
        //}

        /// <summary>
        /// Get Logged-In User profile 
        /// </summary>
        public User GetLoggedInUser(string userid)
        {
            User user = null;
            if (!string.IsNullOrEmpty(userid) && userid != "0")
            {
                user = UserManager.FindById(userid);
            }
            else
            {
                user = UserManager.FindById(productRepository.LoggedInUserIdentity);
            }
           
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }
            return user;
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
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName, DOB = null, Status = 1 };
            var result = await UserManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return ThrowRegisterUserErrors(result);
            }

            var addUserToRoleResult = await UserManager.AddToRoleAsync(user.Id, SecurityRoles.EndUser_Admin); // Only Type 'User' Role will be registered from app
            if (!addUserToRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Format("Failed to add user to role {0}", SecurityRoles.EndUser_Admin));
            }
            int companyId = companyRepository.createCompany(user.Id, request.Email, request.FullName,Guid.NewGuid().ToString());
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                              "/Api_Mobile/Register/Confirm/?UserId=" + user.Id + "&Code=" + HttpUtility.UrlEncode(code);
            await
                emailManagerService.SendAccountVerificationEmail(user, callbackUrl);


            accountService.AddAccountsForNewUser(companyId);
            

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

            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName, DOB = null, Status = 1, ProfileImage = request.ProfilePicturePath, DevicePlatform = request.Client == "android" ? 1 : 0 };
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

            var addUserToRoleResult = await UserManager.AddToRoleAsync(user.Id, SecurityRoles.EndUser_Admin); // Only Type 'User' Role will be registered from app
            if (!addUserToRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Format("Failed to add user to role {0}", SecurityRoles.EndUser_Admin));
            }
            int companyId = companyRepository.createCompany(user.Id, request.Email, request.FullName, Guid.NewGuid().ToString());


            accountService.AddAccountsForNewUser(companyId);
            TransactionManager.UserSignupFreeGiftBalanceTransaction(500, companyId);


            //handling notificationtoken
            if (!string.IsNullOrEmpty(request.NotificationToken))
            {

                var token = new AspNetUsersNotificationToken { UserId = user.Id, ClientType = user.DevicePlatform, DateAdded = DateTime.Now, Token = request.NotificationToken };

                aspNetUsersNotificationTokenRepository.Add(token);
                aspNetUsersNotificationTokenRepository.SaveChanges();
            }

            // Login user
            LoginUser(request.Email);


            await emailManagerService.SendRegisrationSuccessEmail(user.Id);

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


                //updating logged in user
                if (user.Company != null)
                {
                    var company = companyRepository.Find(user.CompanyId.Value);
                    if ( company != null)
                    {
                        company.BillingCity = request.City;
                        company.City = request.City;

                        var country = countryRepository.GetSearchedCountries(request.Country).FirstOrDefault();
                        
                        if ( country != null)
                        {
                            company.BillingCountryId = country.CountryId;
                        }

                        companyRepository.Update(company);
                        companyRepository.SaveChanges();

                    }
                }
                else
                {
                   
                    var CompanyId = companyRepository.createCompany(user.Id, request.Email, request.FullName, user.AuthenticationToken);
                    accountService.AddAccountsForNewUser(CompanyId);
                    TransactionManager.UserSignupFreeGiftBalanceTransaction(500, CompanyId);
                    
                }

                UserLogin userLoginInfo = user.UserLogins.FirstOrDefault(
                    u => u.LoginProvider == request.LoginProvider);


                //provider did not match hence creating the provider profile.
                if (userLoginInfo == null)
                {

                    UserLogin oLogin = new UserLogin();
                    oLogin.LoginProvider = request.LoginProvider;
                    oLogin.ProviderKey = request.LoginProviderKey;
                    oLogin.UserId = user.Id;

                    user.UserLogins.Add(oLogin);
                    await UserManager.UpdateAsync(user);
                }
                else// we have the provider already now checking for the API key
                {
                    if ( userLoginInfo.ProviderKey !=  request.LoginProviderKey)
                    {
                        return new LoginResponse
                        {
                            Message = LanguageResources.WebApiUserService_ProviderKeyInvalid
                        };
                    }
                       
                }

                if (user.Status == (int)UserStatus.InActive)
                {
                    return new LoginResponse
                    {
                        Message = LanguageResources.WebApiUserService_InactiveUser
                    };
                }
                // update GUID 
                user.LastLoginTime = DateTime.Now;
                user.ProfileImage = request.ProfilePicturePath;
                user.DevicePlatform =  request.Client  == "android" ? 1 : 0;

                user.CityName = request.City;
                user.CountryName = request.Country;


                user.AuthenticationToken = Guid.NewGuid().ToString();
                
                await UserManager.UpdateAsync(user);


                //handling notificationtoken
                if ( !string.IsNullOrEmpty( request.NotificationToken ))
                {

                    var token = new AspNetUsersNotificationToken{ UserId = user.Id, ClientType = user.DevicePlatform, DateAdded = DateTime.Now, Token = request.NotificationToken};

                    aspNetUsersNotificationTokenRepository.Add(token);
                    aspNetUsersNotificationTokenRepository.SaveChanges();
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

            return await RegisterExternal(new RegisterExternalRequest
            {
                Email = request.Email,
                FullName = request.FullName,
                LoginProvider = request.LoginProvider,
                LoginProviderKey = request.LoginProviderKey,
                 Client = request.Client,
                  NotificationToken = request.NotificationToken,
                  ProfilePicturePath = request.ProfilePicturePath
            });
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

            //if (user.Status == (int)UserStatus.InActive)
            //{
            //    return new LoginResponse
            //    {
            //        Message = LanguageResources.WebApiUserService_InactiveUser
            //    };
            //}  // always unarchive  user
            user.Status = (int)UserStatus.Active;
            // update GUID 
            user.AuthenticationToken = Guid.NewGuid().ToString();
           
            await UserManager.UpdateAsync(user);
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

            user.Company.StripeCustomerId = customerId;
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

            return user.Company.StripeCustomerId;
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

            return user.Company.StripeCustomerId;
        }

        /// <summary>
        /// Get User using usermanager  For Stripe Work 
        /// </summary>
        public User GetUserByUserId(string userId)
        {
            User user = UserManager.FindById(userId);
            if (user == null)
            {
                throw new SMDException("No such user with provided user Id!");
            }

            return user;
        }


        public User GetUserByEmail(string email)
        {
            User user = UserManager.FindByEmail(email);
           

            return user;
        }


        /// <summary>
        /// Base Data for User Profile 
        /// </summary>
        public UserProfileBaseResponseModel GetBaseDataForUserProfile()
        {
            Company company = companyRepository.GetCompanyById();
            
            return new UserProfileBaseResponseModel
            {
               Countries = countryRepository.GetAllCountries().ToList(),
               //Cities = company != null? cityRepository.GetAllCitiesOfCountry(company.BillingCountryId??0).ToList(): null,
               Industries = industryRepository.GetAll().ToList(),
               Educations = educationRepository.GetAllEducations().ToList(),
               UserRoles = this.RoleManager.Roles.Where(g => g.Id.StartsWith("EndUser")).ToList(), //   manageUserRepository.getUserRoles().ToList()
               GetApprovalCount = companyRepository.GetApprovalCount(),
               GetCouponreviewCount = couponRatingReviewRepository.CouponReviewCount()
            };
        }
        public int generateAndSmsCode(string userId, string phone)
        {
            User user = UserManager.FindById(userId);
            if (user == null)
            {
                throw new SMDException("No such user with provided user Id!");
            }
            Random _rdm = new Random(); 
            int code = _rdm.Next(1000, 9999);
            //if (String.IsNullOrEmpty(user.Phone1))
            //    return 0;
            if (!string.IsNullOrEmpty(phone))
            {
                smsService.SendSMS(phone, "Your verification code for Cash4Ads profile update is " + code.ToString() + ". Please enter this code in Cash4Ads app to update your profile.");
            }
            else
            {
                smsService.SendSMS(user.Phone1, "Your verification code for Cash4Ads profile update is " + code.ToString() + ". Please enter this code in Cash4Ads app to update your profile.");
            }

            
            //var messagingService = new MessagingService("omar.c@me.com", "DBVgYFGNCWwK");
            //if (!string.IsNullOrEmpty(phone))
            //{
            //    messagingService.SendMessage(new SmsMessage(phone, "Your verification code for Cash4Ads profile update is " + code.ToString() + ". Please enter this code in Cash4Ads app to update your profile.", "EX0205631"));
            //} else
            //{
            //    messagingService.SendMessage(new SmsMessage(user.Phone1, "Your verification code for Cash4Ads profile update is " + code.ToString() + ". Please enter this code in Cash4Ads app to update your profile.", "EX0205631"));
            //}
            
            return code;
        }
        public User getUserByAuthenticationToken(string token)
        {
          return  companyRepository.getUserBasedOnAuthenticationToken(token);
        }


        public string GetRoleNameByRoleId(string RoleId)
        {

            return this.RoleManager.Roles.Where(g => g.Id == RoleId).SingleOrDefault().Name; //   manageUserRepository.getUserRoles().ToList()
           
        }


        public int GetUserProfileCompletness(string UserId)
        {
            return aspnetUsersRepository.GetUserProfileCompletness(UserId);
        }
        public GetApprovalCount_Result GetApprovalCount()
        {
            return companyRepository.GetApprovalCount();
        
        }
       

        #endregion

     
       
        #endregion
    }
}
