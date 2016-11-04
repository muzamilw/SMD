using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.Common;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponController : ApiController
    {

        #region Public
        private readonly ICouponService _couponService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public CampaignRequestResponseModel Get([FromUri] AdCampaignSearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                if (request.CampaignId > 0)
                {
                    return _couponService.GetCouponById(request.CampaignId).CreateCampaignFrom();
                }
                else
                {
                    return _couponService.GetCoupons(request).CreateCampaignFrom();
                }
            }
        }
        public void Post(SMD.Models.DomainModels.Coupon couponModel)
        {
            //couponModel.Status = (int)AdCampaignStatus.Draft;
            if (couponModel.CouponId > 0)
            {
                _couponService.UpdateCampaign(couponModel);

            }
            else
            {
                _couponService.CreateCampaign(couponModel);

            }

        }
        #endregion
    }
}
