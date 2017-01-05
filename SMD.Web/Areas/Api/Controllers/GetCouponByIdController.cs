using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
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

        [ApiException]
        public CouponDetails Get(string CouponId, string UserId, string Lat, string Lon)
        {
            try
            {

     
            if (string.IsNullOrEmpty(CouponId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            CouponRatingReviewOverallResponse rating = null;


            var coupon = _couponService.GetCouponByIdDefault(Convert.ToInt64(CouponId), UserId, Lat, Lon, out rating);

            var couponPriceoptions = _couponService.GetCouponPriceOptions(Convert.ToInt64( CouponId));


            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SMD.Models.DomainModels.GetCouponByID_Result, CouponDetails>();
                    cfg.CreateMap<SMD.Models.DomainModels.CouponPriceOption, SMD.MIS.Areas.Api.Models.CouponPriceOption>();
                });

//ForSourceMember(x => x.Coupon, opt => opt.Ignore()


            var res = Mapper.Map<SMD.Models.DomainModels.GetCouponByID_Result, CouponDetails>(coupon);

            res.OverAllStarRating = rating.OverAllStarRating;
            res.CouponRatingReviewResponses = rating.CouponRatingReviewResponses;

            if (couponPriceoptions != null && couponPriceoptions.Count > 0)
                res.CouponPriceOptions = couponPriceoptions.Select(a => Mapper.Map<SMD.Models.DomainModels.CouponPriceOption, SMD.MIS.Areas.Api.Models.CouponPriceOption>(a)).ToList();


                //bloody reversed columns here again. due to omar's and app devs bullshit. it started within SP
            foreach (var item in res.CouponPriceOptions)
            {
                res.Price = coupon.discountType.Value == 1 ? res.Savings * coupon.discount.Value / 100 : res.Savings - coupon.discount.Value;
            }


            res.distance = Math.Round(res.distance.Value,1);
            res.FlaggedByCurrentUser = _couponService.CheckCouponFlaggedByUser(Convert.ToInt64(CouponId), UserId);
            return res;

            }
            catch (Exception e)
            {

                throw e;
            }

        }



        #endregion
    }
}
