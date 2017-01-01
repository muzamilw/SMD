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
using SMD.Common;

namespace SMD.Implementation.Services
{
    public class CouponService : ICouponService
    {
        #region Private

        private readonly ICouponRepository couponRepository;
        private readonly IUserFavouriteCouponRepository _userFavouriteCouponRepository;
        private readonly IUserPurchasedCouponRepository _userPurchasedCouponRepository;
        private readonly ICompanyService _companyService;
        private readonly IAccountRepository _accountRepository;
        private readonly ICouponCategoriesRepository _couponCategoriesRepository;
        private readonly IWebApiUserService _userService;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserCouponViewRepository _userCouponViewRepository;
        private readonly IEmailManagerService emailManagerService;
        private readonly IStripeService stripeService;
        private readonly WebApiUserService webApiUserService;
        private readonly IProductRepository productRepository;
        private readonly ITaxRepository taxRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        private readonly ICompanyRepository _iCompanyRepository;

        private readonly ICouponPriceOptionRepository couponPriceOptionRepository;
        private readonly ICampaignEventHistoryRepository campaignEventHistoryRepository;
        private readonly IAspnetUsersRepository aspnetUserRepository;

        private readonly ICouponRatingReviewRepository couponRatingReviewRepository;

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }
        private string[] SaveImages(Coupon campaign)
        {
            string[] savePaths = new string[12];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + campaign.CouponId);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!string.IsNullOrEmpty(campaign.CouponImage2) && !campaign.CouponImage2.Contains("guid_Voucher2DefaultImage") && !campaign.CouponImage2.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.CouponImage2.Contains("SMD_Content"))
                {
                    string[] paths = campaign.CouponImage2.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    string savePath = directoryPath + "\\guid_Voucher2DefaultImage.jpg";

                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[4] = savePath;
                    campaign.CouponImage2 = savePath;
                }
            }
            if (!string.IsNullOrEmpty(campaign.CouponImage3) && !campaign.CouponImage3.Contains("guid_Coupon3DefaultImage") && !campaign.CouponImage3.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.CouponImage3.Contains("SMD_Content"))
                {
                    string[] paths = campaign.CouponImage3.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.CouponImage3.Substring(campaign.CouponImage3.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_Coupon3DefaultImage.jpg";
                    // File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[5] = savePath;
                    campaign.CouponImage3 = savePath;
                }
            }

            if (!string.IsNullOrEmpty(campaign.CouponImage4) && !campaign.CouponImage4.Contains("guid_Coupon4DefaultImage") && !campaign.CouponImage4.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.CouponImage4.Contains("SMD_Content"))
                {
                    string[] paths = campaign.CouponImage4.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.CouponImage3.Substring(campaign.CouponImage3.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_Coupon4DefaultImage.jpg";
                    // File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[8] = savePath;
                    campaign.CouponImage4 = savePath;
                }
            }

            if (!string.IsNullOrEmpty(campaign.CouponImage5) && !campaign.CouponImage5.Contains("guid_Coupon5DefaultImage") && !campaign.CouponImage5.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.CouponImage5.Contains("SMD_Content"))
                {
                    string[] paths = campaign.CouponImage5.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.CouponImage3.Substring(campaign.CouponImage3.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_Coupon5DefaultImage.jpg";
                    // File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[9] = savePath;
                    campaign.CouponImage5 = savePath;
                }
            }
            if (!string.IsNullOrEmpty(campaign.CouponImage6) && !campaign.CouponImage6.Contains("guid_Coupon6DefaultImage") && !campaign.CouponImage6.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.CouponImage6.Contains("SMD_Content"))
                {
                    string[] paths = campaign.CouponImage6.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.CouponImage3.Substring(campaign.CouponImage3.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_Coupon6DefaultImage.jpg";
                    // File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[10] = savePath;
                    campaign.CouponImage6 = savePath;
                }
            }


            if (!string.IsNullOrEmpty(campaign.couponImage1) && !campaign.couponImage1.Contains("guid_Voucher4DefaultImage") && !campaign.couponImage1.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.couponImage1.Contains("SMD_Content"))
                {
                    string[] paths = campaign.couponImage1.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.couponImage1.Substring(campaign.couponImage1.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_Voucher4DefaultImage.jpg";
                    //File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[6] = savePath;
                    campaign.couponImage1 = savePath;
                }
            }
            if (!string.IsNullOrEmpty(campaign.LogoUrl) && !campaign.LogoUrl.Contains("guid_CampaignLogoImage") && !campaign.LogoUrl.Contains("http://manage.cash4ads.com/"))
            {
                if (campaign.LogoUrl.Contains("SMD_Content"))
                {
                    string[] paths = campaign.LogoUrl.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    //string base64 = campaign.LogoImageBytes.Substring(campaign.LogoImageBytes.IndexOf(',') + 1);
                    //base64 = base64.Trim('\0');
                    //byte[] data = Convert.FromBase64String(base64);
                    string savePath = directoryPath + "\\guid_CampaignLogoImage.jpg";
                    //File.WriteAllBytes(savePath, data);
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[7] = savePath;
                }
            }
            return savePaths;
        }
        private void DeleteImages(Coupon couponModel)
        {
            Coupon coupon = couponRepository.GetCouponByIdSingle(couponModel.CouponId);
            if (couponModel.couponImage1 == "/images/standardplaceholder.png" && coupon.couponImage1.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Voucher4DefaultImage.jpg";
                File.Delete(delpath);

            }
            if (couponModel.CouponImage2 == "/images/standardplaceholder.png" && coupon.CouponImage2.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Voucher2DefaultImage.jpg";
                File.Delete(delpath);

            }
            if (couponModel.CouponImage3 == "/images/standardplaceholder.png" && coupon.CouponImage3.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Coupon3DefaultImage.jpg";
                File.Delete(delpath);

            }
            if (couponModel.CouponImage4 == "/images/standardplaceholder.png" && coupon.CouponImage4.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Coupon4DefaultImage.jpg";
                File.Delete(delpath);

            }
            if (couponModel.CouponImage5 == "/images/standardplaceholder.png" && coupon.CouponImage5.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Coupon5DefaultImage.jpg";
                File.Delete(delpath);

            }
            if (couponModel.CouponImage6 == "/images/standardplaceholder.png" && coupon.CouponImage6.Contains("SMD_Content"))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + couponModel.CouponId);
                string delpath = directoryPath + "\\guid_Coupon6DefaultImage.jpg";
                File.Delete(delpath);

            }

        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponService(ICouponRepository couponRepository, IUserFavouriteCouponRepository userFavouriteCouponRepository, ICompanyService _companyService,
            IUserPurchasedCouponRepository _userPurchasedCouponRepository, IAccountRepository _accountRepository, ICouponCategoriesRepository _couponCategoriesRepository, ICurrencyRepository _currencyRepository, IWebApiUserService _userService, IUserCouponViewRepository userCouponViewRepository, IEmailManagerService emailManagerService, WebApiUserService webApiUserService, IStripeService stripeService, IProductRepository productRepository
            , ITaxRepository taxRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, ICompanyRepository iCompanyRepository, ICouponPriceOptionRepository couponPriceOptionRepository, ICampaignEventHistoryRepository campaignEventHistoryRepository, IAspnetUsersRepository aspnetUserRepository, ICouponRatingReviewRepository couponRatingReviewRepository)
        {
            this.couponRepository = couponRepository;
            this._userFavouriteCouponRepository = userFavouriteCouponRepository;
            this._companyService = _companyService;
            this._userPurchasedCouponRepository = _userPurchasedCouponRepository;
            this._accountRepository = _accountRepository;
            this._userService = _userService;
            this._currencyRepository = _currencyRepository;
            this._couponCategoriesRepository = _couponCategoriesRepository;
            this._userCouponViewRepository = userCouponViewRepository;
            this.emailManagerService = emailManagerService;
            this.webApiUserService = webApiUserService;
            this.stripeService = stripeService;
            this.productRepository = productRepository;
            this.taxRepository = taxRepository;
            this.invoiceRepository = invoiceRepository;
            this.invoiceDetailRepository = invoiceDetailRepository;
            _iCompanyRepository = iCompanyRepository;
            this.couponPriceOptionRepository = couponPriceOptionRepository;
            this.campaignEventHistoryRepository = campaignEventHistoryRepository;
            this.aspnetUserRepository = aspnetUserRepository;
            this.couponRatingReviewRepository = couponRatingReviewRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public IEnumerable<Coupon> GetAllCoupons()
        {
            return couponRepository.GetAllCoupons().ToList();
        }

        public CampaignResponseModel GetCoupons(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new CampaignResponseModel
            {
                Coupon = couponRepository.SearchCampaign(request, out rowCount),
                //Languages = _languageRepository.GetAllLanguages(),
                TotalCount = rowCount
                // UserAndCostDetails = couponRepository.GetUserAndCostDetail()
            };
        }

        public CampaignResponseModel GetCouponById(long CampaignId)
        {
            var campaignEnumarable = couponRepository.GetCouponById(CampaignId);
            foreach (var campaign in campaignEnumarable)
            {
                //if (campaign.StartDateTime.HasValue)
                //    campaign.StartDateTime = campaign.StartDateTime.Value.Add(couponRepository.UserTimezoneOffSet);
                //if (campaign.EndDateTime.HasValue)
                //    campaign.EndDateTime = campaign.EndDateTime.Value.Add(couponRepository.UserTimezoneOffSet);
            }



            return new CampaignResponseModel
            {
                Coupon = campaignEnumarable
            };
        }




        public IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId)
        {
            var result = _userFavouriteCouponRepository.GetAllFavouriteCouponByUserId(UserId);
            foreach (var item in result)
            {
                var differ = new DateTime(item.CouponActiveYear.Value, item.CouponActiveMonth.Value, DateTime.DaysInMonth(item.CouponActiveYear.Value, item.CouponActiveMonth.Value)) - DateTime.Today;
                item.DaysLeft = Convert.ToInt32(differ.TotalDays);

                if (item.LogoUrl == null)
                {
                    var company = this._companyService.GetCompanyById(item.CompanyId.Value);

                    item.LogoUrl = "http://manage.cash4ads.com/" + company.Logo;

                }
                else
                {
                    item.LogoUrl = "http://manage.cash4ads.com/" + item.LogoUrl;
                }


            }

            return result;



        }



        /// <summary>
        /// Setting of favorite coupons
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CouponId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool SetFavoriteCoupon(string UserId, long CouponId, bool mode)
        {
            //inserting the favorite coupon 
            if (mode == true)
            {
                //checking if its not alrady marked
                if (_userFavouriteCouponRepository.GetByCouponId(CouponId, UserId) == null)
                {
                    var favCoupon = new UserFavouriteCoupon { UserId = UserId, CouponId = CouponId };

                    _userFavouriteCouponRepository.Add(favCoupon);
                    _userFavouriteCouponRepository.SaveChanges();
                }
            }
            else // removing the favorite
            {
                var removeCoupon = _userFavouriteCouponRepository.GetByCouponId(CouponId);
                _userFavouriteCouponRepository.Delete(removeCoupon);
                _userFavouriteCouponRepository.SaveChanges();

            }


            return true;
        }

        public SearchCouponsResponse SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId)
        {
            List<SearchCoupons_Result> coupons = couponRepository.SearchCoupons(categoryId, type, size, keywords, pageNo, distance, Lat, Lon, UserId).ToList();



            return new SearchCouponsResponse
            {
                Status = true,
                Message = LanguageResources.Success,
                Coupons = coupons,
                TotalCount = coupons.Any() && coupons[0].TotalItems.HasValue ? coupons[0].TotalItems.Value : 0
            };
        }



        public void CreateCampaign(Coupon couponModel)
        {
            couponModel.UserId = couponRepository.LoggedInUserIdentity;
            couponModel.CompanyId = couponRepository.CompanyId;
            //couponModel.CompanyId = companyRepository.GetUserCompany(couponRepository.LoggedInUserIdentity);
            var user = UserManager.Users.Where(g => g.Id == couponRepository.LoggedInUserIdentity).SingleOrDefault();
            if (user != null)
                couponModel.CreatedBy = user.FullName;
            if (couponModel.Status == 2)
            {
                couponModel.SubmissionDateTime = DateTime.Now;
            }
            couponRepository.Add(couponModel);
            couponRepository.SaveChanges();

            string[] paths = SaveImages(couponModel);
            if (paths != null && paths.Count() > 0)
            {

                if (!string.IsNullOrEmpty(paths[4]))
                {
                    couponModel.CouponImage2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[4];
                }
                if (!string.IsNullOrEmpty(paths[5]))
                {
                    couponModel.CouponImage3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[5];
                }
                if (!string.IsNullOrEmpty(paths[6]))
                {
                    couponModel.couponImage1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[6];
                }


                if (!string.IsNullOrEmpty(paths[8]))
                {
                    couponModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[8];
                }
                if (!string.IsNullOrEmpty(paths[9]))
                {
                    couponModel.CouponImage5 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[9];
                }
                if (!string.IsNullOrEmpty(paths[10]))
                {
                    couponModel.CouponImage6 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[10];
                }



                if (!string.IsNullOrEmpty(paths[7]))
                {
                    couponModel.LogoUrl = paths[7];
                }
                else if (couponModel != null && couponModel.LogoUrl != null && couponModel.LogoUrl.Contains("Content/Images"))
                {
                    couponModel.LogoUrl = null;
                }

                couponRepository.SaveChanges();
            }


            //event history
            campaignEventHistoryRepository.InsertCouponEvent((AdCampaignStatus)couponModel.Status, couponModel.CouponId);



            //savint the categories if any. do we need it ?

            ////if (couponModel.CouponCategories != null && couponModel.CouponCategories.Count() > 0)
            ////{
            ////    foreach (var item in couponModel.CouponCategories)
            ////    {
            ////        CouponCategories oModel = new CouponCategories();

            ////        oModel.CouponId = couponModel.CouponId;
            ////        oModel.CategoryId = item.CategoryId;
            ////       _couponCategoriesRepository.Add(oModel);

            ////    }
            ////    _couponCategoriesRepository.SaveChanges();

            ////}

        }

        public void UpdateCampaign(Coupon couponModel)
        {
            couponModel.CompanyId = couponRepository.CompanyId;
            couponModel.UserId = couponRepository.LoggedInUserIdentity;
            var user = UserManager.Users.Where(g => g.Id == couponRepository.LoggedInUserIdentity).SingleOrDefault();
            if (user != null)
                couponModel.CreatedBy = user.FullName;

            if (couponModel.LogoUrl != null)
            {
                if (couponModel.LogoUrl[0] == '/')
                {
                    couponModel.LogoUrl = couponModel.LogoUrl.Substring(1, couponModel.LogoUrl.Length - 1);
                }
            }
            string[] paths = SaveImages(couponModel);
            DeleteImages(couponModel);

            if (paths != null && paths.Count() > 0)
            {

                if (!string.IsNullOrEmpty(paths[4]))
                {
                    if (!paths[4].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.CouponImage2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[4];
                }
                if (!string.IsNullOrEmpty(paths[5]))
                {
                    if (!paths[5].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.CouponImage3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[5];
                }
                if (!string.IsNullOrEmpty(paths[6]))
                {
                    if (!paths[6].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.couponImage1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[6];
                }




                if (!string.IsNullOrEmpty(paths[8]))
                {
                    if (!paths[8].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.CouponImage4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[8];
                }

                if (!string.IsNullOrEmpty(paths[9]))
                {
                    if (!paths[9].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.CouponImage5 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[9];
                }

                if (!string.IsNullOrEmpty(paths[10]))
                {
                    if (!paths[10].ToLower().Contains(HttpContext.Current.Request.Url.Authority.ToLower()))
                        couponModel.CouponImage6 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + paths[10];
                }





                if (!string.IsNullOrEmpty(paths[7]))
                {
                    couponModel.LogoUrl = paths[7];
                }
                else if (couponModel.LogoUrl != null && couponModel.LogoUrl.Contains("Content/Images"))
                {
                    couponModel.LogoUrl = null;
                }
            }

            if (couponModel.Status == 3)
            {
                couponModel.Approved = true;
            }

            foreach (var couponCategory in _couponCategoriesRepository.GetAll().Where(g => g.CouponId == couponModel.CouponId).ToList())
            {
                _couponCategoriesRepository.Delete(couponCategory);
            }

            if (couponModel.Status == 7)
            {
                couponModel.CouponEndDate = DateTime.Now;

            }
            if (couponModel.Status == 2)
            {
                couponModel.SubmissionDateTime = DateTime.Now;
            }

            if (couponModel.CouponCategories != null && couponModel.CouponCategories.Count() > 0)
            {
                foreach (var item in couponModel.CouponCategories)
                {
                    CouponCategories oModel = new CouponCategories();

                    oModel.CouponId = couponModel.CouponId;
                    oModel.CategoryId = item.CategoryId;
                    _couponCategoriesRepository.Add(oModel);


                }
                _couponCategoriesRepository.SaveChanges();

            }



            couponRepository.Update(couponModel);
            couponRepository.SaveChanges();

            //event history
            campaignEventHistoryRepository.InsertCouponEvent((AdCampaignStatus)couponModel.Status, couponModel.CouponId);




            //price option logic


            var couponDbVersion = couponRepository.GetCouponByIdSingle(couponModel.CouponId);
            #region Sub Stock Categories Items
            //Add  SubStockCategories 
            if (couponModel.CouponPriceOptions != null)
            {
                foreach (var item in couponModel.CouponPriceOptions)
                {
                    if (couponDbVersion.CouponPriceOptions.All(x => x.CouponPriceOptionId != item.CouponPriceOptionId) || item.CouponPriceOptionId == 0)
                    {
                        item.CouponId = couponModel.CouponId;

                        couponDbVersion.CouponPriceOptions.Add(item);
                    }

                }
            }
            //find missing items

            List<CouponPriceOption> missingCouponPriceOptions = new List<CouponPriceOption>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (CouponPriceOption dbversionCouponPriceOptions in couponDbVersion.CouponPriceOptions)
            {

                if (couponModel.CouponPriceOptions != null && couponModel.CouponPriceOptions.All(x => x.CouponPriceOptionId != dbversionCouponPriceOptions.CouponPriceOptionId && x.CouponId == couponModel.CouponId))
                {
                    missingCouponPriceOptions.Add(dbversionCouponPriceOptions);
                }


            }

            //remove missing items
            foreach (CouponPriceOption missingCouponPriceOption in missingCouponPriceOptions)
            {

                CouponPriceOption dbVersionMissingItem = couponDbVersion.CouponPriceOptions.First(x => x.CouponPriceOptionId == missingCouponPriceOption.CouponPriceOptionId);
                if (dbVersionMissingItem.CouponPriceOptionId > 0)
                {


                    couponDbVersion.CouponPriceOptions.Remove(dbVersionMissingItem);
                    couponPriceOptionRepository.Delete(dbVersionMissingItem);
                }
            }
            if (couponModel.CouponPriceOptions != null)
            {
                //updating stock sub categories
                foreach (var subCategoryItem in couponModel.CouponPriceOptions)
                {
                    couponPriceOptionRepository.Update(subCategoryItem);
                }
            }

            #endregion


            couponRepository.Update(couponModel);
            couponRepository.SaveChanges();



        }


        //called from mobile apps
        public GetCouponByID_Result GetCouponByIdDefault(long CouponId, string UserId, string Lat, string Lon, out CouponRatingReviewOverallResponse rating)
        {

            var result = couponRepository.GetCouponByIdSP(CouponId, UserId, Lat, Lon);

            int RatingCount = 0;

            rating = couponRatingReviewRepository.GetPublishedCouponRatingReview(CouponId, out RatingCount);

            result.RatingCount = RatingCount;

            return result;

            //if (coupon.LogoUrl == null)
            //{
            //    var company = this._companyService.GetCompanyById(coupon.CompanyId.Value);

            //    coupon.LogoUrl = "http://manage.cash4ads.com/" + company.Logo;

            //}
            //else
            //{
            //    coupon.LogoUrl = "http://manage.cash4ads.com/" + coupon.LogoUrl;
            //}

            //coupon.DaysLeft = Convert.ToInt32( (new DateTime(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value, DateTime.DaysInMonth(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value)) - DateTime.Today).TotalDays);


            //get the currency and its exchange rate.
            //var currency = _currencyRepository.Find(coupon.CurrencyId.Value);

            //double swapcost = (coupon.Savings.Value / currency.SMDCreditRatio.Value) / 100;

            //coupon.SwapCost = swapcost > 0.50 ? swapcost * 100 : 0.50 * 100;

            //checking if its already flagged by user or not.





        }

        public bool CheckCouponFlaggedByUser(long CouponId, string UserId)
        {

            return _userFavouriteCouponRepository.CheckCouponFlaggedByUser(CouponId, UserId);
        }



        public List<PurchasedCoupons> GetPurchasedCouponByUserId(string UserId)
        {
            var result = _userPurchasedCouponRepository.GetPurchasedCouponByUserId(UserId);

            foreach (var coupon in result)
            {
                if (coupon.LogoUrl == null)
                {
                    var company = this._companyService.GetCompanyById(coupon.CompanyId);

                    coupon.LogoUrl = "http://manage.cash4ads.com/" + company.Logo;

                }
                else
                {
                    coupon.LogoUrl = "http://manage.cash4ads.com/" + coupon.LogoUrl;
                }

            }



            return result.ToList();

        }



        public bool PurchaseCoupon(string UserId, long CouponId, double PurchaseAmount)
        {

            List<Account> accounts = _accountRepository.GetByUserId(UserId);

            //var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
            //if (PurchaseAmount < userVirtualAccount.AccountBalance)
            //{

            //Update Accounts
            //TransactionManager.CouponPurchaseTransaction(CouponId, PurchaseAmount, userVirtualAccount.CompanyId.Value);


            //enter the entry for purchased coupon,
            var newPurchasedCoupon = new UserPurchasedCoupon { CouponId = CouponId, IsRedeemed = false, PurchaseAmount = PurchaseAmount, PurchaseDateTime = DateTime.Now, RedemptionOperator = null, UserId = UserId };

            _userPurchasedCouponRepository.Add(newPurchasedCoupon);
            _userPurchasedCouponRepository.SaveChanges();


            //incrementing the Issues/Purchased count 

            var oCoupon = couponRepository.GetCouponByIdSingle(CouponId);
            oCoupon.CouponIssuedCount = (oCoupon.CouponIssuedCount == null ? 0 : oCoupon.CouponIssuedCount) + 1;

            couponRepository.Update(oCoupon);
            couponRepository.SaveChanges();


            //// Email To User coupon code 
            //try
            //{
            //    //BackgroundEmailManagerService.SendVoucherCodeEmail(dbContext, company.CompanyId, codeCode);
            //}
            //catch (Exception ex)
            //{



            //    return true;

            //}

            return true;

            //}
            //return false;

        }
        public int RedeemPurchasedCoupon(string UserId, long couponPurchaseId, string pinCode, string operatorId)
        {

            var user = _userService.GetUserByUserId(UserId);

            if (user == null)
                throw new Exception("User is null");

            var purchasedCoupon = _userPurchasedCouponRepository.Find(couponPurchaseId);

            if (purchasedCoupon == null)
                throw new Exception("purchased coupon is null");


            if (user.CompanyId.HasValue == false)
                throw new Exception("user companyid  is null");

            var company = _companyService.GetCompanyById(user.CompanyId.Value);

            if (company == null)
                throw new Exception("COMPANY is null");


            if (purchasedCoupon != null && purchasedCoupon.UserId == UserId && company.VoucherSecretKey == pinCode && (purchasedCoupon.IsRedeemed == null || purchasedCoupon.IsRedeemed == false))
            {
                purchasedCoupon.IsRedeemed = true;
                purchasedCoupon.RedemptionDateTime = DateTime.Now;
                purchasedCoupon.RedemptionOperator = operatorId;

                _userPurchasedCouponRepository.Update(purchasedCoupon);

                _userPurchasedCouponRepository.SaveChanges();

                //incrementing the redeemed count 

                var oCoupon = couponRepository.GetCouponByIdSingle(purchasedCoupon.CouponId.Value);

                if (oCoupon == null)
                    throw new Exception("Coupon is null");

                oCoupon.CouponRedeemedCount = (oCoupon.CouponRedeemedCount == null ? 0 : oCoupon.CouponRedeemedCount) + 1;

                couponRepository.Update(oCoupon);
                couponRepository.SaveChanges();

                return 1;


            }
            else if (company.VoucherSecretKey != pinCode)
            {
                return 3;// incorrect pincode
            }
            else if (purchasedCoupon.IsRedeemed == true)
            {
                return 4;// already redeemed
            }

            else
            {
                return 2;// incorrect settings
            }
        }


        public List<Coupon> GetCouponsByCompanyId(int CompanyId)
        {

            var company = this._companyService.GetCompanyById(CompanyId);

            var res = couponRepository.GetCouponsByCompanyId(CompanyId);
            foreach (var coupon in res)
            {
                coupon.DaysLeft = Convert.ToInt32((new DateTime(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value, DateTime.DaysInMonth(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value)) - DateTime.Today).TotalDays);


                if (coupon.LogoUrl == null)
                {
                    coupon.LogoUrl = "http://manage.cash4ads.com/" + company.Logo;
                }
                else
                {
                    coupon.LogoUrl = "http://manage.cash4ads.com/" + coupon.LogoUrl;
                }

            }
            return res;
        }


        public List<CouponPriceOption> GetCouponPriceOptions(long CouponId)
        {
            return couponPriceOptionRepository.GetCouponPriceOptions(CouponId);
        }



        public CouponsResponseModelForApproval GetAdCampaignForAproval(GetPagedListRequest request)
        {
            int rowCount;
            return new CouponsResponseModelForApproval
            {
                Coupons = couponRepository.GetCouponsForApproval(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }

        public string UpdateCouponForApproval(Coupon source)
        {
            string respMesg = "True";
            var dbCo = couponRepository.GetCouponByIdSingle(source.CouponId);
            var userData = webApiUserService.GetUserByUserId(dbCo.UserId);
            var isFlag = dbCo.IsPaymentCollected;
            dbCo.IsMarketingStories = source.IsMarketingStories;
            // Update 
            if (dbCo != null)
            {
                // Approval
                if (source.Approved == true)
                {


                    dbCo.Approved = true;
                    dbCo.ApprovalDateTime = DateTime.Now;
                    dbCo.ApprovedBy = couponRepository.LoggedInUserIdentity;
                    dbCo.Status = (Int32)AdCampaignStatus.Live;
                    if (dbCo.IsPaymentCollected != true && userData.Company.StripeCustomerId !=null)
                    {
                        dbCo.IsPaymentCollected = true;
                        dbCo.PaymentDate = DateTime.Now;
                    }
                    // Stripe payment + Invoice Generation
                    // Muzi bhai said we will see it on latter stage 
                    //todo pilot: unCommenting Stripe payment code on Ads approval
                    if (userData.Company.IsSpecialAccount != true)
                    {
                        if (isFlag != true)
                        {
                            respMesg = CreateStripeSubscription(dbCo, userData.Company.StripeCustomerId);
                            ////if (respMesg.Contains("Failed"))
                            ////{
                            ////    return respMesg;
                            ////}
                            ////else
                            ////{
                            ////    //TransactionManager.CouponApprovalTransaction(dbCo.CouponId, PaymentToBeCharged, dbCo.CompanyId.Value);
                            ////}
                        }
                    }
                }
                // Rejection 
                else
                {
                    dbCo.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbCo.Approved = false;
                    dbCo.IsMarketingStories = false;
                    dbCo.RejectedReason = source.RejectedReason.ToString();

                }
                dbCo.ModifiedDateTime = DateTime.Now;
                dbCo.ModifiedBy = couponRepository.LoggedInUserIdentity;

                couponRepository.SaveChanges();

                //event history
                campaignEventHistoryRepository.InsertCouponEvent((AdCampaignStatus)dbCo.Status, dbCo.CouponId);

                if (source.Approved == true)
                {
                    emailManagerService.SendCouponCampaignApprovalEmail(dbCo.UserId, dbCo.CouponTitle,(dbCo.CouponListingMode == 1 ? 7: 30), dbCo.couponImage1);
                }else
                {
                    emailManagerService.SendCouponCampaignRejectionEmail(dbCo.UserId, dbCo.CouponTitle, (dbCo.CouponListingMode == 1 ? 7 : 30), dbCo.couponImage1, dbCo.RejectedReason );
                }

            }
            return respMesg;
        }
        public string UpdateDealMarketing(Coupon source)
        {
            string respMesg = "True";
            var dbCo = couponRepository.GetCouponByIdSingle(source.CouponId);
            if (dbCo != null)
            {
                if (source.IsMarketingStories == true)
                {
                    dbCo.IsMarketingStories = false;
                    couponRepository.SaveChanges();
                }
            }
            else
            {
                respMesg = "false";
            }
            return respMesg;
        }
        private string CreateStripeSubscription(Coupon source ,string stripeCustomerID)
        {

            if (source.CompanyId != null)
            {

                string response = null;
                Boolean isSystemUser = false;
                double amount = 0;
                // User who added Campaign for approval 
                //var user = webApiUserService.GetUserByUserId(source.UserId);

                var company = _companyService.GetCompanyById(source.CompanyId.Value);

                // Get Current Product
                var product = (dynamic)null;

                if (source.CouponListingMode == 1 && stripeCustomerID == null)
                    product = productRepository.GetProductByCountryId("couponfree");
                else 
                {
                    //product = productRepository.GetProductByCountryId("couponunlimited");

                    if (company.StripeSubscriptionId == null)
                    {
                        var resp = stripeService.CreateCustomerSubscription(company.StripeCustomerId);

                        company.StripeSubscriptionId = resp.SubscriptionId;
                        company.StripeSubscriptionStatus = resp.Status;

                        _companyService.UpdateCompany(company);

                    }
                    else
                    {
                        var resp = stripeService.GetCustomerSubscription(company.StripeSubscriptionId, company.StripeCustomerId);
                        if (resp != null && resp.Status == "active")
                        {
                            //all good .. subscription is active.

                        }
                        else   //create a new subscription
                        {
                            resp = stripeService.CreateCustomerSubscription(company.StripeCustomerId);

                            company.StripeSubscriptionId = resp.SubscriptionId;
                            company.StripeSubscriptionStatus = resp.Status;

                            _companyService.UpdateCompany(company);

                        }


                    }
                }


                ////// Make Stripe actual payment 
                ////response = stripeService.ChargeCustomer((int?)amount, user.Company.StripeCustomerId);
                isSystemUser = false;


                //if (response != null && !response.Contains("Failed"))
                //{

                //    String CompanyName = _iCompanyRepository.GetCompanyNameByID(source.CompanyId.Value);
                //    #region Add Invoice

                //    // Add invoice data
                //    var invoice = new Invoice
                //    {
                //        Country = user.Company.CountryId.ToString(),
                //        Total = (double)amount,
                //        NetTotal = (double)amount,
                //        InvoiceDate = DateTime.Now,
                //        InvoiceDueDate = DateTime.Now.AddDays(7),
                //        Address1 = user.Company.CountryId.ToString(),
                //        CompanyId = user.Company.CompanyId,
                //        CompanyName = CompanyName,
                //        CreditCardRef = response
                //    };
                //    invoiceRepository.Add(invoice);

                //    #endregion
                //    #region Add Invoice Detail

                //    // Add Invoice Detail Data 
                //    var invoiceDetail = new InvoiceDetail
                //    {
                //        InvoiceId = invoice.InvoiceId,
                //        SqId = null,
                //        PQID = null,
                //        ProductId = product.ProductId,
                //        ItemName = product.ProductName,
                //        ItemAmount = (double)amount,
                //        ItemTax = (double)(tax != null ? tax.TaxValue : 0),
                //        ItemDescription = "This is description!",
                //        ItemGrossAmount = (double)amount,
                //        CouponID = source.CouponId,

                //    };
                //    invoiceDetailRepository.Add(invoiceDetail);
                //    invoiceDetailRepository.SaveChanges();

                //    #endregion

                //}
            }
            return "";
        }

        public Currency GetCurrenyById(int id)
        {
            return _currencyRepository.Find(id);
        }
        public IEnumerable<getDealByCouponID_Result> getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity)
        {
            return couponRepository.getDealByCouponIDAnalytics(CouponID, dateRange, Granularity);
        }
        public IEnumerable<getDealByCouponIdRatioAnalytic_Result> getDealByCouponIdRatioAnalytic(int ID, int dateRange)
        {

            return couponRepository.getDealByCouponIdRatioAnalytic(ID, dateRange);
        }
        public int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type)
        {
            return couponRepository.getDealStatByCouponIdFormAnalytic(dealId, Gender, age, type); 
        }
        public DateTime getExpiryDate(int CouponId)
        {

            return couponRepository.getExpiryDate(CouponId);
        }


        public bool PauseAllCoupons(int CompanyId)
        {
            return couponRepository.PauseAllCoupons(CompanyId);
        }

        public bool CompleteAllCoupons(int CompanyId)
        {
            return couponRepository.CompleteAllCoupons(CompanyId);
        }

        public int GetFreeCouponCount()
        {
            return couponRepository.GetFreeCouponCount();
        }
        public CouponsResponseModelForApproval GetMarketingDeals(GetPagedListRequest request)
        {
            int rowCount;
            return new CouponsResponseModelForApproval
            {
                Coupons = couponRepository.GetMarketingDeals(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }




        public bool InsertCouponRatingReview(CouponRatingReview model,string Image1String,  string Image2String,  string Image3String, string Image1ext,  string Image2ext,  string Image3ext)
        {
            
            var ReviwerUser = UserManager.FindById(model.UserId);
            var coupon = couponRepository.GetCouponById(model.CouponId.Value).FirstOrDefault();
            var AdvertiserUser = aspnetUserRepository.GetUserbyCompanyId(coupon.CompanyId.Value);


            couponRatingReviewRepository.Add(model);
            couponRatingReviewRepository.SaveChanges();

            SaveReviewImages(model, Image1String, Image2String, Image3String, Image1ext, Image2ext, Image3ext);

            couponRatingReviewRepository.Update(model);
            couponRatingReviewRepository.SaveChanges();

            //send notification
            emailManagerService.SendNewReviewAvailableToAdvertiser(ReviwerUser.Id, coupon.CouponTitle, model.StarRating.Value, model.Review, ReviwerUser.FullName, AdvertiserUser.Id);


            return true;
        }



        private void SaveReviewImages(CouponRatingReview model, string Image1String, string Image2String, string Image3String,string Image1ext, string Image2ext, string Image3ext)
        {
            
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + model.CouponId + "/" + model.CouponReviewId.ToString());

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }


            if (!string.IsNullOrEmpty(Image1String) )
            {
                 string image1RElpath = "/SMD_Content/Coupons/" + model.CouponId + "/" + model.CouponReviewId.ToString() +  "Image1." + Image1ext;
                string image1abspath = directoryPath + "\\" + "Image1." + Image1ext;

                ImageHelper.SaveBase64(directoryPath + "\\" + image1abspath, Image1String);
                model.ReviewImage1 = image1RElpath;
            }

            if (!string.IsNullOrEmpty(Image2String))
            {
                string image2RElpath = "/SMD_Content/Coupons/" + model.CouponId + "/" + model.CouponReviewId.ToString() + "Image2." + Image1ext;
                string image2abspath = directoryPath + "\\" + "Image2." + Image1ext;

                ImageHelper.SaveBase64(directoryPath + "\\" + image2abspath, Image2String);
                model.ReviewImage2 = image2RElpath;
            }


            if (!string.IsNullOrEmpty(Image3String))
            {
                string image3RElpath = "/SMD_Content/Coupons/" + model.CouponId + "/" + model.CouponReviewId.ToString() + "Image3." + Image3ext;
                string image3abspath = directoryPath + "\\" + "Image3." + Image3ext;

                ImageHelper.SaveBase64(directoryPath + "\\" + image3abspath, Image3String);
                model.Reviewimage3 = image3RElpath;
            }

            
        }


        public String GetUserName(string id)
        {
            return aspnetUserRepository.GetUserName(id);
        }
        public CouponRatingReviewResponseModel GetAllCouponRatingReviewByCompany(GetPagedListRequest request)
        {
            int rowCount;
            return new CouponRatingReviewResponseModel
            {
                CouponsReview = couponRatingReviewRepository.GetAllCouponRatingReviewByCompany(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }
        public string UpdateCouponRating(CouponRatingReview source)
        {
            string respMesg = "True";
            var dbCo = couponRatingReviewRepository.Find(source.CouponReviewId);
            dbCo.Status = source.Status;
            couponRatingReviewRepository.SaveChanges();
            return respMesg;
        }
        public int CouponReviewCount()
        {
            return couponRatingReviewRepository.CouponReviewCount();

        }
        public List<GetRandom3Deal_Result> GetRandomDeals()
        {
            return couponRepository.GetRandomDeals();
        }
        public CouponStatsResponse getDealStats(long Id)
        {
            return couponRepository.getDealStats(Id);
        }

        public List<CampaignResponseLocation> getDealUserLocationByCId(long CouponId)
        {
            return _userCouponViewRepository.getDealUserLocationByCId(CouponId);
        }
        #endregion
    }
}
