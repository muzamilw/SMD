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
        /// <param name="isProcessed">Tranation status </param>
        private void PerformTransaction(long? adCampaingId, long? surveyQuestionId, Account account, double? transactionAmount,
            int transactionSequence, TransactionType transactionType, bool isCredit = true, bool isProcessed = false)
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
                account.AccountBalance += transactionAmount;
            }
            else
            {
                transaction.DebitAmount = transactionAmount;
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
        private async Task PerformAdCampaignTransactions(AdViewedRequest request, Account advertisersAccount, double? adClickRate,
            Account adViewersAccount, double? adViewersCut, Company referringCompany, double? smdsCut, Account affiliatesAccount,
            Account smdAccount, AdCampaign adCampaign, bool adViewerUsedVoucher = false)
        {
            // Debit Advertiser
            var transactionSequence = 1;

            // Perform the transactions
            // Debit Campaign Advertiser
            PerformTransaction(request.AdCampaignId, null, advertisersAccount, adClickRate, transactionSequence, TransactionType.AdClick, false);

            // Credit AdViewer
            if (!adViewerUsedVoucher)
            {
                transactionSequence += 1;
                PerformTransaction(request.AdCampaignId, null, adViewersAccount, adViewersCut, transactionSequence, TransactionType.AdClick);

                // Credit Affiliate
                smdsCut = PerformCreditTransactionForAffiliate(request, referringCompany, smdsCut, affiliatesAccount, ref transactionSequence);
            }
            
            // Credit SMD
            transactionSequence += 1;
            PerformTransaction(request.AdCampaignId, null, smdAccount, smdsCut, transactionSequence, TransactionType.AdClick, true, true);

            // Update AdCampaign Amount Spent
            if (!adCampaign.AmountSpent.HasValue)
            {
                adCampaign.AmountSpent = 0;
            }
            adCampaign.AmountSpent += adClickRate;

            // Add Campaign Response 
            PerformSkipOrUpdateUserAdSelection((int)request.AdCampaignId, request.UserId,
                (adViewerUsedVoucher ? (int)AdRewardType.Voucher : (int)AdRewardType.Cash), false, adViewersCut, request.companyId,request.userQuizSelection.HasValue?request.userQuizSelection.Value:0, false);

            // Save Changes
            transactionRepository.SaveChanges();

            // If Ad Viewer Used Voucher then Email Voucher to Him
            if (adViewerUsedVoucher)
            {
                await emailManagerService.SendVoucherEmail(request.UserId, adCampaign.Voucher1Description, adCampaign.Voucher1Value, adCampaign.Voucher1ImagePath);
            }
        }

        /// <summary>
        /// Credit Campaing Affiliate
        /// </summary>
        private double? PerformCreditTransactionForAffiliate(AdViewedRequest request, Company referringCompany, double? smdsCut,
            Account affiliatesAccount, ref int transactionSequence)
        {
            if (referringCompany != null)
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
        private void SetupSurveyApproveTransaction(SurveyQuestion surveyQuestion, out Account advertisersAccount, out Account smdAccount, 
            string systemUserId)
        {
            advertisersAccount = accountRepository.GetByUserId(surveyQuestion.UserId, AccountType.VirtualAccount);
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
        /// Performs Transactions on Survey Approval
        /// </summary>
        private void PerformSurveyApproveTransactions(ApproveSurveyRequest request, Account advertisersAccount, double approvedSurveyAmount,
            double? smdsCut, Account smdAccount, SurveyQuestion surveyQuestion, double amount, int? productId,
            double taxValue, bool isApiCall = true)
        {
            // Debit Advertiser
            var transactionSequence = 1;

            // Perform the transactions
            // Debit Survey Advertiser
            PerformTransaction(null, request.SurveyQuestionId, advertisersAccount, approvedSurveyAmount, transactionSequence, TransactionType.ApproveSurvey, false, true);

            // Credit SMD
            transactionSequence += 1;
            PerformTransaction(null, request.SurveyQuestionId, smdAccount, smdsCut, transactionSequence, TransactionType.ApproveSurvey, true, true);

            // Add Invoice Details
            AddInvoiceDetails(surveyQuestion, amount, request.StripeResponse, productId, taxValue, isApiCall);
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

        /// <summary>
        /// Updates Profile Image
        /// </summary>
        private static void UpdateProfileImage(UpdateUserProfileRequest request, User user)
        {
            string smdContentPath = ConfigurationManager.AppSettings["SMD_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(smdContentPath + "/Users/" + user.CompanyId);

            // Create directory if not there
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            user.ProfileImage = ImageHelper.Save(mapPath, user.ProfileImage, string.Empty, request.ProfileImageName,
                request.ProfileImage, request.ProfileImageBytes);
        }
        
        #region Product Response Actions

        /// <summary>
        /// Update Profile Question User Answer
        /// </summary>
        /// <param name="request"></param>
        private void UpdateProfileQuestionUserAnswer(ProductActionRequest request)
        {
            if (request.Type != null && (request.Type.Value == (int?)ProductType.Question && request.ItemId.HasValue) && 
                (!request.IsSkipped.HasValue || !request.IsSkipped.Value))
            {
                if (request.PqAnswerIds == null)
                {
                    request.PqAnswerIds = new List<int>();
                }
                
                profileQuestionAnswerService.UpdateProfileQuestionUserAnswer(
                    new UpdateProfileQuestionUserAnswerApiRequest
                    {
                        ProfileQuestionAnswerIds = request.PqAnswerIds,
                        ProfileQuestionId = (int)request.ItemId.Value,
                        UserId = request.UserId,
                        companyId = request.companyId.Value
                    });
            }
        }

        /// <summary>
        /// Execute AdClicked / Viewed
        /// </summary>
        private async Task ExecuteAdClickViewed(ProductActionRequest request)
        {
            if (request.AdClickedViewed.HasValue && request.AdClickedViewed.Value && request.ItemId.HasValue && request.AdRewardUserSelection.HasValue &&
                (!request.IsSkipped.HasValue || !request.IsSkipped.Value))
            {
                if (request.AdRewardUserSelection.Value == (int)AdRewardType.Cash)
                {
                    await UpdateTransactionOnViewingAd(new AdViewedRequest
                    {
                        AdCampaignId = request.ItemId.Value,
                        UserId = request.UserId,
                        companyId = request.companyId.Value,
                        userQuizSelection = request.UserQuestionResponse 
                    });    
                }
                else if (request.AdRewardUserSelection.Value == (int)AdRewardType.Voucher)
                {
                    await UpdateTransactionOnViewingAdWithVoucher(new AdViewedRequest
                    {
                        AdCampaignId = request.ItemId.Value,
                        UserId = request.UserId,
                        companyId = request.companyId.Value,
                        userQuizSelection =  request.UserQuestionResponse 
                    }); 
                }
            }
        }

        /// <summary>
        /// Skips Ad Campaign
        /// </summary>
        private void PerformSkipOrUpdateUserAdSelection(int adCampaignId, string userId, int? userSelection, bool? isSkipped,
            double? endUserAmount, int companyId,int userQuizSelection, bool saveChanges = true)
        {
            AdCampaign adCampaign = adCampaignRepository.Find(adCampaignId);
            if (adCampaign == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_AdCampaignNotFound, adCampaignId));
            }

            AdCampaignResponse adCampaignResponse = adCampaignResponseRepository.GetByUserId(adCampaignId, userId) ??
                                                    CreateAdCampaignResponse(adCampaignId, userId, adCampaign);
            adCampaignResponse.CompanyId = companyId;
            adCampaignResponse.UserQuestionResponse = userQuizSelection;
            UpdateAdResponse(userSelection, isSkipped, endUserAmount, adCampaignResponse);
            
            if (!saveChanges)
            {
                return;
            }

            adCampaignRepository.SaveChanges();
        }

        /// <summary>
        /// Creates New Ad Campaign Response
        /// </summary>
        private AdCampaignResponse CreateAdCampaignResponse(int adCampaignId, string userId, AdCampaign adCampaign)
        {
            AdCampaignResponse adCampaignResponse = adCampaignResponseRepository.Create();
            adCampaignResponse.CampaignId = adCampaignId;
            adCampaignResponse.UserId = userId;
            adCampaignResponseRepository.Add(adCampaignResponse);
            adCampaign.AdCampaignResponses.Add(adCampaignResponse);
            return adCampaignResponse;
        }

        /// <summary>
        /// Updates users response for Ads
        /// </summary>
        private void UpdateAdResponse(int? userSelection, bool? isSkipped, double? endUserAmount, AdCampaignResponse adCampaignResponse)
        {
            if (isSkipped.HasValue && isSkipped.Value)
            {
                if (!adCampaignResponse.SkipCount.HasValue)
                {
                    adCampaignResponse.SkipCount = 0;
                }

                adCampaignResponse.SkipCount += 1;
            }
            else if (userSelection.HasValue)
            {
                adCampaignResponse.UserSelection = userSelection;
                if (!adCampaignResponse.EndUserDollarAmount.HasValue)
                {
                    adCampaignResponse.EndUserDollarAmount = 0;
                }
                adCampaignResponse.EndUserDollarAmount += endUserAmount ?? 0;
            }
            adCampaignResponse.CreatedDateTime = DateTime.Now.Add(-(adCampaignRepository.UserTimezoneOffSet));
        }

        /// <summary>
        /// Skips Survey Question
        /// </summary>
        private void PerformSkipOrUserSurveySelection(int sqId, string userId, int? userSelection, bool? isSkipped,int companyId)
        {
            SurveyQuestion surveyQuestion = surveyQuestionRepository.Find(sqId);
            if (surveyQuestion == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.WebApiUserService_SurveyQuestionNotFound, sqId));
            }

            SurveyQuestionResponse sqResponse = surveyQuestionResponseRepository.GetByUserId(sqId, userId) ??
                                                CreateSurveyQuestionResponse(sqId, userId, surveyQuestion);
            // Update User Response
            UpdateProductsResponse(userSelection, isSkipped, sqResponse,companyId);
            surveyQuestionRepository.SaveChanges();
        }

        /// <summary>
        /// Creates New Survey Question Response
        /// </summary>
        private SurveyQuestionResponse CreateSurveyQuestionResponse(int sqId, string userId, SurveyQuestion surveyQuestion)
        {
            SurveyQuestionResponse sqResponse = surveyQuestionResponseRepository.Create();
            sqResponse.SqId = sqId;
            sqResponse.UserId = userId;
            surveyQuestionResponseRepository.Add(sqResponse);
            surveyQuestion.SurveyQuestionResponses.Add(sqResponse);
            return sqResponse;
        }

        /// <summary>
        /// Update Users response for product
        /// </summary>
        private void UpdateProductsResponse(int? userSelection, bool? isSkipped, SurveyQuestionResponse sqResponse,int companyId)
        {
            if (isSkipped.HasValue && isSkipped.Value)
            {
                if (!sqResponse.SkipCount.HasValue)
                {
                    sqResponse.SkipCount = 0;
                }

                sqResponse.SkipCount += 1;
            }
            else if (userSelection.HasValue)
            {
                sqResponse.UserSelection = userSelection;
                sqResponse.SkipCount = 0;
            }
            sqResponse.CompanyId = companyId;
            sqResponse.ResoponseDateTime = DateTime.Now.Add(-(surveyQuestionRepository.UserTimezoneOffSet));
        }


        /// <summary>
        /// Skip Product
        /// </summary>
        /// <param name="request"></param>
        private void SkipProduct(ProductActionRequest request)
        {
            if (request.IsSkipped.HasValue && request.IsSkipped.Value)
            {
                if (!request.ItemId.HasValue || !request.Type.HasValue)
                {
                    throw new SMDException(LanguageResources.WebApiUserService_ProductTypeNotProvided);
                }

                if (request.Type.Value == (int)ProductType.Question)
                {
                    profileQuestionService.PerformSkip((int)request.ItemId.Value);
                }

                else if (request.Type.Value == (int)ProductType.Ad)
                {
                    PerformSkipOrUpdateUserAdSelection((int)request.ItemId.Value, request.UserId, null, true, null, request.companyId.Value,request.UserQuestionResponse.HasValue? request.UserQuestionResponse.Value:0, true);
                }

                else if (request.Type.Value == (int)ProductType.SurveyQuestion)
                {
                    PerformSkipOrUserSurveySelection((int)request.ItemId.Value, request.UserId, null, true,request.companyId.Value);
                }
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
            ISurveyQuestionResponseRepository surveyQuestionResponseRepository, IEducationRepository educationRepository, ICityRepository cityRepository, ICompanyRepository companyRepository)
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

            User cash4Ads = await UserManager.FindByEmailAsync(SystemUsers.Cash4Ads);
            if (cash4Ads == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            // Validates if Ad Campaing Exists and has Advertiser info as well
            var surveyQuestion = await ValidateSurveyQuestion(request);

            // Begin Transaction
            // SMD will get 100 %
            // Get Current Product
            var product = productRepository.GetProductByCountryId(surveyQuestion.CountryId, "SQ");
            // Tax Applied
            var tax = taxRepository.GetTaxByCountryId(surveyQuestion.CountryId);
            var taxValue = tax != null && tax.TaxValue.HasValue ? tax.TaxValue.Value : 0;
            // Total includes tax
            double? approvedSurveyAmount = product.SetupPrice + taxValue;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            SetupSurveyApproveTransaction(surveyQuestion, out advertisersAccount, out smdAccount, cash4Ads.Id);

            // Perform Transactions
            PerformSurveyApproveTransactions(request, advertisersAccount, (double)approvedSurveyAmount, approvedSurveyAmount,
                smdAccount, surveyQuestion, (double)approvedSurveyAmount, product.ProductId, taxValue, isApiCall);

            return new BaseApiResponse
            {
                Status = true,
                Message = "Success"
            };
        }

        #endregion

        #region Ad Approve

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

            User cash4Ads = await UserManager.FindByEmailAsync(SystemUsers.Cash4Ads);
            if (cash4Ads == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, LanguageResources.WebApiUserService_InvalidUser,
                    "Cash4Ads"));
            }

            // Validates if Ad Campaing Exists
            var adCampaign = await ValidateAdCampaign(request);

            // Get Referral if any
            Company referringCompany = null;
            Account affiliatesAccount = null;
            if ((adViewer.Company.ReferringCompanyID.HasValue))
            {
                referringCompany = companyRepository.GetAll().Where(g=>g.CompanyId == adViewer.Company.ReferringCompanyID).SingleOrDefault();
                if (referringCompany == null)
                {
                    throw new SMDException(LanguageResources.WebApiUserService_ReferrerNotFound);
                }

                affiliatesAccount = accountRepository.GetByCompanyId(referringCompany.CompanyId, AccountType.VirtualAccount);
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
            SetupAdCampaignTransaction(request, adCampaign, out adViewersAccount, out advertisersAccount, out smdAccount, cash4Ads.Id);

            // Perform Transactions
            await PerformAdCampaignTransactions(request, advertisersAccount, adClickRate, adViewersAccount, adViewersCut, referringCompany, smdsCut,
                affiliatesAccount, smdAccount, adCampaign);

            return new BaseApiResponse
                   {
                       Status = true,
                       Message = LanguageResources.Success
                   };
        }

        /// <summary>
        /// Update Transactions on Viewing Ad
        /// </summary>
        public async Task<BaseApiResponse> UpdateTransactionOnViewingAdWithVoucher(AdViewedRequest request)
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
            var adCampaign = await ValidateAdCampaign(request);
            
            // Begin Transaction
            // Ad Viewer will get Voucher and 100% will be given to SMD
            double? adClickRate = adCampaign.ClickRate ?? 0;
            double? smdsCut = adClickRate;
            Account adViewersAccount;
            Account advertisersAccount;
            Account smdAccount;

            // Sets up transaction 
            // Gets Accounts required
            SetupAdCampaignTransaction(request, adCampaign, out adViewersAccount, out advertisersAccount, out smdAccount, cash4Ads.Id);

            // Perform Transactions
            await PerformAdCampaignTransactions(request, advertisersAccount, adClickRate, adViewersAccount, 0, null, smdsCut,
                null, smdAccount, adCampaign, true);

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
        public async Task<BaseApiResponse> ExecuteActionOnProductsResponse(ProductActionRequest request)
        {
            if (!request.Type.HasValue)
            {
                throw new SMDException(LanguageResources.WebApiUserService_ProductTypeNotProvided);
            }

            // Check if user exists
            User user = await UserManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Question Answered
            UpdateProfileQuestionUserAnswer(request);

            // Ad Clicked / Viewed
            await ExecuteAdClickViewed(request);

            // Update Survey User Selection
            if (request.Type.Value == (int)ProductType.SurveyQuestion && request.ItemId.HasValue && request.SqUserSelection.HasValue)
            {
                PerformSkipOrUserSurveySelection((int)request.ItemId.Value, request.UserId, request.SqUserSelection, null,request.companyId.Value);
            }

            // Product Skipped
            SkipProduct(request);

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
           

            /// update country and city based on name
            if (!string.IsNullOrEmpty(request.Country))
            {
                request.CountryId = countryRepository.GetCountryId(request.Country);
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                request.CityId = cityRepository.GetCityId(request.City);
            }
            // Update User
            user.Update(request);
            //update company
            companyRepository.updateCompany(request);
            // Save Changes
           await UserManager.UpdateAsync(user);
           if (request.ProfileImageBytes != null)
               await UpdateProfileImage(request);

            return new BaseApiResponse
            {
                Status = true,
                Message = "Success"
            };
        }

        /// <summary>
        /// Update Profile Image
        /// </summary>
        public async Task<UpdateProfileImageResponse> UpdateProfileImage(UpdateUserProfileRequest request)
        {
            User user = await UserManager.FindByIdAsync(request.UserId);
            
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_InvalidUserId);
            }

            // Update Profile Image
            UpdateProfileImage(request, user);
            companyRepository.updateCompanyLogo(user.ProfileImage, user.CompanyId.Value);
            // Save Changes
           // await UserManager.UpdateAsync(user); // because now we will save profile image in company 

            return new UpdateProfileImageResponse
                   {
                       ImageUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + user.ProfileImage,
                       Status = true,
                       Message = "Success"
                   };
        }

        /// <summary>
        /// Get Logged-In User profile 
        /// </summary>
        public User GetLoggedInUser()
        {
            User user =  UserManager.FindById(productRepository.LoggedInUserIdentity);
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
            companyRepository.createCompany(user.Id, request.Email, request.FullName,Guid.NewGuid().ToString());
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
                if (user.Company != null)
                {
                    // update user name  and cuntry name for api 
                    if (user.Company.Country != null)
                        user.CountryName = user.Company.Country.CountryName;
                    if (user.Company.City != null)
                        user.CityName = user.Company.City.CityName;
                }
                else
                {
                    user.AuthenticationToken = Guid.NewGuid().ToString();
                    companyRepository.createCompany(user.Id, request.Email, request.FullName, user.AuthenticationToken);
                    
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
                // update GUID 
                user.AuthenticationToken = Guid.NewGuid().ToString();
                await UserManager.UpdateAsync(user);

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
                LoginProviderKey = request.LoginProviderKey
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


        /// <summary>
        /// Base Data for User Profile 
        /// </summary>
        public UserProfileBaseResponseModel GetBaseDataForUserProfile()
        {
            return new UserProfileBaseResponseModel
            {
               Countries = countryRepository.GetAllCountries().ToList(),
               Industries = industryRepository.GetAll().ToList(),
               Educations = educationRepository.GetAllEducations().ToList()
            };
        }
        public int generateAndSmsCode(string userId)
        {
            User user = UserManager.FindById(userId);
            if (user == null)
            {
                throw new SMDException("No such user with provided user Id!");
            }
            Random _rdm = new Random(); 
            int code = _rdm.Next(1000, 9999);
            if (String.IsNullOrEmpty(user.Phone1))
                return 0;
            var messagingService = new MessagingService("omar.c@me.com", "DBVgYFGNCWwK");
            messagingService.SendMessage(new SmsMessage(user.Phone1, "Your verification code for Cash4Ads profile update is " + code.ToString() + ". Please enter this code in Cash4Ads app to update your profile.", "EX0205631"));
            return code;
        }
        public User getUserByAuthenticationToken(string token)
        {
          return  companyRepository.getUserBasedOnAuthenticationToken(token);
        }
        #endregion

        #endregion
    }
}
