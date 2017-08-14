using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetRandomDealController : ApiController
    {
        #region Attributes
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        public GetRandomDealController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        #region Method
        public List<GetRandom3Deal_Result> Get()
        {
            var obj = _couponService.GetRandomDeals();
            return obj;
        }
        #endregion
    }
}
