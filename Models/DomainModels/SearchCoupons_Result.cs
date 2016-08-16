﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class SearchCoupons_Result
    {
        public long CouponId { get; set; }
        public string CouponTitle { get; set; }
        public string CouponImage1 { get; set; }
        public string LogoUrl { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public Nullable<double> SwapCost { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CouponActiveMonth { get; set; }
        public Nullable<int> CouponActiveYear { get; set; }

        public Nullable<int> TotalItems { get; set; }
        
    }
}