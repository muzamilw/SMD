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

                string base64 = campaign.CouponImage2.Substring(campaign.CouponImage2.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Voucher2DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[4] = savePath;
                campaign.CouponImage2 = savePath;
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
            if (!string.IsNullOrEmpty(campaign.couponImage1) && !campaign.couponImage1.Contains("guid_Voucher4DefaultImage") && !campaign.couponImage1.Contains("http://manage.cash4ads.com/"))
            {

                string base64 = campaign.couponImage1.Substring(campaign.couponImage1.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_Voucher4DefaultImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[6] = savePath;
                campaign.couponImage1 = savePath;
            }
            if (!string.IsNullOrEmpty(campaign.LogoImageBytes))
            {
                string base64 = campaign.LogoImageBytes.Substring(campaign.LogoImageBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\guid_CampaignLogoImage.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[7] = savePath;
            }
            return savePaths;
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponService(ICouponRepository couponRepository, IUserFavouriteCouponRepository userFavouriteCouponRepository, ICompanyService _companyService, IUserPurchasedCouponRepository _userPurchasedCouponRepository, IAccountRepository _accountRepository)
        {
            this.couponRepository = couponRepository;
            this._userFavouriteCouponRepository = userFavouriteCouponRepository;
            this._companyService = _companyService;
            this._userPurchasedCouponRepository = _userPurchasedCouponRepository;
            this._accountRepository = _accountRepository;
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
                var favCoupon = new UserFavouriteCoupon { UserId = UserId, CouponId = CouponId };

                _userFavouriteCouponRepository.Add(favCoupon);
                _userFavouriteCouponRepository.SaveChanges();
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

          
        }

        public void UpdateCampaign(Coupon couponModel)
        {
            couponModel.CompanyId = couponRepository.CompanyId;
            couponModel.UserId = couponRepository.LoggedInUserIdentity;
            var user = UserManager.Users.Where(g => g.Id == couponRepository.LoggedInUserIdentity).SingleOrDefault();
            if (user != null)
                couponModel.CreatedBy = user.FullName;
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
                else if (couponModel.LogoUrl.Contains("Content/Images"))
                {
                    couponModel.LogoUrl = null;
                }
            }
          
            if (couponModel.Status == 3)
            {
                couponModel.Approved = true;
            }
            couponRepository.Update(couponModel);
            couponRepository.SaveChanges();

        

            
        }


        public Coupon GetCouponByIdDefault(long CouponId)
        {

            var coupon = couponRepository.GetCouponById(CouponId).SingleOrDefault();

            if (coupon.LogoUrl == null)
            {
                var company = this._companyService.GetCompanyById(coupon.CompanyId.Value);

                coupon.LogoUrl = "http://manage.cash4ads.com/" + company.Logo;

            }
            else
            {
                coupon.LogoUrl = "http://manage.cash4ads.com/" + coupon.LogoUrl;
            }

            coupon.DaysLeft =Convert.ToInt32( (new DateTime(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value, DateTime.DaysInMonth(coupon.CouponActiveYear.Value, coupon.CouponActiveMonth.Value)) - DateTime.Today).TotalDays);

            //checking if its already flagged by user or not.


            return coupon;


        }

          public bool CheckCouponFlaggedByUser(long CouponId, string UserId)
        {

            return _userFavouriteCouponRepository.CheckCouponFlaggedByUser(CouponId, UserId);
        }



          public List<Coupon> GetPurchasedCouponByUserId(string UserId)
          {
              var result = _userPurchasedCouponRepository.GetPurchasedCouponByUserId(UserId);
             

              return result.ToList();

          }



        public bool PurchaseCoupon(string UserId, long CouponId, double PurchaseAmount)
          {

              List<Account> accounts = _accountRepository.GetByUserId(UserId);

              var userVirtualAccount = accounts.Where(g => g.AccountType == (int)AccountType.VirtualAccount).FirstOrDefault();
              if (PurchaseAmount < userVirtualAccount.AccountBalance)
              {

                  // Update Accounts
                  ////string codeCode = UpdateCouponAccounts(company, smdUser.Company, CouponId, dbContext);
                  ////if (!string.IsNullOrEmpty(codeCode))
                  ////{
                  ////    // Save Changes
                  ////    dbContext.SaveChanges();

                  ////    // Email To User coupon code 
                  ////    try
                  ////    {
                  ////        BackgroundEmailManagerService.SendVoucherCodeEmail(dbContext, company.CompanyId, codeCode);
                  ////    }
                  ////    catch (Exception ex)
                  ////    {

                  ////    }
                  ////}
                  ////else
                  ////{
                  ////    return false;// coupon already redeemed
                  ////}

                  return true;

              }
              else
              {
                  return false;// insufficent balance 
              }
          }

        #endregion
    }
}
