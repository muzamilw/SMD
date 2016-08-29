using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiModel = SMD.MIS.Areas.Api.Models;
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
      
        public AddCouponsResponseModelForApproval Get([FromUri] GetPagedListRequest request)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.CouponsResponseModelForApproval, ApiModel.AddCouponsResponseModelForApproval>());
            var obj = _couponService.GetAdCampaignForAproval(request);
            return Mapper.Map<DomainModel.CouponsResponseModelForApproval, ApiModel.AddCouponsResponseModelForApproval>(obj);
        }
            
       
      

        #endregion
    }
}
