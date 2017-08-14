using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;
using System;
using AutoMapper;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using AutoMapper;
using SMD.WebBase.Mvc;
using SMD.Implementation.Services;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class UserPurchasedCouponController : ApiController
    {
        #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserPurchasedCouponController(ICouponService _couponService)
        {
            this._couponService = _couponService;
        }

        #endregion
        #region Public

          public UserPurchasedCouponResponse Get(string authenticationToken, string UserId)
          {

              if (string.IsNullOrEmpty(authenticationToken))
              {
                  throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
              }

              var response = new UserPurchasedCouponResponse { Message = "Success", Status = true };

              //Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Coupon, SMD.Models.Common.PurchasedCoupons>());
              try
              {
                  var res = _couponService.GetPurchasedCouponByUserId(UserId);
                  //Mapper.Map<SMD.Models.DomainModels.Coupon, CouponDetails>(coupon)
                  response.PurchasedCoupon = res;//.Select(a => Mapper.Map<SMD.Models.DomainModels.Coupon, SMD.Models.Common.PurchasedCoupons>(a));
                  //response.PurchasedCoupon = 
                      
              }
              catch (Exception e)
              {

                  response.Status = false;
                  response.Message = e.Message;
              }

              return response;

          }



        /// <summary>
        /// Purchase a Coupon
        /// </summary>
        [ApiExceptionCustom]
          public BaseApiResponse Post(string authenticationToken, string UserId, int CouponId, double SwapCost)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {



                if (string.IsNullOrEmpty(authenticationToken))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                }

                if (_couponService.PurchaseCoupon(UserId, CouponId, SwapCost))
                    return response;
                else
                {
                    response.Status = false;
                    response.Message = "Error in purchasing coupon";
                    return response;
                }
            }
            catch (Exception e)
            {

                response.Status = false;
                response.Message = e.ToString();
                return response;
            }
        }


        #endregion
    }
}
