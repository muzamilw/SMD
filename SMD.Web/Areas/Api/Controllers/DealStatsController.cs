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
    public class DealStatsController : ApiController
    {
        #region Attributes
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        public DealStatsController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        // GET: api/DealStats/5
        public CouponStatsResponse Get(int id)
        {
            return _couponService.getDealStats(id);
        }

       
    }
}
