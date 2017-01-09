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
    public class UserCouponCategoryClickController : ApiController
    {
        #region Public
        private readonly ICouponCategoryService couponCategoryService;
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserCouponCategoryClickController(ICouponCategoryService couponCategoryService)
        {
            this.couponCategoryService = couponCategoryService;
            
        }

        #endregion
        #region Public




        public BaseApiResponse Put(string authenticationToken, string UserId, int CouponCategoryId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response = new BaseApiResponse { Message = "Success", Status = true };
            var result = false;

            try
            {
                result = couponCategoryService.InsertUserCouponCategoryClick(CouponCategoryId, UserId);
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
