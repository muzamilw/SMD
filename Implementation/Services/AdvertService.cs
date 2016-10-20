using System.Globalization;
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
        private readonly ICurrencyRepository _currencyRepository;
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
        private readonly ICompanyRepository companyRepository;
        private readonly IAdCampaignResponseRepository _adcampaignResponseRepository;
        private readonly ICouponCategoryRepository _couponCategoryRepository;
        private readonly ICouponCategoryRepository _companyRepository;
        private readonly ICampaignCategoriesRepository _campaignCategoriesRepository;
        private readonly IUserFavouriteCouponRepository _userFavouriteCouponRepository;

        private readonly IAdCampaignClickRateHistoryRepository _adCampaignClickRateHistoryRepository;

        #region Private Funcs
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }
        private string[] SaveImages(AdCampaign campaign)
        {
            string[] savePaths = new string[9];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + campaign.CampaignId);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (!string.IsNullOrEmpty(campaign.VideoBytes))
            {

                string base64 = campaign.VideoBytes.Substring(campaign.VideoBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_CampaignDefaultVideo.mp4";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[8] = savePath;

            }
            if (!string.IsNullOrEmpty(campaign.CampaignImagePath) && !campaign.CampaignImagePath.Contains("guid_CampaignDefaultImage") && !campaign.CampaignImagePath.Contains("http"))
            {
                string base64 = campaign.CampaignImagePath.Substring(campaign.CampaignImagePath.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_CampaignDefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[0] = savePath;
            }
            if (campaign.Type == 3)
            {
                if (!string.IsNullOrEmpty(campaign.CampaignTypeImagePath) && !campaign.CampaignTypeImagePath.Contains("guid_CampaignTypeDefaultImage"))
                {
                    string base64 = campaign.CampaignTypeImagePath.Substring(campaign.CampaignTypeImagePath.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] data = Convert.FromBase64String(base64);

                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    string savePath = directoryPath + "\\guid_CampaignTypeDefaultImage.jpg";
                    File.WriteAllBytes(savePath, data);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[1] = savePath;
                }

            }
            if (!string.IsNullOrEmpty(campaign.VoucherImagePath) && !campaign.VoucherImagePath.Contains("guid_Voucher1DefaultImage") && !campaign.VoucherImagePath.Contains("http://manage.cash4ads.com/"))
            {

                string base64 = campaign.VoucherImagePath.Substring(campaign.VoucherImagePath.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Voucher1DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[2] = savePath;
                campaign.Voucher1ImagePath = savePath;
            }
            if (!string.IsNullOrEmpty(campaign.buyItImageBytes))
            {
                string base64 = campaign.buyItImageBytes.Substring(campaign.buyItImageBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\buyIt1DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[3] = savePath;
            }
            if (!string.IsNullOrEmpty(campaign.couponImage2) && !campaign.couponImage2.Contains("guid_Voucher2DefaultImage") && !campaign.couponImage2.Contains("http://manage.cash4ads.com/"))
            {

                string base64 = campaign.couponImage2.Substring(campaign.couponImage2.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Voucher2DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[4] = savePath;
                campaign.couponImage2 = savePath;
            }
            if (!string.IsNullOrEmpty(campaign.CouponImage3) && !campaign.CouponImage3.Contains("guid_Coupon3DefaultImage") && !campaign.CouponImage3.Contains("http://manage.cash4ads.com/"))
            {

                string base64 = campaign.CouponImage3.Substring(campaign.CouponImage3.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Coupon3DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[5] = savePath;
                campaign.CouponImage3 = savePath;
            }
            if (!string.IsNullOrEmpty(campaign.CouponImage4) && !campaign.CouponImage4.Contains("guid_Voucher4DefaultImage") && !campaign.CouponImage4.Contains("http://manage.cash4ads.com/"))
            {

                string base64 = campaign.CouponImage4.Substring(campaign.CouponImage4.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Voucher4DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[6] = savePath;
                campaign.CouponImage4 = savePath;
            }

            if (!string.IsNullOrEmpty(campaign.LogoImageBytes))
            {
               
                string[] paths = campaign.LogoImageBytes.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                string savePath = directoryPath + "\\guid_CampaignLogoImage.jpg";
                File.Copy(url, savePath, true);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[7] = savePath;
                campaign.LogoImageBytes = savePath;
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
            ISurveyQuestionRepository surveyQuestionRepository,
            IProductRepository productRepository, ITaxRepository taxRepository, IInvoiceRepository invoiceRepository,
            IInvoiceDetailRepository invoiceDetailRepository, IEducationRepository educationRepository,
            IStripeService stripeService, WebApiUserService webApiUserService, ICompanyRepository companyRepository, IAdCampaignResponseRepository adcampaignResponseRepository
            , ICouponCategoryRepository couponCategoryRepository
            , ICampaignCategoriesRepository campaignCategoriesRepository
            , IUserFavouriteCouponRepository userFavouriteCouponRepository, ICurrencyRepository currencyRepository, IAdCampaignClickRateHistoryRepository adCampaignClickRateHistoryRepository)
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
            this.companyRepository = companyRepository;
            this._adcampaignResponseRepository = adcampaignResponseRepository;
            this._couponCategoryRepository = couponCategoryRepository;
            this._campaignCategoriesRepository = campaignCategoriesRepository;

            this._userFavouriteCouponRepository = userFavouriteCouponRepository;
            this._currencyRepository = currencyRepository;
            this._adCampaignClickRateHistoryRepository = adCampaignClickRateHistoryRepository;
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

            var company = companyRepository.GetCompanyById();


            if (loggedInUser != null)
            {
                campaignProduct = productRepository.GetProductByCountryId(code);
                objUC.CountryId = loggedInUser.Company.CountryId;
                //objUC.CityId = loggedInUser.Company.CityId;
                objUC.City = loggedInUser.Company.City != null ? loggedInUser.Company.City : "";
                //objUC.GeoLat = loggedInUser.Company.City != null ? loggedInUser.Company.City.GeoLat : "";
                //objUC.GeoLong = loggedInUser.Company.City != null ? loggedInUser.Company.City.GeoLong : "";


                objUC.EducationId = loggedInUser.EducationId;

                objUC.IndustryId = loggedInUser.IndustryId;
                objUC.LanguageId = loggedInUser.LanguageId;

                objUC.UserProfileImage = loggedInUser.ProfileImage;
                objUC.CountryName = loggedInUser.Company.Country != null ? loggedInUser.Company.Country.CountryName : "";
                objUC.EducationTitle = loggedInUser.Education != null ? loggedInUser.Education.Title : "";
                objUC.IndustryName = loggedInUser.Industry != null ? loggedInUser.Industry.IndustryName : "";
                objUC.LanguageName = loggedInUser.Language != null ? loggedInUser.Language.LanguageName : "";
                objUC.isStripeIntegrated = company == null ? false : (String.IsNullOrEmpty(company.StripeCustomerId) || company.StripeCustomerId == "undefined" ? false : true);
                objUC.IsSpecialAccount = company.IsSpecialAccount;




                var currency = _countryRepository.Find(company.BillingCountryId.Value).Currency;
                objUC.CurrencyCode = currency.CurrencyCode;
                objUC.CurrencySymbol = currency.CurrencySymbol;

                
            }
            if (campaignProduct != null)
            {
                objUC.AgeClausePrice = campaignProduct.AgeClausePrice ?? 0;
                objUC.EducationClausePrice = campaignProduct.EducationClausePrice ?? 0;
                objUC.LocationClausePrice = campaignProduct.LocationClausePrice ?? 0;
                objUC.OtherClausePrice = campaignProduct.OtherClausePrice ?? 0;
                objUC.ProfessionClausePrice = campaignProduct.ProfessionClausePrice ?? 0;
                objUC.GenderClausePrice = campaignProduct.GenderClausePrice ?? 0;
                objUC.BuyItClausePrice = campaignProduct.BuyItClausePrice ?? 0;
                objUC.QuizQuestionClausePrice = campaignProduct.QuizQuestionClausePrice ?? 0;
                objUC.TenDayDeliveryClausePrice = campaignProduct.TenDayDeliveryClausePrice ?? 0;
                objUC.ThreeDayDeliveryClausePrice = campaignProduct.ThreeDayDeliveryClausePrice ?? 0;
                objUC.FiveDayDeliveryClausePrice = campaignProduct.FiveDayDeliveryClausePrice ?? 0;
                objUC.VoucherClausePrice = campaignProduct.VoucherClausePrice ?? 0;
            }

            return new AdCampaignBaseResponse
            {
                Languages = _languageRepository.GetAllLanguages(),
                Education = _educationRepository.GetAll(),
                UserAndCostDetails = objUC,
                Industry = _industryRepository.GetAll(),
                CouponCategory = _couponCategoryRepository.GetAllCoupons(),
                Currencies = _currencyRepository.GetAllCurrencies()
                //DiscountVouchers = _adCampaignRepository.GetCouponsByUserIdWithoutFilter(loggedInUser.Id)
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
            campaignModel.CompanyId = _adCampaignRepository.CompanyId;
            //campaignModel.CompanyId = companyRepository.GetUserCompany(_adCampaignRepository.LoggedInUserIdentity);
            var user = UserManager.Users.Where(g => g.Id == _adCampaignRepository.LoggedInUserIdentity).SingleOrDefault();
            if (user != null)
                campaignModel.CreatedBy = user.FullName;
            campaignModel.StartDateTime = new DateTime(2005, 1, 1);//campaignModel.StartDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            campaignModel.EndDateTime = new DateTime(2040, 1, 1);//campaignModel.EndDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            if (campaignModel.MaxBudget != null)
            {
                campaignModel.MaxBudget = Math.Round(Convert.ToDouble(campaignModel.MaxBudget), 2);
            }
            if (campaignModel.Status == 2)
            {
                campaignModel.SubmissionDateTime = DateTime.Now;
            }
            //toCamdo pilot: harcoding ClickRate = 1 for every campaign

            // not needed now
            //if (campaignModel.ClickRate == 0)
            //{ campaignModel.ClickRate = 0.0; }
            //else
            //campaignModel.ClickRate = 0.20;
            if (campaignModel.Status == 2)
            {
                campaignModel.SubmissionDateTime = DateTime.Now;


              
            }

            _adCampaignRepository.Add(campaignModel);
            _adCampaignRepository.SaveChanges();

            //maintaining click rate history
           
           _adCampaignClickRateHistoryRepository.Add(new AdCampaignClickRateHistory { CampaignID = campaignModel.CampaignId, ClickRate = campaignModel.ClickRate, RateChangeDateTime = DateTime.Now });
                _adCampaignClickRateHistoryRepository.SaveChanges();

            string[] paths = SaveImages(campaignModel);
            if (paths != null && paths.Count() > 0)
            {
                if (!string.IsNullOrEmpty(paths[0]))
                {
                    campaignModel.ImagePath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[0];
                }
                if (!string.IsNullOrEmpty(paths[1]))
                {
                    campaignModel.LandingPageVideoLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[1];
                }
                if (!string.IsNullOrEmpty(paths[8]) && !paths[8].Contains("http:"))
                {
                    campaignModel.VideoLink2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[8];
                }
                if (!string.IsNullOrEmpty(paths[2]))
                {
                    campaignModel.Voucher1ImagePath = paths[2];
                }
                if (!string.IsNullOrEmpty(paths[3]))
                {
                    campaignModel.BuyItImageUrl = paths[3];
                }
                if (!string.IsNullOrEmpty(paths[4]))
                {
                    campaignModel.couponImage2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[4];
                }
                if (!string.IsNullOrEmpty(paths[5]))
                {
                    campaignModel.CouponImage3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[5];
                }
                if (!string.IsNullOrEmpty(paths[6]))
                {
                    campaignModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[6];
                }
                if (!string.IsNullOrEmpty(paths[7]))
                {
                    campaignModel.LogoUrl = paths[7];
                }
                else if (campaignModel.LogoUrl.Contains("Content/Images"))
                {
                    campaignModel.LogoUrl = null;
                }

                _adCampaignRepository.SaveChanges();
            }

            if (campaignModel.CouponCategories != null && campaignModel.CouponCategories.Count() > 0)
            {
                foreach (CouponCategory item in campaignModel.CouponCategories)
                {
                    CampaignCategory oModel = new CampaignCategory();

                    oModel.CampaignId = campaignModel.CampaignId;
                    oModel.CategoryId = item.CategoryId;
                    _campaignCategoriesRepository.Add(oModel);
                }
                _campaignCategoriesRepository.SaveChanges();

            }

            if (campaignModel.Type == 5 && campaignModel.IsSavedCoupon == true)
            {
                UserFavouriteCoupon oFav = new UserFavouriteCoupon();
                oFav.CouponId = campaignModel.CampaignId;
                oFav.UserId = _adCampaignRepository.LoggedInUserIdentity;
                _userFavouriteCouponRepository.Add(oFav);
                _userFavouriteCouponRepository.SaveChanges();
            }
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



        public CampaignSearchResponseModel SearchCampaigns(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new CampaignSearchResponseModel
            {
                Campaign = _adCampaignRepository.SearchCampaigns(request, out rowCount),
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
            // campaignModel.UserId = _adCampaignRepository.LoggedInUserIdentity;
            var user = UserManager.Users.Where(g => g.Id == _adCampaignRepository.LoggedInUserIdentity).SingleOrDefault();
            var campaignDb = _adCampaignRepository.Find(campaignModel.CampaignId);


            if (user != null)
                campaignModel.CreatedBy = user.FullName;
            string[] paths = SaveImages(campaignModel);
            if (paths != null && paths.Count() > 0)
            {
                if (!string.IsNullOrEmpty(paths[0]) && !paths[0].Contains("http:"))
                {
                    if (!paths[0].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        campaignModel.ImagePath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[0];
                }
                if (!string.IsNullOrEmpty(paths[1]) && !paths[1].Contains("http:"))
                {
                    campaignModel.LandingPageVideoLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[1];
                }
                if (!string.IsNullOrEmpty(paths[8]) && !paths[8].Contains("http:"))
                {
                    campaignModel.VideoLink2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[8];
                }
                if (!string.IsNullOrEmpty(paths[3]))
                {
                    campaignModel.BuyItImageUrl = paths[3];
                }
                if (!string.IsNullOrEmpty(paths[4]))
                {
                    if (!paths[4].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        campaignModel.couponImage2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[4];
                }
                if (!string.IsNullOrEmpty(paths[5]))
                {
                    if (!paths[5].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        campaignModel.CouponImage3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[5];
                }
                if (!string.IsNullOrEmpty(paths[6]))
                {
                    if (!paths[6].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        campaignModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[6];
                }
                if (!string.IsNullOrEmpty(paths[7]))
                {
                    campaignModel.LogoUrl = paths[7];
                }
                else if (campaignModel.LogoUrl.Contains("Content/Images"))
                {
                    campaignModel.LogoUrl = null;
                }

                if (campaignModel.LogoUrl.StartsWith("/"))
                {

                    string path = campaignModel.LogoUrl.Substring(1);

                    if (path.StartsWith("/"))
                    {
                        path = path.Substring(1);
                    }
                    campaignModel.LogoUrl = path;
                }


            }


            
          

            if (!string.IsNullOrEmpty(campaignModel.couponImage2) && !campaignModel.couponImage2.Contains("http:"))
            {
                campaignModel.couponImage2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + campaignModel.couponImage2;
            }
            if (!string.IsNullOrEmpty(campaignModel.CouponImage3) && !campaignModel.CouponImage3.Contains("http:"))
            {
                campaignModel.CouponImage3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + campaignModel.CouponImage3;
            }
            if (!string.IsNullOrEmpty(campaignModel.CouponImage4) && !campaignModel.CouponImage4.Contains("http:"))
            {
                campaignModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + campaignModel.CouponImage4;
            }
            //if (!string.IsNullOrEmpty(campaignModel.VideoLink2) && !campaignModel.CouponImage4.Contains("http:"))
            //{
            //    campaignModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + campaignModel.CouponImage4;
            //}

            campaignModel.StartDateTime = new DateTime(2005, 1, 1);//campaignModel.StartDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);
            campaignModel.EndDateTime = new DateTime(2040, 1, 1);//campaignModel.EndDateTime.Value.Subtract(_adCampaignRepository.UserTimezoneOffSet);

          
            //maintaining click rate history
            if (campaignDb.ClickRate != campaignModel.ClickRate)
            {
                _adCampaignClickRateHistoryRepository.Add(new AdCampaignClickRateHistory { CampaignID = campaignModel.CampaignId, ClickRate = campaignModel.ClickRate, RateChangeDateTime = DateTime.Now });
                _adCampaignClickRateHistoryRepository.SaveChanges();
            }



            if (campaignModel.Status == 3)
            {
                campaignModel.Approved = true;
            }
            if (campaignModel.MaxBudget != null)
            {
                campaignModel.MaxBudget = Math.Round(Convert.ToDouble(campaignModel.MaxBudget), 2);
            }
            campaignModel.CompanyId = _adCampaignRepository.CompanyId;

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
            if (campaignModel.AdCampaignTargetCriterias != null && campaignModel.AdCampaignTargetCriterias.Count() > 0)
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


            // remove categories if campaign has and add again
            List<CampaignCategory> listOfCouponCatsToDelete = _campaignCategoriesRepository.GetAll().Where(c => c.CampaignId == campaignModel.CampaignId).ToList();
            _campaignCategoriesRepository.RemoveAll(listOfCouponCatsToDelete);
            // add or update target locations
            if (campaignModel.CouponCategories != null && campaignModel.CouponCategories.Count() > 0)
            {
                foreach (CouponCategory item in campaignModel.CouponCategories)
                {
                    CampaignCategory oModel = new CampaignCategory();

                    oModel.CampaignId = campaignModel.CampaignId;
                    oModel.CategoryId = item.CategoryId;
                    _campaignCategoriesRepository.Add(oModel);


                }
                _campaignCategoriesRepository.SaveChanges();


            }


            UserFavouriteCoupon oFav = _userFavouriteCouponRepository.GetByCouponId(campaignModel.CampaignId);
            if (campaignModel.Type == 5 && campaignModel.IsSavedCoupon == true && oFav == null)
            {
                oFav = new UserFavouriteCoupon();
                oFav.CouponId = campaignModel.CampaignId;
                oFav.UserId = _adCampaignRepository.LoggedInUserIdentity;
                _userFavouriteCouponRepository.Add(oFav);
                _userFavouriteCouponRepository.SaveChanges();
            }
            else if (campaignModel.Type == 5 && campaignModel.IsSavedCoupon == false && oFav != null)
            {
                _userFavouriteCouponRepository.Delete(oFav);
                _userFavouriteCouponRepository.SaveChanges();
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
                AdCampaigns = _adCampaignRepository.SearchAdCampaignsForApproval(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Update Ad CAmpaign | baqer
        /// </summary>
        //public AdCampaign UpdateAdCampaign(AdCampaign source)
        //{
        //    var dbAd = _adCampaignRepository.Find(source.CampaignId);
        //    // Update 
        //    if (dbAd != null)
        //    {
        //        // Approval
        //        if (source.Approved == true)
        //        {
        //            dbAd.Approved = true;
        //            dbAd.ApprovalDateTime = DateTime.Now;
        //            dbAd.ApprovedBy = _adCampaignRepository.LoggedInUserIdentity;
        //            dbAd.Status = (Int32)AdCampaignStatus.Live;
        //            dbAd.StartDateTime = DateTime.Now;
        //            dbAd.EndDateTime = DateTime.Now.AddDays(30);
        //            // Stripe payment + Invoice Generation
        //            // Muzi bhai said we will see it on latter stage 

        //            //todo pilot: unCommenting Stripe payment code on Ads approval
        //             MakeStripePaymentandAddInvoiceForCampaign(dbAd);
        //        }
        //        // Rejection 
        //        else
        //        {
        //            dbAd.Status = (Int32)AdCampaignStatus.ApprovalRejected;
        //            dbAd.Approved = false;
        //            dbAd.RejectedReason = source.RejectedReason;
        //            emailManagerService.SendQuestionRejectionEmail(dbAd.UserId);
        //        }
        //        dbAd.ModifiedDateTime = DateTime.Now;
        //        dbAd.ModifiedBy = _adCampaignRepository.LoggedInUserIdentity;

        //        _adCampaignRepository.SaveChanges();



        //        AdCampaign campaignupdatedrec = _adCampaignRepository.Find(source.CampaignId);

        //        return campaignupdatedrec;
        //    }
        //    return new AdCampaign();
        //}
        public AdCampaign SendApprovalRejectionEmail(AdCampaign source)
        {
            var dbAd = _adCampaignRepository.Find(source.CampaignId);
            // Update 
            if (dbAd != null)
            {
                if (dbAd.Approved == true)
                {
                    emailManagerService.SendCampaignApprovalEmail(dbAd.UserId, dbAd.CampaignName, dbAd.Type);

                }
                else
                {
                    emailManagerService.SendCampaignRejectionEmail(dbAd.UserId, dbAd.CampaignName, dbAd.RejectedReason, dbAd.Type);

                }

            }
            return new AdCampaign();
        }

        /// <summary>
        /// Makes Payment From Stripe & Add Invoice | baqer
        /// </summary>
        private string MakeStripePaymentandAddInvoiceForCampaign(AdCampaign source)
        {
            #region Stripe Payment
            string response = null;
            Boolean isSystemUser = false;
            double amount = 0;
            // User who added Campaign for approval 
            var user = webApiUserService.GetUserByUserId(source.UserId);
            // Get Current Product
            var product = productRepository.GetProductByCountryId("Ad");
            // Tax Applied
            var tax = taxRepository.GetTaxByCountryId(user.Company.CountryId);
            // Total includes tax
            if (product != null)
            {
                amount = product.SetupPrice ?? 0 + tax.TaxValue ?? 0;


                // If It is not System User then make transation 
                //if (user.Roles.Any(role => role.Name.ToLower().Equals("user")))
                //{
                // Make Stripe actual payment 
                response = stripeService.ChargeCustomer((int?)amount, user.Company.StripeCustomerId);
                isSystemUser = false;

            }

            #endregion

            if (response != null && !response.Contains("Failed"))
            {
                if (source.CompanyId!=null)
                {
                  TransactionManager.AddCompaignApprovalTransaction(source.CampaignId, amount, source.CompanyId.Value);
                  String CompanyName =  companyRepository.GetCompanyNameByID(source.CompanyId.Value);
                   
                    #region Add Invoice
                    // Add invoice data
                    var invoice = new Invoice
                    {
                        Country = user.Company.CountryId.ToString(),
                        Total = (double)amount,
                        NetTotal = (double)amount,
                        InvoiceDate = DateTime.Now,
                        InvoiceDueDate = DateTime.Now.AddDays(7),
                        Address1 = user.Company.CountryId.ToString(),
                        CompanyId = user.Company.CompanyId,
                        CompanyName = CompanyName,
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
                        ItemName = product.ProductName,
                        ItemAmount = (double)amount,
                        ItemTax = (double)(tax != null ? tax.TaxValue : 0),
                        ItemDescription = "This is description!",
                        ItemGrossAmount = (double)amount,
                        CampaignId = source.CampaignId,

                    };
                    invoiceDetailRepository.Add(invoiceDetail);
                    invoiceDetailRepository.SaveChanges();

                    #endregion
                }
            }
            return response;
        }



        /// <summary>
        /// Get profile questions 
        /// </summary>
        public AdCampaignBaseResponse GetProfileQuestionData()
        {

            return new AdCampaignBaseResponse
            {
                ProfileQuestions = _profileQuestionRepository.GetAll().Where(g => g.Status != 0 &&( g.CompanyId == _profileQuestionAnswerRepository.CompanyId || g.CompanyId == null))
            };
        }

        public AdCampaignBaseResponse GetALLSurveyQuestionData()
        {
            return new AdCampaignBaseResponse
            {
                SurveyQuestions = _surveyQuestionRepository.GetAll().Where(g => g.Status != 0)
            };
        }

        public AdCampaignBaseResponse GetSurveyQuestionDataByCompanyId()
        {
            return new AdCampaignBaseResponse
            {
                SurveyQuestions = _surveyQuestionRepository.GetAllByCompanyId().Where(g => g.Status != 0)
            };
        }

        public AdCampaignBaseResponse GetSurveyQuestionAnser(long SqID)
        {
            return new AdCampaignBaseResponse
            {
                SurveyQuestions = _surveyQuestionRepository.GetSurveyQuestionAnswer(SqID)
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
        public AdCampaignBaseResponse getQuizCampaigns()
        {
            return new AdCampaignBaseResponse
            {
                AdCampaigns = _adCampaignRepository.GetAll().Where(g => g.UserId == _adCampaignRepository.LoggedInUserIdentity && g.VerifyQuestion != null && g.VerifyQuestion != "")
            };
          
        }
        public AdCampaignBaseResponse getCompanyBranches()
        {
            return new AdCampaignBaseResponse
            {
                listBranches = _adCampaignRepository.GetAllBranches()
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
        public bool CopyCampaign(long campaignId)
        {
            bool result = false;

            return result;
        }


        public long CopyAddCampaigns(long CampaignId)
        {
            long NewCampaignID = 0;
            AdCampaign source = _adCampaignRepository.GetAdCampaignById(CampaignId).FirstOrDefault();

            // create new instance of add campaign
            AdCampaign target = CreateNewCampaign();

            // Clone
            NewCampaignID = CloneCampaign(source, target);

            // Copy image url
            // ImagePath
            //Landing Page video link
            //Voucher1imagepath
            // Buy it imag url




            return NewCampaignID;




        }

        /// <summary>
        /// Creates New Item and assigns new generated code
        /// </summary>
        private AdCampaign CreateNewCampaign()
        {
            AdCampaign campaignTarget = _adCampaignRepository.Create();
            _adCampaignRepository.Add(campaignTarget);

            return campaignTarget;
        }

        /// <summary>
        /// Creates Copy of Campaign
        /// </summary>
        private long CloneCampaign(AdCampaign source, AdCampaign target)
        {
            try
            {
                // Clone campaign
                source.Clone(target);

                // Clone campaign response
                //CloneAdCampaignResponse(source, target);

                // clone camapign target criterias
                CloneAdCampaignTargetCriteria(source, target);


                // Clone campaign target locations
                CloneAdCampaignTargetLocation(source, target);




                companyRepository.SaveChanges();

                string directoryPathToSaveImages = HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + target.CampaignId);
                if (!Directory.Exists(directoryPathToSaveImages))
                {
                    Directory.CreateDirectory(directoryPathToSaveImages);
                }
                if (!string.IsNullOrEmpty(source.BuyItImageUrl))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/" + source.BuyItImageUrl)))
                    {

                        File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/" + source.BuyItImageUrl), System.Web.HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + target.CampaignId + "/buyItDefaultImage" + Path.GetExtension(source.BuyItImageUrl)), true);
                        target.BuyItImageUrl = "SMD_Content/AdCampaign/" + target.CampaignId + "/buyItDefaultImage" + Path.GetExtension(source.BuyItImageUrl);
                    }
                }
                if (!string.IsNullOrEmpty(source.Voucher1ImagePath))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/" + source.Voucher1ImagePath)))
                    {

                        File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/" + source.Voucher1ImagePath), System.Web.HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + target.CampaignId + "/guid_Voucher1DefaultImage" + Path.GetExtension(source.BuyItImageUrl)), true);
                        target.Voucher1ImagePath = "SMD_Content/AdCampaign/" + target.CampaignId + "/guid_Voucher1DefaultImage" + Path.GetExtension(source.BuyItImageUrl);
                    }
                }
                if (!string.IsNullOrEmpty(source.LogoUrl))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/" + source.LogoUrl)))
                    {

                        File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/" + source.LogoUrl), System.Web.HttpContext.Current.Server.MapPath("~/SMD_Content/AdCampaign/" + target.CampaignId + "/guid_CampaignLogoImage" + Path.GetExtension(source.LogoUrl)), true);
                        target.LogoUrl = "SMD_Content/AdCampaign/" + target.CampaignId + "/guid_CampaignLogoImage" + Path.GetExtension(source.LogoUrl);
                    }
                }

                companyRepository.SaveChanges();


                return target.CampaignId;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        ///// <summary>
        ///// Copy campaign response
        ///// </summary>
        //public void CloneAdCampaignResponse(AdCampaign source, AdCampaign target)
        //{

        //    if (source.AdCampaignResponses == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.AdCampaignResponses == null)
        //    {
        //        target.AdCampaignResponses = new List<AdCampaignResponse>();
        //    }

        //    foreach (AdCampaignResponse responses in source.AdCampaignResponses)
        //    {
        //        AdCampaignResponse targetResponse = _adcampaignResponseRepository.Create();
        //        _adcampaignResponseRepository.Add(targetResponse);
        //        targetResponse.CampaignId = target.CampaignId;
        //        target.AdCampaignResponses.Add(targetResponse);
        //        responses.Clone(targetResponse);



        //    }


        //}


        /// <summary>
        /// Copy campaign targets
        /// </summary>
        public void CloneAdCampaignTargetCriteria(AdCampaign source, AdCampaign target)
        {

            if (source.AdCampaignTargetCriterias == null)
            {
                return;
            }

            // Initialize List
            if (target.AdCampaignTargetCriterias == null)
            {
                target.AdCampaignTargetCriterias = new List<AdCampaignTargetCriteria>();
            }

            foreach (AdCampaignTargetCriteria Criterias in source.AdCampaignTargetCriterias)
            {
                AdCampaignTargetCriteria targetCriteria = _adCampaignTargetCriteriaRepository.Create();
                _adCampaignTargetCriteriaRepository.Add(targetCriteria);
                targetCriteria.CampaignId = target.CampaignId;
                target.AdCampaignTargetCriterias.Add(targetCriteria);
                Criterias.Clone(targetCriteria);

            }


        }


        public void CloneAdCampaignTargetLocation(AdCampaign source, AdCampaign target)
        {

            if (source.AdCampaignTargetLocations == null)
            {
                return;
            }

            // Initialize List
            if (target.AdCampaignTargetLocations == null)
            {
                target.AdCampaignTargetLocations = new List<AdCampaignTargetLocation>();
            }

            foreach (AdCampaignTargetLocation location in source.AdCampaignTargetLocations)
            {
                AdCampaignTargetLocation targetLocation = _adCampaignTargetLocationRepository.Create();
                _adCampaignTargetLocationRepository.Add(targetLocation);
                targetLocation.CampaignId = target.CampaignId;
                target.AdCampaignTargetLocations.Add(targetLocation);
                location.Clone(targetLocation);

            }


        }
        public List<GetCoupons_Result> GetCoupons(string UserId)
        {
            return _adCampaignRepository.GetCoupons(UserId);
        }


        /// <summary>
        /// Update Approval campaign
        /// </summary>
        public string UpdateAdApprovalCampaign(AdCampaign source)
        {
            string respMesg = "True";
            var dbAd = _adCampaignRepository.Find(source.CampaignId);
            var userData = webApiUserService.GetUserByUserId(dbAd.UserId);
            var  isFlag = dbAd.IsPaymentCollected;
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
                    dbAd.StartDateTime = DateTime.Now;
                    dbAd.EndDateTime = DateTime.Now.AddDays(30);
                    if (dbAd.IsPaymentCollected != true)
                    {
                        dbAd.IsPaymentCollected = true;
                        dbAd.PaymentDate = DateTime.Now;
                    }

                    // Stripe payment + Invoice Generation
                    // Muzi bhai said we will see it on latter stage 

                    //todo pilot: unCommenting Stripe payment code on Ads approval
                    if (userData.Company.IsSpecialAccount != true)
                    {
                        if (isFlag != true)
                        {
                            respMesg = MakeStripePaymentandAddInvoiceForCampaign(dbAd);
                        }
                    }
                    if (respMesg.Contains("Failed"))
                    {
                        return respMesg;
                    }
                }
                // Rejection 
                else
                {
                    dbAd.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbAd.Approved = false;
                    dbAd.RejectedReason = source.RejectedReason;
                    
                   
                }
                dbAd.ModifiedDateTime = DateTime.Now;
                dbAd.ModifiedBy = _adCampaignRepository.LoggedInUserIdentity;

                _adCampaignRepository.SaveChanges();

                if (source.Approved != true)
                {
                   emailManagerService.SendQuestionRejectionEmail(dbAd.UserId);
                }

            }
            return respMesg;
        }



        //public CouponCodeModel GenerateCouponCodes(int numbers, long CampaignId)
        //{
        //    CouponCodeModel oModel = new CouponCodeModel();
        //    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    List<string> alreadyAddedCodes = _couponCodeRepository.GetCampaignCoupons(CampaignId).Select(c => c.Code).ToList();
        //    List<CouponCode> codesList = new List<CouponCode>();
        //    CouponCode oCode = null;
        //    bool isAddCode = false;
        //    for (int i = 0; i < numbers; i++)
        //    {
        //        string uCode = new string(chars.OrderBy(o => Guid.NewGuid()).Take(6).ToArray());
        //        if (alreadyAddedCodes != null && alreadyAddedCodes.Contains(uCode))
        //        {
        //            isAddCode = false;
        //        }
        //        else
        //        {
        //            isAddCode = true;
        //        }
        //        if (isAddCode)
        //        {
        //            oCode = new CouponCode();
        //            oCode.Code = uCode;
        //            oCode.CampaignId = CampaignId;
        //            oCode.UserId = _adCampaignRepository.LoggedInUserIdentity;
        //            codesList.Add(oCode);
        //            _couponCodeRepository.Add(oCode);

        //        }
        //    }
        //    AdCampaign ocoupon = _adCampaignRepository.Find(CampaignId);

        //    ocoupon.CouponQuantity = alreadyAddedCodes.Count + codesList.Count;
        //    _couponCodeRepository.SaveChanges();
        //    _adCampaignRepository.SaveChanges();
        //    oModel.CouponList = codesList;
        //    oModel.CouponQuantity = alreadyAddedCodes.Count + codesList.Count;
        //    return oModel;
        //}

        //public string UpdateCouponSettings(string VoucherCode, string SecretKey, string UserId)
        //{
        //    return _couponCodeRepository.UpdateCouponSettings(VoucherCode, SecretKey, UserId);
        //}

        public IEnumerable<getCampaignsByStatus_Result> getCampaignsByStatus() {
            return this._campaignCategoriesRepository.getCampaignsByStatus();
        
        }
        public IEnumerable<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            return _campaignCategoriesRepository.GetLiveCampaignCountOverTime(CampaignType, DateFrom, DateTo, Granularity);
        }

        #endregion

        public IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {

            return _adCampaignRepository.getAdsCampaignByCampaignIdAnalytics(compaignId, CampStatus, dateRange, Granularity);
        }

        public IEnumerable<getAdsCampaignByCampaignIdRatioAnalytic_Result> getAdsCampaignByCampaignIdRatioAnalytic(int ID, int dateRange)
        {

            return _adCampaignRepository.getAdsCampaignByCampaignIdRatioAnalytic(ID, dateRange);
        }
        public IEnumerable<getAdsCampaignByCampaignIdtblAnalytic_Result> getAdsCampaignByCampaignIdtblAnalytic(int ID)
        {
            return _adCampaignRepository.getAdsCampaignByCampaignIdtblAnalytic(ID);
        }
        public IEnumerable<getCampaignROItblAnalytic_Result> getCampaignROItblAnalytic(int ID)
        {
            return _adCampaignRepository.getCampaignROItblAnalytic(ID);
        }
    }
}
