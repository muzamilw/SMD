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


        public CouponDetails Get(string CouponId, string UserId)
        {
            if (string.IsNullOrEmpty(CouponId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            var coupon = _couponService.GetCouponByIdDefault(Convert.ToInt64( CouponId));

            

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Coupon, CouponDetails>());
            var res =  Mapper.Map<SMD.Models.DomainModels.Coupon, CouponDetails>(coupon);
            res.FlaggedByCurrentUser = _couponService.CheckCouponFlaggedByUser(Convert.ToInt64(CouponId), UserId);
            return res;



            //var retCoupon = new CouponDetails
            //{
            //    AdvertisersLogoPath = coupon.LogoUrl,
            //    City = "",
            //    Country = "",
            //    CouponActualValue = coupon.CouponActualValue,
            //    CouponDiscountedValue = coupon.CouponDiscountValue.HasValue == true? coupon.CouponDiscountValue.Value:0,
            //    CouponId = coupon.CampaignId,
            //    CouponImage1 = coupon.ImagePath,
            //    CouponImage2 = coupon.couponImage2,
            //    CouponImage3 = coupon.CouponImage3,
            //    CouponImage4 = coupon.CouponImage4,
            //    CouponName = coupon.CampaignName,
            //    CouponSwapValue = coupon.CouponSwapValue,
            //    CouponTakenValue = 0,
            //    CouponTitle = coupon.DisplayTitle,
            //    DaysLeft = 10,
            //    FinePrintLine1 = coupon.VoucherFinePrintLine1,
            //    FinePrintLine2 = coupon.VoucherFinePrintLine2,
            //    FinePrintLine3 = coupon.VoucherFinePrintLine3,
            //    FinePrintLine4 = coupon.VoucherFinePrintLine4,
            //    FinePrintLine5 = coupon.VoucherFinePrintLine5,
            //    HighlightLine1 = coupon.VoucherHighlightLine1,
            //    HighlightLine2 = coupon.VoucherHighlightLine2,
            //    HighlightLine3 = coupon.VoucherHighlightLine3,
            //    HighlightLine4 = coupon.VoucherHighlightLine4,
            //    HighlightLine5 = coupon.VoucherHighlightLine5,
            //    LocationLine1 = coupon.VoucherLocationLine1,
            //    LocationLine2 = coupon.VoucherLocationLine2,
            //    LocationLine3 = coupon.VoucherLocationLine3,
            //    LocationLine4 = coupon.VoucherLocationLine4,
            //    LocationLine5 = coupon.VoucherLocationLine5,
            //    HowToRedeemLine1 = coupon.VoucherHowToRedeemLine1,
            //    HowToRedeemLine2 = coupon.VoucherHowToRedeemLine2,
            //    HowToRedeemLine3 = coupon.VoucherHowToRedeemLine3,
            //    HowToRedeemLine4 = coupon.VoucherHowToRedeemLine4,
            //    HowToRedeemLine5 = coupon.VoucherHowToRedeemLine5,
            //    LocationLAT = coupon.VoucherLocationLAT,
            //    LocationLON = coupon.VoucherLocationLON,
            //    Phone = coupon.VoucherRedemptionPhone

            //};

           

        }



        #endregion
    }
}
