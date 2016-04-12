
using System.Collections.Generic;
using SMD.Models.ResponseModels;
using System;

namespace SMD.MIS.Areas.Api.Models
{
    public class EducationDropdown
    {
        /// <summary>
        /// Education id
        /// </summary>
        public long EducationId { get; set; }

        /// <summary>
        /// Education Title
        /// </summary>
        public string EducationName { get; set; }
    }
    public class Education
    {
        /// <summary>
        /// Education id
        /// </summary>
        public long EducationId { get; set; }

        /// <summary>
        /// Education Title
        /// </summary>
        public string Title { get; set; }
    }
    public class CouponCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

    }
    public class EducationResponse : BaseApiResponse
    {
        public IEnumerable<Education> Educations { get; set; }
    }
    public class CouponCategoryResponse : BaseApiResponse
    {
        public IEnumerable<CouponCategory> CouponCategories { get; set; }
    }

    public class CouponCategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}