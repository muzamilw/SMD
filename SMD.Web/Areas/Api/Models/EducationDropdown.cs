﻿
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

        public string ImagePath { get; set; }

        public int SortOrder { get; set; }

    }
    public partial class CouponCategories
    {
        public long Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<long> CouponId { get; set; }

    }

    public class CouponPriceOption
    {
        public long CouponPriceOptionId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public string Description { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public string VoucherCode { get; set; }
        public string OptionUrl { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
        public string VoucherCode2 { get; set; }
        public string VoucherCode3 { get; set; }
        public string VoucherCode4 { get; set; }
        public string URL { get; set; }
        public Nullable<double> ist4dayPrice { get; set; }
        public Nullable<double> day5Price { get; set; }
        public Nullable<double> day6Price { get; set; }
        public Nullable<double> day7Price { get; set; }
        public Nullable<double> ist4dayPer { get; set; }
        public Nullable<double> day5Per { get; set; }
        public Nullable<double> day6Per { get; set; }
        public Nullable<double> day7Per { get; set; }
    }


    public class CouponCodeModel
    {
        public long CodeId { get; set; }
        public long CampaignId { get; set; }
        public string Code { get; set; }
        public bool IsTaken { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? TakenDateTime { get; set; }

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
    public class DiscountVoucher
    {
        public long CouponId { get; set; }
        public string Name { get; set; }

    }

    public class CouponCodeQuantityModel
    {
        public int CouponQuantity { get; set; }
        public IEnumerable<CouponCodeModel> CouponList { get; set; }
    }
}