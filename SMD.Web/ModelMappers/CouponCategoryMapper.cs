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
                Status= true
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
                code = source.Code
            };
        }
    }
}