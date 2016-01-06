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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Stripe;


namespace SMD.Implementation.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
          #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveyQuestionRepository surveyQuestionRepository;
        private readonly ISurveyQuestionTargetCriteriaRepository surveyQuestionTargtCriteriaRepository;
        private readonly ISurveyQuestionTargetLocationRepository surveyQuestionTargetLocationRepository;
        private readonly ICountryRepository countryRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IEmailManagerService emailManagerService;
        private readonly IProductRepository productRepository;
        private readonly ITaxRepository taxRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        private readonly IStripeService stripeService;
        private readonly WebApiUserService webApiUserService;
        private readonly IEducationRepository _educationRepository;
        private readonly IIndustryRepository _industryRepository;
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private string[] SaveSurveyImages(SurveyQuestion question)
        {
            string[] savePaths = new string[2];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/SurveyQuestions/" + question.SqId);
            
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (question.LeftPictureBytes != null)
            {
                string base64 = question.LeftPictureBytes.Substring(question.LeftPictureBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\LeftPicture.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[0] = savePath;
            }
            if (question.RightPictureBytes != null)
            {
                string base64 = question.RightPictureBytes.Substring(question.RightPictureBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\RightPicture.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[1] = savePath;
            }
            return savePaths;
        }

        /// <summary>
        /// Makes Payment From Stripe & Add Invoice | baqer
        /// </summary>
        private async void MakeStripePaymentandAddInvoice(SurveyQuestion source)
        {
            #region Stripe Payment

            // Get Current Product
            var product = productRepository.GetProductByCountryId(source.CountryId, "SQ");
            // Tax Applied
            var tax = taxRepository.GetTaxByCountryId(source.CountryId);
            // Total includes tax
            var amount = product.SetupPrice + tax.TaxValue;
            // User who added Survey Question for approval 
            var user = webApiUserService.GetUserByUserId(source.UserId);
            // Make Stripe actual payment 
            var response = stripeService.ChargeCustomer((int?)amount, user.StripeCustomerId);

            #endregion
            if (response != "failed")
            {
                #region Invocing + Transactions
                var requestModel = new ApproveSurveyRequest
                {
                    UserId = source.UserId,
                    Amount = (double)amount,
                    SurveyQuestionId = source.SqId,
                    StripeResponse = response

                };
                // Transactions + Invocing 
                await webApiUserService.UpdateTransactionOnSurveyApproval(requestModel, false);
                #endregion
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionService(ISurveyQuestionRepository _surveyQuestionRepository, ICountryRepository _countryRepository, ILanguageRepository _languageRepository, IEmailManagerService emailManagerService, ISurveyQuestionTargetCriteriaRepository _surveyQuestionTargtCriteriaRepository, ISurveyQuestionTargetLocationRepository _surveyQuestionTargetLocationRepository, IProductRepository productRepository, ITaxRepository taxRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IStripeService stripeService, WebApiUserService webApiUserService, IEducationRepository educationRepository, IIndustryRepository industryRepository)
        {
            this.surveyQuestionRepository = _surveyQuestionRepository;
            this.languageRepository = _languageRepository;
            this.emailManagerService = emailManagerService;
            this.countryRepository = _countryRepository;
            this.surveyQuestionTargetLocationRepository = _surveyQuestionTargetLocationRepository;
            this.productRepository = productRepository;
            this.taxRepository = taxRepository;
            this.invoiceRepository = invoiceRepository;
            this.invoiceDetailRepository = invoiceDetailRepository;
            this.stripeService = stripeService;
            this.webApiUserService = webApiUserService;
            this.surveyQuestionTargtCriteriaRepository = _surveyQuestionTargtCriteriaRepository;
            this._educationRepository = educationRepository;
            this._industryRepository = industryRepository;
        }

        #endregion
        #region public

        public SurveyQuestionResponseModel GetSurveyQuestions(SurveySearchRequest request)
        {
            int rowCount;
            return new SurveyQuestionResponseModel
            {
                SurveyQuestions = surveyQuestionRepository.SearchSurveyQuestions(request, out rowCount),
                Countries =  new List<Country>(),
                Languages = new List<Language>(),
                TotalCount = rowCount
            };
        }
        public SurveyQuestionResponseModel GetSurveyQuestions()
        {
            int rowCount;
            string code = Convert.ToString((int)ProductCode.SurveyQuestion);
            var product = productRepository.GetAll().Where(g=>g.ProductCode == code).FirstOrDefault();
            var userBaseData = surveyQuestionRepository.getBaseData();
            double? setupPrice = 0;
            if (product != null)
            {
                userBaseData.CurrencySymbol = product.Currency != null ? product.Currency.CurrencyCode : "$";
                setupPrice = product.SetupPrice;
            }
            return new SurveyQuestionResponseModel
            {
                SurveyQuestions = surveyQuestionRepository.SearchSurveyQuestions(null, out rowCount),
                Countries = countryRepository.GetAllCountries(),
                Languages = languageRepository.GetAllLanguages(),
                TotalCount = rowCount,
                setupPrice = setupPrice,
                objBaseData = userBaseData,
                Education = _educationRepository.GetAll(),
                Industry = _industryRepository.GetAll()
            };
        }

        /// <summary>
        /// Get Survey Questions that need aprroval | baqer
        /// </summary>
        public SurveyQuestionResposneModelForAproval GetRejectedSurveyQuestionsForAproval(SurveySearchRequest request)
        {
            int rowCount;
            return new SurveyQuestionResposneModelForAproval
            {
                SurveyQuestions = surveyQuestionRepository.SearchRejectedProfileQuestions(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Edit/Approve Survey Question | baqer
        /// </summary>
        public SurveyQuestion EditSurveyQuestion(SurveyQuestion source)
        {
            var dbServey=surveyQuestionRepository.Find(source.SqId);
            if (dbServey != null)
            {
               // Approved 
                if (source.Approved == true)
                {
                    dbServey.Approved = source.Approved;
                    dbServey.ApprovalDate = source.ApprovalDate;
                    dbServey.ApprovedByUserId = surveyQuestionRepository.LoggedInUserIdentity;
                    dbServey.Status = (Int32)AdCampaignStatus.Live;
                    // Strpe + Invoice Work 
                    MakeStripePaymentandAddInvoice(dbServey);

                  //  emailManagerService.SendQuestionApprovalEmail(dbServey.UserId);

                } // Rejected 
                else
                {
                    dbServey.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbServey.Approved = false;
                    dbServey.RejectionReason = source.RejectionReason;
                 //   emailManagerService.SendQuestionRejectionEmail(dbServey.UserId);
                }
                dbServey.ModifiedDate = DateTime.Now;
                dbServey.ModifiedBy = surveyQuestionRepository.LoggedInUserIdentity;
            }
            surveyQuestionRepository.SaveChanges();
            return surveyQuestionRepository.Find(source.SqId);
        }

        public bool Create(SurveyQuestion survey)
        {
            try
            {
                survey.UserId = surveyQuestionRepository.LoggedInUserIdentity;
                survey.Type = (int)SurveyQuestionType.Advertiser;
                survey.StartDate = survey.StartDate.Value.Subtract(surveyQuestionRepository.UserTimezoneOffSet);
                survey.EndDate = survey.EndDate.Value.Subtract(surveyQuestionRepository.UserTimezoneOffSet);
                survey.SubmissionDate = DateTime.Now;
                surveyQuestionRepository.Add(survey);
                surveyQuestionRepository.SaveChanges();
                string[] paths = SaveSurveyImages(survey);
               // return surveyQuestionRepository.updateSurveyImages(paths, survey.SqId);
                if(survey.LeftPictureBytes != null)
                    survey.LeftPicturePath = paths[0];
                if(survey.RightPictureBytes != null)
                    survey.RightPicturePath = paths[1];
                surveyQuestionRepository.SaveChanges();
                return true;
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(SurveyQuestion survey)
        {
            try
            {
                survey.ModifiedDate = DateTime.Now;
                survey.ModifiedBy = surveyQuestionRepository.LoggedInUserIdentity;
                survey.StartDate = survey.StartDate.Value.Subtract(surveyQuestionRepository.UserTimezoneOffSet);
                survey.EndDate = survey.EndDate.Value.Subtract(surveyQuestionRepository.UserTimezoneOffSet);
                surveyQuestionRepository.Update(survey);
                surveyQuestionRepository.SaveChanges();
                string[] paths = SaveSurveyImages(survey);
                // return surveyQuestionRepository.updateSurveyImages(paths, survey.SqId);
                if (survey.LeftPictureBytes != null)
                    survey.LeftPicturePath = paths[0];
                if (survey.RightPictureBytes != null)
                    survey.RightPicturePath = paths[1];
                // add or update locations
                foreach(var loc in survey.SurveyQuestionTargetLocations)
                {
                    if (loc.Id != 0)
                    {
                        surveyQuestionTargetLocationRepository.Update(loc);
                    }
                    else
                    {
                        loc.SqId = survey.SqId;
                        surveyQuestionTargetLocationRepository.Add(loc);
                    }
                }
                // add or update criteria
                foreach (var criteria in survey.SurveyQuestionTargetCriterias)
                {
                    if (criteria.Id != 0)
                    {
                        if (criteria.Type != (int)SurveyQuestionTargetCriteriaType.Language && criteria.Type != (int)SurveyQuestionTargetCriteriaType.Industry)  // industry and languages are addable and deleteable
                            surveyQuestionTargtCriteriaRepository.Update(criteria);
                    }
                    else
                    {
                        criteria.SqId = survey.SqId;
                        surveyQuestionTargtCriteriaRepository.Add(criteria);
                    }
                }
                // delete criteria 
                foreach (var dbCriteria in surveyQuestionTargtCriteriaRepository.GetAll().Where(g => g.SqId == survey.SqId).ToList())
                {
                    var clientObj = survey.SurveyQuestionTargetCriterias.Where(g => g.Id == dbCriteria.Id).SingleOrDefault();
                    if (clientObj == null)
                        surveyQuestionTargtCriteriaRepository.Delete(dbCriteria);
                }
                // delete locations 
                // delete criteria 
                foreach (var dbLocation in surveyQuestionTargetLocationRepository.GetAll().Where(g => g.SqId == survey.SqId).ToList())
                {
                    var clientObj = survey.SurveyQuestionTargetLocations.Where(g => g.Id == dbLocation.Id).SingleOrDefault();
                    if (clientObj == null)
                        surveyQuestionTargetLocationRepository.Delete(dbLocation);
                }
                surveyQuestionTargetLocationRepository.SaveChanges();
                surveyQuestionTargtCriteriaRepository.SaveChanges();
                surveyQuestionRepository.SaveChanges();
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SurveyQuestionEditResponseModel GetSurveyQuestion(long SqId)
        {
            SurveyQuestion Servey = surveyQuestionRepository.Find(SqId);
            if(Servey.StartDate.HasValue)
                Servey.StartDate = Servey.StartDate.Value.Add(surveyQuestionRepository.UserTimezoneOffSet);
            if(Servey.EndDate.HasValue)
                 Servey.EndDate = Servey.EndDate.Value.Add(surveyQuestionRepository.UserTimezoneOffSet);

            return new SurveyQuestionEditResponseModel
            {
                SurveyQuestionObj = Servey
           
            };
        }

        /// <summary>
        /// Get Surveys For APi | baqer
        /// </summary>
        public SurveyForApiSearchResponse GetSueveysForApi(GetSurveysApiRequest request)
        {
            return new SurveyForApiSearchResponse
            {
                Surveys = surveyQuestionRepository.GetSurveysForApi(request)
            };
        }

        /// <summary>
        /// Returns count of Matches Surveys| baqer
        /// </summary>
        public long GetAudienceSurveyCount(GetAudienceSurveyRequest request)
        {
            #region Comma Separted String
            string list = null;
            if (request.ProfileQuestionIds != null && request.ProfileQuestionIds.Count > 0)
            {
                int i = 0;
                for (; request.ProfileQuestionIds.Count > i + 1; i++)
                {
                    list = list + request.ProfileQuestionIds[i] + ",";
                }
                list = list + request.ProfileQuestionIds[i];
                request.IdsList = list;
            }
            #endregion
            return surveyQuestionRepository.GetAudienceSurveyCount(request);
        }

        /// <summary>
        /// Returns count of Matches AdCampaigns| baqer
        /// </summary>
        public long GetAudienceAdCampaignCount(GetAudienceSurveyRequest request)
        {
            #region Comma Separted String
            string list = null;
            if (request.ProfileQuestionIds != null && request.ProfileQuestionIds.Count > 0)
            {
                int i = 0;
                for (; request.ProfileQuestionIds.Count > i + 1; i++)
                {
                    list = list + request.ProfileQuestionIds[i] + ",";
                }
                list = list + request.ProfileQuestionIds[i];
                request.IdsList = list;
            }
            #endregion
            return surveyQuestionRepository.GetAudienceAdCampaignCount(request);  
        }

        // added by saqib for getting audience count survey add /edit screen
        public GetAudience_Result GetAudienceCount(GetAudienceCountRequest request)
        {
            return surveyQuestionRepository.GetAudienceCount(request);
        }

        

        /// <summary>
        /// Stripe Payment Work
        /// </summary>
        public string CreateChargeWithCustomerId(int? amount, string customerId)
        {
            // Verify If Credit Card is not expired
            var customerService = new StripeCustomerService();
            var customer = customerService.Get(customerId);
            if (customer == null)
            {
                throw new SMDException("Customrt Not Found!");
            }

            // If Card has been expired then skip payment
            if (customer.SourceList != null && customer.SourceList.Data != null && customer.DefaultSourceId != null)
            {
                var defaultStripeCard = customer.SourceList.Data.FirstOrDefault(card => card.Id == customer.DefaultSourceId);
                if (defaultStripeCard != null && (Convert.ToInt32(defaultStripeCard.ExpirationMonth) < DateTime.Now.Month ||
                    Convert.ToInt32(defaultStripeCard.ExpirationYear) < DateTime.Now.Year))
                {
                    throw new SMDException("Card Expired!");
                }
            }

            var stripeChargeCreateOptions = new StripeChargeCreateOptions
            {
                CustomerId = customerId,
                Amount = amount,
                Currency = "usd",
                Capture = true
                // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            };
            var chargeService = new StripeChargeService();
            var resposne = chargeService.Create(stripeChargeCreateOptions);
            if (resposne.Status == "succeeded")
            {
                return resposne.BalanceTransactionId;
            }
            return "failed";
        }

        /// <summary>
        /// Get Survey By Id
        /// </summary>
        public SurveyQuestion GetSurveyQuestionById(long sqid)
        {
            return surveyQuestionRepository.Find(sqid);
        }
     
        #endregion
    }

}
