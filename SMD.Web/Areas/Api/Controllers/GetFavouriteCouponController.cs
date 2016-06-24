using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
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
    public class GetFavouriteCouponController : ApiController
    {
        #region Public
        private readonly IAdvertService _advertService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public GetFavouriteCouponController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Add Campaigns
        /// </summary>
        public FavouriteCouponResponse Get(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            FavouriteCouponResponse response = new FavouriteCouponResponse();

            try
            {
                response.FavouriteCoupon = _advertService.GetAllFavouriteCouponByUserId(UserId);
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Status = false;
                response.Message = e.ToString();

            }

            return response;
        }
        #endregion
    }
}
