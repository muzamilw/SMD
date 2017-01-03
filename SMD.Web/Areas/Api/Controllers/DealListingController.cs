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
    public class DealListingController : ApiController
    {
       private readonly ICouponService _couponService;
       
        public DealListingController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        
        public List<CouponCategories> Get(int categoryid)
        {
            return _couponService.GetDealsByCtgId(categoryid);
        }
    }
}
