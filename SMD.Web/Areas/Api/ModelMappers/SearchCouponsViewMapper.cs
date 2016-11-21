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

                       CouponTitle = source.CouponTitle,

                       CouponImage1 = source.CouponImage1,
                       SwapCost = 0,
                       Price = source.Price.Value,
                       Savings = source.Savings.Value,

                       CompanyId = source.CompanyId.Value,

                       LogoUrl = source.LogoUrl,
                       DaysLeft = 0,//Convert.ToInt32((new DateTime(source.CouponActiveYear.Value, source.CouponActiveMonth.Value, DateTime.DaysInMonth(source.CouponActiveYear.Value, source.CouponActiveMonth.Value)) - DateTime.Today).TotalDays),
                       distance = Math.Round(source.distance.Value, 1),
                       CompanyName = source.CompanyName == null ? "" : source.CompanyName,
                       LocationCity = source.LocationCity == null ? "" : source.LocationCity,
                       LocationTitle = source.LocationTitle == null ? "" : source.LocationTitle,
                       DealsCount = source.DealsCount ,
                       CurrencyCode = source.CurrencyCode== null ? "" : source.CurrencyCode,
                       CurrencySymbol = source.CurrencySymbol == null ? "" : source.CurrencySymbol,
                       AvgRating = source.AvgRating == null ? 0 : source.AvgRating

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