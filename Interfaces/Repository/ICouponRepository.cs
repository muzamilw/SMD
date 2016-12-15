﻿using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICouponRepository : IBaseRepository<Coupon, long>
    {
        IEnumerable<Coupon> GetAllCoupons();
        Coupon Find(long id);
        IEnumerable<Coupon> SearchCampaign(AdCampaignSearchRequest request, out int rowCount);
        IEnumerable<Coupon> GetCouponById(long campaignId);

        Coupon GetCouponByIdSingle(long couponId);


        IEnumerable<SearchCoupons_Result> SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId);

        List<Coupon> GetCouponsByCompanyId(int CompanyId);
        IEnumerable<vw_Coupons> GetCouponsForApproval(GetPagedListRequest request, out int rowCount);


        GetCouponByID_Result GetCouponByIdSP(long CouponId, string UserId, string Lat, string Lon);
        IEnumerable<getDealByCouponID_Result> getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity);
        IEnumerable<getDealByCouponIdRatioAnalytic_Result> getDealByCouponIdRatioAnalytic(int ID, int dateRange);
        int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type);
        
        DateTime getExpiryDate(int CouponId);

        bool PauseAllCoupons(int CompanyId);

        bool CompleteAllCoupons(int CompanyId);

        int GetCouponByBranchId(long id);
        int GetFreeCouponCount();
        IEnumerable<vw_Coupons> GetMarketingDeals(GetPagedListRequest request, out int rowCount);

        List<GetRandom3Deal_Result> GetRandomDeals();

        List<GetUsersCouponsForEmailNotification_Result> GetUsersCouponsForEmailNotification(int mode);


        List<GetUsersCouponsForEmailNotification_Result> GetDealsWhichHavejustExpired();


        bool CompleteCoupons(long[] couponIds);

    }
}
