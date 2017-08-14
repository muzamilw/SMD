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


            int additionalDiscount = 0;
            int daysleft  = 0;
            if (coupon.CouponListingMode == 1) //7 day deal
            {
                daysleft = (coupon.ApprovalDateTime.Value.AddDays(7) - DateTime.Now).Days;
            }
            else //30 day deal
            {
                daysleft = (coupon.ApprovalDateTime.Value.AddDays(30) - DateTime.Now).Days;
            }

            if (coupon.DealEndingDiscountType == 1 && daysleft >= 0 && daysleft <=3) //30% discount on last 3 days
            {
                additionalDiscount = 20;
            }
            else if (coupon.DealEndingDiscountType == 2 && daysleft == 3) //30% discount on last 3 days
            {
                additionalDiscount = 20;
            }
            else if (coupon.DealEndingDiscountType == 2 && daysleft >= 0 && daysleft <= 2) //30% discount on last 3 days
            {
                additionalDiscount = 25;
            }
            else if (coupon.DealEndingDiscountType == 3 && daysleft == 3) //30% discount on last 3 days
            {
                additionalDiscount = 20;
            }
            else if (coupon.DealEndingDiscountType == 3 && daysleft == 2) //30% discount on last 3 days
            {
                additionalDiscount = 25;
            }
            else if (coupon.DealEndingDiscountType == 3 && daysleft >= 0 && daysleft <= 1) //30% discount on last 3 days
            {
                additionalDiscount = 30;
            }
             /////////////////////////////// dollar amount
            if (coupon.DealEndingDiscountType == 4 && daysleft >= 0 && daysleft <= 3) //30% discount on last 3 days
            {
                additionalDiscount = 10;
            }
            else if (coupon.DealEndingDiscountType == 5 && daysleft == 3) //30% discount on last 3 days
            {
                additionalDiscount = 10;
            }
            else if (coupon.DealEndingDiscountType == 5 && daysleft >= 0 && daysleft <= 2) //30% discount on last 3 days
            {
                additionalDiscount = 20;
            }
            else if (coupon.DealEndingDiscountType == 6 && daysleft == 3) //30% discount on last 3 days
            {
                additionalDiscount = 10;
            }
            else if (coupon.DealEndingDiscountType == 6 && daysleft == 2) //30% discount on last 3 days
            {
                additionalDiscount = 20;
            }
            else if (coupon.DealEndingDiscountType == 6 && daysleft >= 0 && daysleft <= 1) //30% discount on last 3 days
            {
                additionalDiscount = 30;
            }
                
                
            foreach (var item in res.CouponPriceOptions)
            {
                if (coupon.discountType.HasValue)
                {
                    item.Savings = coupon.discountType.Value == 1 ? item.Price.Value - (item.Price.Value * (coupon.discount.Value + additionalDiscount) / 100) : item.Price.Value - (coupon.discount.Value + additionalDiscount);
                }
            }


            res.distance = Math.Round(res.distance.Value,1);
            res.FlaggedByCurrentUser = _couponService.CheckCouponFlaggedByUser(Convert.ToInt64(CouponId), UserId);
            if (coupon.CouponListingMode == 3)
            {
                res.IsCashAMoon = 1;
            }
            else
            {
                res.IsCashAMoon = 0;
            }
            if (coupon.CashBackDealCounter == null)
            {
                res.CashBackDealCounter = 0;
            }
            res.IsCounterApplied = coupon.IsAppliedDec??false;
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
