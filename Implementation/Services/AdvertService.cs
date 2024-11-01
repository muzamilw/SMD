﻿using Microsoft.AspNet.Identity;
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
using System.Web;
using Stripe;

namespace SMD.Implementation.Services
{
    public class AdvertService : IAdvertService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IAdCampaignRepository _adCampaignRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdCampaignTargetLocationRepository _adCampaignTargetLocationRepository;
        private readonly IAdCampaignTargetCriteriaRepository _adCampaignTargetCriteriaRepository;
        private readonly IEmailManagerService emailManagerService;
        private readonly IProfileQuestionRepository _profileQuestionRepository;
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IIndustryRepository _industryRepository;
        private readonly IProductRepository productRepository;
        private readonly ITaxRepository taxRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IStripeService stripeService;
        private readonly WebApiUserService webApiUserService;

        #region Private Funcs
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }
        private string[] SaveImages(AdCampaign campaign)
        {
            string[] savePaths = new string[2];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + campaign.CampaignId);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (!string.IsNullOrEmpty(campaign.CampaignImagePath) && !campaign.CampaignImagePath.Contains("CampaignDefaultImage"))
            {
                string base64 = campaign.CampaignImagePath.Substring(campaign.CampaignImagePath.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\CampaignDefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[0] = savePath;
            }
            if (campaign.Type == 3)
            {
                if (!string.IsNullOrEmpty(campaign.CampaignTypeImagePath) && !campaign.CampaignTypeImagePath.Contains("CampaignTypeDefaultImage"))
                {
                    string base64 = campaign.CampaignTypeImagePath.Substring(campaign.CampaignTypeImagePath.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] data = Convert.FromBase64String(base64);

                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    string savePath = directoryPath + "\\CampaignTypeDefaultImage.jpg";
                    File.WriteAllBytes(savePath, data);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[1] = savePath;
                }

            }
            return savePaths;
        }
        #endregion

        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public AdvertService(
            IAdCampaignRepository adCampaignRepository,
            ILanguageRepository languageRepository,
            IIndustryRepository industryRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IAdCampaignTargetLocationRepository adCampaignTargetLocationRepository,
             IEmailManagerService emailManagerService,
            IAdCampaignTargetCriteriaRepository adCampaignTargetCriteriaRepository,
            IProfileQuestionRepository profileQuestionRepository,
            IProfileQuestionAnswerRepository profileQuestionAnswerRepository,
            ISurveyQuestionRepository surveyQuestionRepository, IProductRepository productRepository, ITaxRepository taxRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository,IEducationRepository educationRepository, IStripeService stripeService, WebApiUserService webApiUserService)
        {
            this._adCampaignRepository = adCampaignRepository;
            this._languageRepository = languageRepository;
            this._countryRepository = countryRepository;
            this._cityRepository = cityRepository;
            this._adCampaignTargetLocationRepository = adCampaignTargetLocationRepository;
            this._adCampaignTargetCriteriaRepository = adCampaignTargetCriteriaRepository;
            this.emailManagerService = emailManagerService;
            this._profileQuestionRepository = profileQuestionRepository;
            this._profileQuestionAnswerRepository = profileQuestionAnswerRepository;
            this._surveyQuestionRepository = surveyQuestionRepository;
            this.productRepository = productRepository;
            this.taxRepository = taxRepository;
            this.invoiceRepository = invoiceRepository;
            this.invoiceDetailRepository = invoiceDetailRepository;
            this._industryRepository = industryRepository;
            this._educationRepository = educationRepository;
            this.stripeService = stripeService;
            this.webApiUserService = webApiUserService;
        }

        /// <summary>
        /// Get Base Data 
        /// </summary>
        public AdCampaignBaseResponse GetCampaignBaseData()
        {
            string code = Convert.ToString((int)ProductCode.AdApproval);
            User loggedInUser = _adCampaignRepository.GetUserById();
            Product campaignProduct = null;
            UserAndCostDetail objUC = new UserAndCostDetail();
            if (loggedInUser != null)
            {
                campaignProduct = productRepository.GetProductByCountryId(Convert.ToInt32(loggedInUser.CountryId), code);
                objUC.CountryId = loggedInUser.CountryId;
                objUC.CityId = loggedInUser.CityId;
                objUC.CityName = loggedInUser.City != null ? loggedInUser.City.CityName : "";
                objUC.GeoLat = loggedInUser.City != null ? loggedInUser.City.GeoLat : "";
                objUC.GeoLong = loggedInUser.City != null ? loggedInUser.City.GeoLong : "";


                objUC.EducationId = loggedInUser.EducationId;

                objUC.IndustryId = loggedInUser.IndustryId;
                objUC.LanguageId = loggedInUser.LanguageId;


                objUC.CountryName = loggedInUser.Country != null ? loggedInUser.Country.CountryName : "";
                objUC.EducationTitle = loggedInUser.Education != null ? loggedInUser.Education.Title : "";
                objUC.IndustryName = loggedInUser.Industry != null ? loggedInUser.Industry.IndustryName : "";
                objUC.LanguageName = loggedInUser.Language != null ? loggedInUser.Language.LanguageName : "";
                objUC.isStripeIntegrated = String.IsNullOrEmpty(loggedInUser.StripeCustomerId) ? false : true;
            }
            if (campaignProduct != null)
            {
                objUC.AgeClausePrice = campaignProduct.AgeClausePrice;
                objUC.EducationClausePrice = campaignProduct.EducationClausePrice;
                objUC.LocationClausePrice = campaignProduct.LocationClausePrice;
                objUC.OtherClausePrice = campaignProduct.OtherClausePrice;
                objUC.ProfessionClausePrice = campaignProduct.ProfessionClausePrice;
                objUC.GenderClausePrice = campaignProduct.GenderClausePrice;
            }
            return new AdCampaignBaseResponse
            {
                Languages = _languageRepository.GetAllLanguages(),
                Education = _educationRepository.GetAll(),
                UserAndCostDetails = objUC,
                Industry = _industryRepository.GetAll()
            };
        }

        /// <summary>
        /// Get cities and countries search date
        /// </summary>
        public AdCampaignBaseResponse SearchCountriesAndCities(string searchString)
        {
            return new AdCampaignBaseResponse
            {
                countries = _countryRepository.GetSearchedCountries(searchString),
                Cities = _cityRepository.GetSearchCities(searchString)
            };
        }

        /// <summary>
        /// Get langauge search data
        /// </summary>
        public AdCampaignBaseResponse SearchLanguages(string searchString)
        {
            return new AdCampaignBaseResponse
            {
                Languages = _languageRepository.GetSearchedLanguages(searchString)
            };
        }
        /// <summary>
        /// Get Industry search data
        /// </summary>
        public IEnumerable<Industry> SearchIndustry(string searchString)
        {
            return _industryRepository.SearchIndustries(searchString);

        }
        public IEnumerable<Education> SearchEducation(string searchString)
        {
            return _educationRepository.SearchEducation(searchString);
        }

        /// <summary>
        /// Add Campaign
        /// </summary>
        public void CreateCampaign(AdCampaign campaignModel)
        {
            campaignModel.UserId = _adCampaignRepository.LoggedInUserIdentity;
            string[] paths = SaveImages(campaignModel);
            if (paths != null && paths.Count() > 0)
            {
                if (!string.IsNullOrEmpty(paths[0]))
                {
                    campaignModel.ImagePath = paths[0];
                }
                if (!string.IsNullOrEmpty(paths[1]))
                {
                    campaignModel.LandingPageVideoLink = paths[1];
                }


            }
            campaignModel.StartDateTime = campaignModel.StartDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            campaignModel.EndDateTime = campaignModel.EndDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            _adCampaignRepository.Add(campaignModel);
            _adCampaignRepository.SaveChanges();


        }
        public CampaignResponseModel GetCampaigns(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new CampaignResponseModel
            {
                Campaign = _adCampaignRepository.SearchCampaign(request, out rowCount),
                //Languages = _languageRepository.GetAllLanguages(),
                TotalCount = rowCount
                // UserAndCostDetails = _adCampaignRepository.GetUserAndCostDetail()
            };
        }

        public CampaignResponseModel GetCampaignById(long CampaignId)
        {
            var campaignEnumarable = _adCampaignRepository.GetAdCampaignById(CampaignId);
            foreach (var campaign in campaignEnumarable)
            {
                if (campaign.StartDateTime.HasValue)
                    campaign.StartDateTime = campaign.StartDateTime.Value.Add(_adCampaignRepository.UserTimezoneOffSet);
                if (campaign.EndDateTime.HasValue)
                    campaign.EndDateTime = campaign.EndDateTime.Value.Add(_adCampaignRepository.UserTimezoneOffSet);
            }
            return new CampaignResponseModel
            {
                Campaign = campaignEnumarable
            };
        }
        /// <summary>
        /// update Campaign
        /// </summary>
        public void UpdateCampaign(AdCampaign campaignModel)
        {
            campaignModel.UserId = _adCampaignRepository.LoggedInUserIdentity;
            string[] paths = SaveImages(campaignModel);
            if (paths != null && paths.Count() > 0)
            {
                if (!string.IsNullOrEmpty(paths[0]) && !paths[0].Contains("http:"))
                {
                    campaignModel.ImagePath = paths[0];
                }
                if (!string.IsNullOrEmpty(paths[1]) && !paths[1].Contains("http:"))
                {
                    campaignModel.LandingPageVideoLink = paths[1];
                }
            }
            campaignModel.StartDateTime = campaignModel.StartDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            campaignModel.EndDateTime = campaignModel.EndDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);

            _adCampaignRepository.Update(campaignModel);
            _adCampaignRepository.SaveChanges();

            // add or update target locations
            if (campaignModel.AdCampaignTargetLocations != null && campaignModel.AdCampaignTargetLocations.Count() > 0)
            {
                foreach (AdCampaignTargetLocation item in campaignModel.AdCampaignTargetLocations)
                {
                    if (item.Id == 0)
                    {
                        item.CampaignId = campaignModel.CampaignId;
                        _adCampaignTargetLocationRepository.Add(item);
                    }
                    else
                    {
                        _adCampaignTargetLocationRepository.Update(item);
                    }

                }
                _adCampaignTargetLocationRepository.SaveChanges();
                // delete location records if any
                List<long> idsToCompare = campaignModel.AdCampaignTargetLocations.Where(i => i.Id > 0).Select(i => i.Id).ToList();
                List<AdCampaignTargetLocation> listOflocationsToDelete = _adCampaignTargetLocationRepository.GetAll().Where(c => c.CampaignId == campaignModel.CampaignId && !idsToCompare.Contains(c.Id)).ToList();
                _adCampaignTargetLocationRepository.RemoveAll(listOflocationsToDelete);

            }
            else
            {
                List<AdCampaignTargetLocation> listOflocationsToDelete = _adCampaignTargetLocationRepository.GetAll().Where(c => c.CampaignId == campaignModel.CampaignId).ToList();
                _adCampaignTargetLocationRepository.RemoveAll(listOflocationsToDelete);
            }



            // add or update target criterias
            if (campaignModel.AdCampaignTargetLocations != null && campaignModel.AdCampaignTargetLocations.Count() > 0)
            {
                foreach (AdCampaignTargetCriteria citem in campaignModel.AdCampaignTargetCriterias)
                {
                    if (citem.CriteriaId == 0)
                    {
                        citem.CampaignId = campaignModel.CampaignId;
                        _adCampaignTargetCriteriaRepository.Add(citem);
                    }
                    else
                    {
                        _adCampaignTargetCriteriaRepository.Update(citem);
                    }


                }
                _adCampaignTargetCriteriaRepository.SaveChanges();

                List<long> idsToCompare = campaignModel.AdCampaignTargetCriterias.Where(i => i.CriteriaId > 0).Select(i => i.CriteriaId).ToList();
                List<AdCampaignTargetCriteria> listOfCriteriasToDelete = _adCampaignTargetCriteriaRepository.GetAll().Where(c => c.CampaignId == campaignModel.CampaignId && !idsToCompare.Contains(c.CriteriaId)).ToList();
                _adCampaignTargetCriteriaRepository.RemoveAll(listOfCriteriasToDelete);
            }
            else
            {
                List<AdCampaignTargetCriteria> listOfCriteriasToDelete = _adCampaignTargetCriteriaRepository.GetAll().Where(c => c.CampaignId == campaignModel.CampaignId).ToList();
                _adCampaignTargetCriteriaRepository.RemoveAll(listOfCriteriasToDelete);
            }

        }
        #endregion
        #region Public

        /// <summary>
        /// Get Ad Campaigns that are need aprroval | baqer
        /// </summary>
        public AdCampaignResposneModelForAproval GetAdCampaignForAproval(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new AdCampaignResposneModelForAproval
            {
                AdCampaigns = _adCampaignRepository.SearchAdCampaigns(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Update Ad CAmpaign | baqer
        /// </summary>
        public AdCampaign UpdateAdCampaign(AdCampaign source)
        {
            var dbAd = _adCampaignRepository.Find(source.CampaignId);
            // Update 
            if (dbAd != null)
            {
                // Approval
                if (source.Approved == true)
                {
                    dbAd.Approved = true;
                    dbAd.ApprovalDateTime = DateTime.Now;
                    dbAd.ApprovedBy = _adCampaignRepository.LoggedInUserIdentity;
                    dbAd.Status = (Int32)AdCampaignStatus.Live;
                    emailManagerService.SendQuestionApprovalEmail(dbAd.UserId);

                    // Stripe payment + Invoice Generation
                    MakeStripePaymentandAddInvoiceForCampaign(dbAd);
                }
                // Rejection 
                else
                {
                    dbAd.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbAd.Approved = false;
                    dbAd.RejectedReason = source.RejectedReason;
                    emailManagerService.SendQuestionRejectionEmail(dbAd.UserId);
                }
                dbAd.ModifiedDateTime = DateTime.Now;
                dbAd.ModifiedBy = _adCampaignRepository.LoggedInUserIdentity;

                _adCampaignRepository.SaveChanges();
                return _adCampaignRepository.Find(source.CampaignId);
            }
            return new AdCampaign();
        }


        /// <summary>
        /// Makes Payment From Stripe & Add Invoice | baqer
        /// </summary>
        private void MakeStripePaymentandAddInvoiceForCampaign(AdCampaign source)
        {
            #region Stripe Payment

            // User who added Campaign for approval 
            var user = webApiUserService.GetUserByUserId(source.UserId);
            // Get Current Product
            var product = productRepository.GetProductByCountryId(user.CountryId, "Ad");
            // Tax Applied
            var tax = taxRepository.GetTaxByCountryId(user.CountryId);
            // Total includes tax
            var amount = product.SetupPrice + tax.TaxValue;

            // Make Stripe actual payment 
            var response = stripeService.ChargeCustomer((int?)amount, user.StripeCustomerId);

            #endregion
            if (response != "failed")
            {
                #region Add Invoice

                // Add invoice data
                var invoice = new Invoice
                {
                    Country = user.CountryId.ToString(),
                    Total = (double)amount,
                    NetTotal = (double)amount,
                    InvoiceDate = DateTime.Now,
                    InvoiceDueDate = DateTime.Now.AddDays(7),
                    Address1 = user.CountryId.ToString(),
                    UserId = user.Id,
                    CompanyName = "My Company",
                    CreditCardRef = response
                };
                invoiceRepository.Add(invoice);

                #endregion
                #region Add Invoice Detail

                // Add Invoice Detail Data 
                var invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = invoice.InvoiceId,
                    SqId = null,
                    ProductId = product.ProductId,
                    ItemName = "Ad Campaign",
                    ItemAmount = (double)amount,
                    ItemTax = (double)tax.TaxValue,
                    ItemDescription = "This is description!",
                    ItemGrossAmount = (double)amount,
                    CampaignId = source.CampaignId,

                };
                invoiceDetailRepository.Add(invoiceDetail);
                invoiceDetailRepository.SaveChanges();

                #endregion
            }
        }



        /// <summary>
        /// Get profile questions 
        /// </summary>
        public AdCampaignBaseResponse GetProfileQuestionData()
        {
            return new AdCampaignBaseResponse
            {
                ProfileQuestions = _profileQuestionRepository.GetAll()
            };
        }

        /// <summary>
        /// Get profile answers by question id 
        /// </summary>
        public AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId)
        {
            return new AdCampaignBaseResponse
            {
                ProfileQuestionAnswers =
                    _profileQuestionAnswerRepository.GetAllProfileQuestionAnswerByQuestionId(QuestionId)
            };
        }
        /// <summary>
        /// Get survey questions 
        /// </summary>
        public AdCampaignBaseResponse GetSurveyQuestionData(long surveyId)
        {
            return new AdCampaignBaseResponse
            {
                SurveyQuestions = _surveyQuestionRepository.GetAll().Where(g => g.UserId == _surveyQuestionRepository.LoggedInUserIdentity)
            };
        }

        /// <summary>
        /// Get Ads For API  | baqer
        /// </summary>
        public AdCampaignApiSearchRequestResponse GetAdCampaignsForApi(GetAdsApiRequest request)
        {
            var response = _surveyQuestionRepository.GetAdCompaignForApi(request);
            return new AdCampaignApiSearchRequestResponse
            {
                AdCampaigns = response
            };
        }


        /// <summary>
        /// Get Ad Campaign By Id
        /// </summary>
        public AdCampaign GetAdCampaignById(long campaignId)
        {
            return _adCampaignRepository.Find(campaignId);
        }
        #endregion
    }
}
