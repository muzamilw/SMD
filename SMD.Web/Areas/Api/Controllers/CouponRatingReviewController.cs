using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponRatingReviewController : ApiController
    {

        #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CouponRatingReviewController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion
        #region Public


        public BaseApiResponse Put(string authenticationToken,CouponRatingReviewRequest request)
        {
            try
            {


                var model = new CouponRatingReview { CompanyId = request.CompanyId, CouponId = request.CouponId, RatingDateTime = DateTime.Now, Review = request.Review, StarRating = request.StarRating, Status = 1, UserId = request.UserId };

                return new BaseApiResponse { Message = "Success", Status = _couponService.InsertCouponRatingReview(model, request.ReviewImage1, request.ReviewImage2, request.Reviewimage3, request.ReviewImage1ext, request.ReviewImage2ext, request.Reviewimage3ext) };

            }
            catch (Exception e)
            {

                return new BaseApiResponse { Message = e.ToString(), Status = false };
            }
        }
        #endregion
    }
}
