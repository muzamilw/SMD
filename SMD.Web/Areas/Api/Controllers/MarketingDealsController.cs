using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class MarketingDealsController : ApiController
    {
        #region Attributes

        private readonly ICouponService _couponService;
        #endregion

        #region Constructors
        public MarketingDealsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion

        #region Methods
        public AddCouponsResponseModelForApproval Get([FromUri]GetPagedListRequest request)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.vw_Coupons, CouponsApproval>());
            var obj = _couponService.GetMarketingDeals(request);
            var retobj = new AddCouponsResponseModelForApproval();
            foreach (var item in obj.Coupons)
            {
                item.ApprovedBy = _couponService.GetUserName(item.ApprovedBy);
                retobj.Coupons.Add(Mapper.Map<SMD.Models.DomainModels.vw_Coupons, CouponsApproval>(item));
            }
            retobj.TotalCount = obj.TotalCount;
            return retobj;
        }
        public string Post(CouponsApproval coupon)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CouponsApproval, SMD.Models.DomainModels.Coupon>());
            if (coupon == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _couponService.UpdateDealMarketing(Mapper.Map<CouponsApproval, SMD.Models.DomainModels.Coupon>(coupon));
        }
        #endregion
    }
}
