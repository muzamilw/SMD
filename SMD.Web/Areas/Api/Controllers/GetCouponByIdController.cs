using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
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
    public class GetCouponByIdController : ApiController
    {
         
        
        #region Private
        private readonly ICouponService _couponService;
        private readonly ICompanyService _companyService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCouponByIdController(ICouponService _couponService, ICompanyService _companyService)
        {

            this._couponService = _couponService;
            this._companyService = _companyService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public CouponDetails Get(string CouponId, string UserId, string Lat, string Lon)
        {
            if (string.IsNullOrEmpty(CouponId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            var coupon = _couponService.GetCouponByIdDefault(Convert.ToInt64( CouponId),UserId,Lat,Lon);
            

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.GetCouponByID_Result, CouponDetails>());
            var res = Mapper.Map<SMD.Models.DomainModels.GetCouponByID_Result, CouponDetails>(coupon);
            res.distance = Math.Round(res.distance.Value,1);
            res.FlaggedByCurrentUser = _couponService.CheckCouponFlaggedByUser(Convert.ToInt64(CouponId), UserId);
            return res;

        }



        #endregion
    }
}
