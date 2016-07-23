using SMD.Models.Common;
using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Get Ads,Surveys,Questions | API Reposne | Domain
    /// </summary>
    public class SearchCouponsResponse : BaseApiResponse
    {
        /// <summary>
        /// List of Products
        /// </summary>
        public IEnumerable<SearchCoupons_Result> Coupons { get; set; }

        /// <summary>
        /// Total number of Products
        /// </summary>
        public int TotalCount { get; set; }
    }
}
