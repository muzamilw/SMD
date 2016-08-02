using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class UserFavouriteCouponController : ApiController
    {
        #region Public
        private readonly IAdvertService _advertService;
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserFavouriteCouponController(IAdvertService advertService, ICouponService couponService)
        {
            _advertService = advertService;
            _couponService = couponService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Add Campaigns
        /// </summary>
        public FavouriteCouponResponse Get(string authenticationToken,string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            FavouriteCouponResponse response = new FavouriteCouponResponse();

            try
            {
                 Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Coupon, Coupons>());

           

            var favs = _couponService.GetAllFavouriteCouponByUserId(UserId);
            response.FavouriteCoupon = favs.Select(a => Mapper.Map<SMD.Models.DomainModels.Coupon, Coupons>(a)).ToList();

               
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Status = false;
                response.Message = e.ToString();

            }

            return response;
        }


        public BaseApiResponse Post(string authenticationToken,string UserId, long CouponId, bool mode)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response = new BaseApiResponse { Message = "Success", Status = true };
            var result = false;

            try
            {
                result = _couponService.SetFavoriteCoupon(UserId, CouponId, mode);
            }
            catch (Exception e)
            {

                response.Status = false;
                response.Message = e.Message;
            }

            return response;

        }
        #endregion
    }
}
