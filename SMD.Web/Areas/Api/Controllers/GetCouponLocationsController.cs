using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCouponLocationsController : ApiController
    {


        #region Attributes
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        public GetCouponLocationsController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        #region Method
        public List<CouponLocation> Get(string categoryId, string ctype, string size, string keywords, string pageNo, string distance, string Lat, string Lon, string UserId)
        {
           //var query= from coupon in _couponService.GetAllCouponList() where coupon.Status==3 && coupon.
            //var obj = from coupon in 
            return null;
        }
        #endregion
    }
}
