using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{
    public static class CouponCategoryMapper
    {
        public static CouponCategoryResponse CreateFrom(this IEnumerable<Models.DomainModels.CouponCategory> source)
        {
            return new CouponCategoryResponse
            {
                CouponCategories = source != null ? source.Select(edu => edu.CreateFrom()) : new List<CouponCategory>(),
                Status = true
            };
        }
        public static CouponCategory CreateFrom(this Models.DomainModels.CouponCategory source)
        {
            return new CouponCategory
            {
                CategoryId = source.CategoryId,
                Name = source.Name
            };
        }
        public static CouponCategoryModel CreateFromForCategories(this Models.DomainModels.CouponCategory source, bool isSelected)
        {
            return new CouponCategoryModel
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                IsSelected = isSelected
            };
        }
        public static CouponCodeModel CreateFrom(this Models.DomainModels.CouponCode source)
        {
            return new CouponCodeModel
            {
                Code = source.Code,
                CampaignId = source.CampaignId ?? 0,
                CodeId = source.CodeId,
                IsTaken = source.IsTaken == null ? false : true,
                UserId = source.UserId,
                UserName = source.AspNetUser == null ? "" : source.AspNetUser.FullName,
                TakenDateTime = source.TakenDateTime

            };
        }
        public static DiscountVoucher CreateFromDiscountVoucher(this Models.Common.Coupons source)
        {
            return new DiscountVoucher
            {
                CouponId = source.CouponId,
                Name = source.CouponTitle
            };
        }
    }
}