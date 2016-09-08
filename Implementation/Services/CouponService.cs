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
    public class CouponService: ICouponService
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
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }
        private string[] SaveImages(Coupon campaign)
        {
            string[] savePaths = new string[8];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + campaign.CouponId);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
           
            if (!string.IsNullOrEmpty(campaign.CouponImage2) && !campaign.CouponImage2.Contains("guid_Voucher2DefaultImage") && !campaign.CouponImage2.Contains("http://manage.cash4ads.com/"))
            {
               if(campaign.CouponImage2.Contains("SMD_Content"))
               {
                string[] paths = campaign.CouponImage2.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length-1]);
                string savePath = directoryPath + "\\guid_Voucher2DefaultImage.jpg";

                File.Copy(url, savePath,true);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[4] = savePath;
                campaign.CouponImage2 = savePath;
               }
            }
            if (!string.IsNullOrEmpty(campaign.CouponImage3) && !campaign.CouponImage3.Contains("guid_Coupon3DefaultImage") && !campaign.CouponImage3.Contains("http://manage.cash4ads.com/"))
            {
                if(campaign.CouponImage3.Contains("SMD_Content"))
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
            if (!string.IsNullOrEmpty(campaign.couponImage1) && !campaign.couponImage1.Contains("guid_Voucher4DefaultImage") && !campaign.couponImage1.Contains("http://manage.cash4ads.com/"))
            {
                if(campaign.couponImage1.Contains("SMD_Content"))
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
                if(campaign.LogoUrl.Contains("SMD_Content"))
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

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponService(ICouponRepository couponRepository, IUserFavouriteCouponRepository userFavouriteCouponRepository, ICompanyService _companyService,
            IUserPurchasedCouponRepository _userPurchasedCouponRepository, IAccountRepository _accountRepository, ICouponCategoriesRepository _couponCategoriesRepository, ICurrencyRepository _currencyRepository, IWebApiUserService _userService, IUserCouponViewRepository userCouponViewRepository, IEmailManagerService emailManagerService, WebApiUserService webApiUserService, IStripeService stripeService, IProductRepository productRepository
            , ITaxRepository taxRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, ICompanyRepository iCompanyRepository)
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
                var differ = new DateTime(item.CouponActiveYear.Value,item.CouponActiveMonth.Value,DateTime.DaysInMonth(item.CouponActiveYear.Value, item.CouponActiveMonth.Value)) - DateTime.Today;
                item.DaysLeft = Convert.ToInt32( differ.TotalDays);

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

            if(couponModel.LogoUrl != null)
            {
                if(couponModel.LogoUrl[0] == '/')
                {
                    couponModel.LogoUrl = couponModel.LogoUrl.Substring(1, couponModel.LogoUrl.Length-1);
                }
            }
            string[] paths = SaveImages(couponModel);
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

            foreach(var couponCategory in _couponCategoriesRepository.GetAll().Where(g=>g.CouponId == couponModel.CouponId).ToList())
            {
                _couponCategoriesRepository.Delete(couponCategory);
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

        

            
        }


        //called from mobile apps
        public GetCouponByID_Result GetCouponByIdDefault(long CouponId,string UserId,string Lat, string Lon)
        {

            return couponRepository.GetCouponByIdSP(CouponId,UserId,Lat,Lon);


            

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

              var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
              //if (PurchaseAmount < userVirtualAccount.AccountBalance)
              //{

                  //Update Accounts
                  TransactionManager.CouponPurchaseTransaction(CouponId, PurchaseAmount, userVirtualAccount.CompanyId.Value);


                  //enter the entry for purchased coupon,
                  var newPurchasedCoupon = new UserPurchasedCoupon { CouponId = CouponId, IsRedeemed = false, PurchaseAmount = PurchaseAmount, PurchaseDateTime = DateTime.Now, RedemptionOperator = null, UserId = UserId };

                  _userPurchasedCouponRepository.Add(newPurchasedCoupon);
                  _userPurchasedCouponRepository.SaveChanges();


                  //incrementing the Issues/Purchased count 

                  var oCoupon = couponRepository.GetCouponByIdSingle(CouponId);
                  oCoupon.CouponIssuedCount = (oCoupon.CouponIssuedCount == null ? 0 : oCoupon.CouponIssuedCount ) + 1;

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

                oCoupon.CouponRedeemedCount =   (oCoupon.CouponRedeemedCount == null ? 0 : oCoupon.CouponRedeemedCount) +  1;

                couponRepository.Update(oCoupon);
                couponRepository.SaveChanges();

                return 1;
            

            }
              else if ( company.VoucherSecretKey != pinCode)
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

            var res =couponRepository.GetCouponsByCompanyId(CompanyId);
            foreach (var coupon in res)
	        {
		        coupon.DaysLeft =Convert.ToInt32( (new DateTime(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value, DateTime.DaysInMonth(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value)) - DateTime.Today).TotalDays);


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
            // Update 
            if (dbCo != null)
            {
                // Approval
                if (source.Approved == true)
                {
                    double PaymentToBeCharged = 0;

                    dbCo.Approved = true;
                    dbCo.ApprovalDateTime = DateTime.Now;
                    dbCo.ApprovedBy = couponRepository.LoggedInUserIdentity;
                    dbCo.Status = (Int32)AdCampaignStatus.Live;
                    // Stripe payment + Invoice Generation
                    // Muzi bhai said we will see it on latter stage 
                    //todo pilot: unCommenting Stripe payment code on Ads approval

                    respMesg = MakeStripePaymentandAddInvoiceForCoupon(dbCo, out PaymentToBeCharged);
                    if (respMesg.Contains("Failed"))
                    {
                        return respMesg;
                    }
                    else
                    {
                        TransactionManager.CouponApprovalTransaction(dbCo.CouponId, PaymentToBeCharged, dbCo.CompanyId.Value);
                    }
                }
                // Rejection 
                else
                {
                    dbCo.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbCo.Approved = false;
                    dbCo.RejectedReason = source.RejectedReason.ToString();
                    emailManagerService.SendCouponRejectionEmail(dbCo.UserId, dbCo.RejectedReason);
                }
                dbCo.ModifiedDateTime = DateTime.Now;
                dbCo.ModifiedBy = couponRepository.LoggedInUserIdentity;

                couponRepository.SaveChanges();

            }
            return respMesg;
        }
        private string MakeStripePaymentandAddInvoiceForCoupon(Coupon source, out double PaymentAmount)
        {
            #region Stripe Payment
            string response = null;
            Boolean isSystemUser = false;
            double amount = 0;
            // User who added Campaign for approval 
            var user = webApiUserService.GetUserByUserId(source.UserId);
            // Get Current Product
            var product = (dynamic)null;
            if(source.CouponListingMode==1)
                product = productRepository.GetProductByCountryId("couponfree");
            if (source.CouponListingMode == 2)
                product = productRepository.GetProductByCountryId("couponunlimited");
            if (source.CouponListingMode == 3)
                product = productRepository.GetProductByCountryId("couponnationwide");
            // Tax Applied
            var tax = taxRepository.GetTaxByCountryId(user.Company.CountryId);
            // Total includes tax
            if (product != null)
            {
                amount = product.SetupPrice ?? 0 + tax.TaxValue ?? 0;

                PaymentAmount = amount;


                // If It is not System User then make transation 
                //if (user.Roles.Any(role => role.Name.ToLower().Equals("user")))
                //{
                // Make Stripe actual payment 
                response = stripeService.ChargeCustomer((int?)amount, user.Company.StripeCustomerId);
                isSystemUser = false;

            }
            else
            {
                PaymentAmount = 0;
                response = "Failed : Product not defined";
            }

            #endregion

            if (response != null && !response.Contains("Failed"))
            {
                if (source.CompanyId!= null)
                {
                    String CompanyName = _iCompanyRepository.GetCompanyNameByID(source.CompanyId.Value);
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
                        PQID=null,
                        ProductId = product.ProductId,
                        ItemName = product.ProductName,
                        ItemAmount = (double)amount,
                        ItemTax = (double)(tax != null ? tax.TaxValue : 0),
                        ItemDescription = "This is description!",
                        ItemGrossAmount = (double)amount,
                        CouponID = source.CouponId,

                    };
                    invoiceDetailRepository.Add(invoiceDetail);
                    invoiceDetailRepository.SaveChanges();

                    #endregion
                }
            }
            return response;
        }



        #endregion
    }
}
