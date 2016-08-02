using System.Collections.Generic;
using System.Linq;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using SMD.Models.Common;
using System;

namespace SMD.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Product View Mapper
    /// </summary>
    public static class SearchCouponsViewMapper
    {
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static Coupons CreateFrom(this SearchCoupons_Result source)
        {
            return new Coupons
                   {
                         CouponId = source.CouponId,
        CouponName = source.CouponName,
        CouponTitle = source.CouponTitle,
        Firstline = source.Firstline,
        SecondLine = source.SecondLine,
        CouponImage = source.CouponImage,
        SwapCost = Convert.ToDouble( source.CouponSwapValue),
        Price = Convert.ToDouble( source.CouponActualValue),
        Savings = Convert.ToDouble( source.CouponActualValue),

        CompanyId = source.CompanyId,

        AdvertisersLogoPath = source.AdvertisersLogoPath,

      
                   };
        }

        /// <summary>
        /// Get Products Response Mapper
        /// </summary>
        public static SearchCouponsViewResponse CreateFrom(this SearchCouponsResponse source)
        {
            return new SearchCouponsViewResponse
                   {
                       Status = source.Status, 
                       Message = source.Message, 
                       TotalCount = source.TotalCount,
                       Coupons = source.Coupons != null ? source.Coupons.Select(pd => pd.CreateFrom()) : new List<Coupons>()
                   };
        }
    }
}