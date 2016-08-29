using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DomainModel = SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponApprovalController : ApiController
    {
        #region Attributes 
        private readonly ICouponService _couponService;
        #endregion 

        #region Constructor

        public CouponApprovalController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        #endregion

        #region Methods
      
        public AddCouponsResponseModelForApproval Get([FromUri]GetPagedListRequest request)
        {
            
            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Coupon, CouponsApproval >());
            var obj = _couponService.GetAdCampaignForAproval(request);

           var retobj =  new AddCouponsResponseModelForApproval();

          
            foreach (var item in obj.Coupons)
            {

                retobj.Coupons.Add(Mapper.Map<SMD.Models.DomainModels.Coupon, CouponsApproval>(item));
            }


            retobj.TotalCount = obj.TotalCount;

            return retobj;
           

            
        }
            
       
      

        #endregion
    }
}
