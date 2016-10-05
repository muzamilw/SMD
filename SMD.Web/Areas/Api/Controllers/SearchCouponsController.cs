using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class SearchCouponsController : ApiController
    {
        #region Private
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchCouponsController(ICouponService couponService)
        {

            this._couponService = couponService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>
      
        //public string Get(string authenticationToken)
        //{
        //    return "hello";

        //}

        //--[ApiException]
        public SearchCouponsViewResponse Get(string categoryId, string ctype, string size, string keywords, string pageNo, string distance, string Lat, string Lon, string UserId)
        {


            SearchCouponsViewResponse response = null;

            try
            {


                if (keywords == "null" || keywords == null)
                {
                    keywords = "";
                }
                response = _couponService.SearchCoupons(Convert.ToInt32(categoryId), Convert.ToInt32(ctype), Convert.ToInt32(size), keywords, Convert.ToInt32(pageNo), Convert.ToInt32(distance), Lat, Lon, UserId).CreateFrom();



                //response = webApiUserService.GetProducts(request).CreateFrom();
            }
            catch (Exception e)
            {
                response = new SearchCouponsViewResponse();
                response.ErrorMessage = e.ToString();

            }

            return response;


        }

        #endregion
    }
}
