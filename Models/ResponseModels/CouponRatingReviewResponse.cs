using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{


    public class CouponRatingReviewOverallResponse
    {

        public Nullable<long> CouponId { get; set; }

        public double? OverAllStarRating { get; set; }


        public List<CouponRatingReviewResponse> CouponRatingReviewResponses { get; set; }
    }


    public class CouponRatingReviewResponse
    {
        public long CouponReviewId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public Nullable<double> StarRating { get; set; }
        public string Review { get; set; }
        public Nullable<DateTime> RatingDateTime { get; set; }
        public string UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> Status { get; set; }
        public string ReviewImage1 { get; set; }
        public string ReviewImage2 { get; set; }
        public string Reviewimage3 { get; set; }

        public string FullName { get; set; }

        public string CouponTitle { get; set; }

        public string ProfileImage { get; set; }
        
    }
    public class CouponRatingReviewResponseModel
    {
        public List<CouponRatingReviewResponse> CouponsReview { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }

    }

}
