using AutoMapper;
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
    public class GetCurrencyController : ApiController
    {
        #region Attribites
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        public GetCurrencyController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        #region Method

        public CurrencyApprov Get(int id)
        {
            var obj = _couponService.GetCurrenyById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Currency, CurrencyApprov>());
            return Mapper.Map<Currency, CurrencyApprov>(obj);
        }


        #endregion
    }
}
