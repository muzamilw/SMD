using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CouponRatingReviewApiModel
    {
        public long CouponReviewId { get; set; }
        public Nullable<int> StarRating { get; set; }
        public string Review { get; set; }
        public Nullable<System.DateTime> RatingDateTime { get; set; }
        public string UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> Status { get; set; }
        public string ReviewImage1 { get; set; }
        public string ReviewImage2 { get; set; }
        public string Reviewimage3 { get; set; }

       
    }
}