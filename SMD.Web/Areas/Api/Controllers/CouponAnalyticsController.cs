using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponAnalyticsController : ApiController
    {
        #region private
        private readonly ICouponService _ICouponService;
        #endregion

        #region Constructor
        public CouponAnalyticsController(ICouponService ICouponService)
        {
            this._ICouponService = ICouponService ;
        }
         #endregion
        
        #region public
        public DealByCouponIdForAnalyticsResponse getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity, int type, int Gender, int age, int Stype)
        {

            DealByCouponIdForAnalyticsResponse data = new DealByCouponIdForAnalyticsResponse();
            
            if (type == 1)
            {
                data.lineCharts = _ICouponService.getDealByCouponIDAnalytics(CouponID, dateRange, Granularity);
                data.pieCharts = _ICouponService.getDealByCouponIdRatioAnalytic(CouponID, dateRange);
                data.ImpressionStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 1);
                data.ClickTrouStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 2);
                data.UserLocation = _ICouponService.getDealUserLocationByCId(CouponID);

            }
            else
            {
                if (Stype == 1) {
                     data.ImpressionStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 1);
                }
                else if (Stype == 2) 
                {
                    data.ClickTrouStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 2); 
                }
               
                
            }

         //   data.expiryDate = _ICouponService.getExpiryDate(CouponID).ToString("D");
            return data;
        }
        #endregion
    }
}

 //public int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type)
 //       {
 //           return couponRepository.getDealStatByCouponIdFormAnalytic(dealId, Gender, age, type); 
 //       }