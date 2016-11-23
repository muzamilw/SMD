using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
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
    public class CouponReviewController : ApiController
    {
         #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CouponReviewController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion
        #region Public


        public CouponRatingReviewResponseModel Get([FromUri] GetPagedListRequest request)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.CouponRatingReview, CouponRatingReviewResponse>());
            var obj = _couponService.GetAllCouponRatingReviewByCompany(request);
            var retobj = new CouponRatingReviewResponseModel();
            retobj = obj;
            retobj.TotalCount = obj.TotalCount;
           
            return retobj;
        }
        public string Post(CouponRatingReviewApiModel review)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CouponRatingReviewApiModel, SMD.Models.DomainModels.CouponRatingReview>());
            if (review == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _couponService.UpdateCouponRating(Mapper.Map<CouponRatingReviewApiModel, SMD.Models.DomainModels.CouponRatingReview>(review));
        }
        #endregion
    }
}
