using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface ICouponService
    {
        IEnumerable<Coupon> GetAllCoupons();

        CampaignResponseModel GetCouponById(long CampaignId);

        CampaignResponseModel GetCoupons(AdCampaignSearchRequest request);

        IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId);

        bool SetFavoriteCoupon(string UserId, long CouponId, bool mode);

        SearchCouponsResponse SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId);
    }
}
