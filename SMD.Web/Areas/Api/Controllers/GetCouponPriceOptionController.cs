using AutoMapper;
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
    public class GetCouponPriceOptionController : ApiController
    {
        #region Attributes
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        public GetCouponPriceOptionController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        #region Methods
        public List<CouponPriceOption> Get(long CouponId)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.CouponPriceOption, CouponPriceOption>());
            var obj = _couponService.GetCouponPriceOptions(CouponId);
            return Mapper.Map<List<SMD.Models.DomainModels.CouponPriceOption>, List<CouponPriceOption>>(obj);
        }
        #endregion
    }
}
