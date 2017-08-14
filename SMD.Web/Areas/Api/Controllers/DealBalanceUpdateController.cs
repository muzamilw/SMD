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
    public class DealBalanceUpdateController : ApiController
    {
       

           private ICouponService _couponService;
           public DealBalanceUpdateController(ICouponService _couponService)
           {
            this._couponService = _couponService;
           }

           public DealCashBackResponse Get(string UserId,string CouponId,string Amount,string CompanyId)
           {
              
              return _couponService.UpdateCouponPrice(UserId, Convert.ToInt64(CouponId), Convert.ToDouble(Amount),Convert.ToInt64(CompanyId));
             
           }

    }
    public class Response
    {
        public string Message { get; set; }
        
        public bool Status { get; set; }
    }
}
