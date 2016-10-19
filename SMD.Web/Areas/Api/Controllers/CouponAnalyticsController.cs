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
        public DealByCouponIdForAnalyticsResponse getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity)
        {

            DealByCouponIdForAnalyticsResponse data = new DealByCouponIdForAnalyticsResponse();
            data.lineCharts = _ICouponService.getDealByCouponIDAnalytics(CouponID, dateRange, Granularity);
            data.pieCharts = _ICouponService.getDealByCouponIdRatioAnalytic(CouponID, dateRange);
            data.expiryDate = _ICouponService.getExpiryDate(CouponID).ToString("D");
            return data;
        }
        #endregion
    }
}