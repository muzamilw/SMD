using AutoMapper;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCouponByCompanyIdController : ApiController
    {
         
        
        #region Private
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCouponByCompanyIdController(ICouponService _couponService)
        {

            this._couponService = _couponService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public List<Coupons> Get(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }


            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Coupon, Coupons>());



            var compcoupons = _couponService.GetCouponsByCompanyId(Convert.ToInt32(CompanyId));
            return compcoupons.Select(a => Mapper.Map<SMD.Models.DomainModels.Coupon, Coupons>(a)).ToList();

            //return _advertService.GetCouponsByCompanyId(CompanyId);
        }

        #endregion
    }
}
