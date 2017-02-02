using SMD.Models.Common;
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
        List<Coupon> GetAllCouponList();
        List<AdGetCouponCategories> GetDealsByCtgId(long CouponId);
      
        IEnumerable<Coupon> GetAllCoupons();

        CampaignResponseModel GetCouponById(long CampaignId);

        CampaignResponseModel GetCoupons(AdCampaignSearchRequest request);

        IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId);

        bool SetFavoriteCoupon(string UserId, long CouponId, bool mode);
        void CreateCampaign(Coupon couponModel);
        void UpdateCampaign(Coupon couponModel);
        SearchCouponsResponse SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId);


        GetCouponByID_Result GetCouponByIdDefault(long CouponId, string UserId, string Lat, string Lon, out CouponRatingReviewOverallResponse rating);

        bool CheckCouponFlaggedByUser(long CouponId, string UserId);

        List<PurchasedCoupons> GetPurchasedCouponByUserId(string UserId);

        bool PurchaseCoupon(string UserId, long CouponId, double PurchaseAmount);


        int RedeemPurchasedCoupon(string UserId, long couponPurchaseId, string pinCode, string operatorId);

        List<Coupon> GetCouponsByCompanyId(int CompanyId);
        CouponsResponseModelForApproval GetAdCampaignForAproval(GetPagedListRequest request);


        List<CouponPriceOption> GetCouponPriceOptions(long CouponId);
        string UpdateCouponForApproval(Coupon source);
        Currency GetCurrenyById(int id);
        IEnumerable<getDealByCouponID_Result> getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity);
        IEnumerable<getDealByCouponIdRatioAnalytic_Result> getDealByCouponIdRatioAnalytic(int ID, int dateRange);
        int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type);
        
        DateTime getExpiryDate(int CouponId);

        bool PauseAllCoupons(int CompanyId);

        bool CompleteAllCoupons(int CompanyId);

        int GetFreeCouponCount();
        CouponsResponseModelForApproval GetMarketingDeals(GetPagedListRequest request);
        String GetUserName(string id);
        string UpdateDealMarketing(Coupon source);

        bool InsertCouponRatingReview(CouponRatingReview model, string Image1String, string Image2String, string Image3String, string Image1ext, string Image2ext, string Image3ext);

        CouponRatingReviewResponseModel GetAllCouponRatingReviewByCompany(GetPagedListRequest request);

        string UpdateCouponRating(CouponRatingReview source);
        int CouponReviewCount();
        List<GetRandom3Deal_Result> GetRandomDeals();
        CouponStatsResponse getDealStats(long Id);
        List<CampaignResponseLocation> getDealUserLocationByCId(long CouponId);
        List<getDealImpressionByAgeByCouponId_Result> getDealImpressionByAgeByCouponId(long CId);
        List<getDealImpressionByGenderByCouponId_Result> getDealImpressionByGenderByCouponId(long CId);
        List<getDealImpressionByProfessionByCouponId_Result> getDealImpressionByProfessionByCouponId(long CId);


    }
}
