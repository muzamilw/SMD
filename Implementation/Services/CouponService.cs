using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class CouponService: ICouponService
    {
        #region Private

        private readonly ICouponRepository couponRepository;
        private readonly IUserFavouriteCouponRepository _userFavouriteCouponRepository;


        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponService(ICouponRepository couponRepository, IUserFavouriteCouponRepository userFavouriteCouponRepository)
        {
            this.couponRepository = couponRepository;
            this._userFavouriteCouponRepository = userFavouriteCouponRepository;
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
                // UserAndCostDetails = _adCampaignRepository.GetUserAndCostDetail()
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


        #endregion
    }
}
