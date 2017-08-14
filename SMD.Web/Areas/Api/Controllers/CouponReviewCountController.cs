using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponReviewCountController : ApiController
    {

        #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CouponReviewCountController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion
        #region Method
        public int Get()
        {
            int count = 0;
            count = _couponService.CouponReviewCount();
            return count;
        }
        #endregion

    }
}
