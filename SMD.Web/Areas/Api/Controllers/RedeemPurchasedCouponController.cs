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
    public class RedeemPurchasedCouponController : ApiController
    {
        #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public RedeemPurchasedCouponController(ICouponService _couponService)
        {
            this._couponService = _couponService;
        }

        #endregion
        #region Public

         


        /// <summary>
        /// Purchase a Coupon
        /// </summary>
        [ApiExceptionCustom]
        public BaseApiResponse Post(string authenticationToken, string UserId, long couponPurchaseId, string pinCode, string operatorId)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                }

                var result = _couponService.RedeemPurchasedCoupon(UserId, couponPurchaseId, pinCode, operatorId);
                if (result == 1)
                    return response; // all good
                else if ( result == 3)
                {
                    response.Status = false;
                    response.Message = "Invalid Pin Code";
                    return response; //
                }
                else if (result == 4)
                {
                    response.Status = false;
                    response.Message = "Coupon already redeemed";
                    return response; //
                }
                else
                {
                    response.Status = false;
                    response.Message = "Invalid UserCode";
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
