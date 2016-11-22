﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CouponRatingReviewRequest
    {
       
        public Nullable<long> CouponId { get; set; }
        public Nullable<int> StarRating { get; set; }
        public string Review { get; set; }
               public string UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
       
        public string ReviewImage1 { get; set; }
        public string ReviewImage2 { get; set; }
        public string Reviewimage3 { get; set; }

        public string ReviewImage1ext { get; set; }
        public string ReviewImage2ext { get; set; }
        public string Reviewimage3ext { get; set; }

    
    }

   
}