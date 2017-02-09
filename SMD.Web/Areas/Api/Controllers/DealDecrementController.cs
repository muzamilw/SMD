using SMD.Interfaces.Services;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class DealDecrementController : ApiController
    {

      
        private ICouponService _couponService;
        public DealDecrementController(ICouponService _couponService)
        {
            this._couponService = _couponService;
        }

        public DealCashBackResponse Get(string CompanyId, string CouponId, string UserId)
        {
            var obj = _couponService.DealDecrementCounter(Convert.ToInt64(CouponId), UserId, Convert.ToInt64(CompanyId));
            return _couponService.DealDecrementCounter(Convert.ToInt64(CouponId), UserId, Convert.ToInt64(CompanyId));
        }


    }
}
