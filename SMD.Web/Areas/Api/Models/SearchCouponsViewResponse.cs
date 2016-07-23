using SMD.Models.DomainModels;
using SMD.Models.Common;
using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Get Ads,Surveys,Questions | API Reposne | Domain
    /// </summary>
    public class SearchCouponsViewResponse : BaseApiResponse
    {
        /// <summary>
        /// List of Products
        /// </summary>
        public IEnumerable<Coupons> Coupons { get; set; }

        /// <summary>
        /// Total number of Products
        /// </summary>
        public int TotalCount { get; set; }

        public string ErrorMessage { get; set; }
    }
}
