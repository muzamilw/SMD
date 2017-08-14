using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Get Ads,Surveys,Questions | API Reposne | Domain
    /// </summary>
    public class ProductViewResponse : BaseApiResponse
    {
        /// <summary>
        /// Products
        /// </summary>
        public IEnumerable<ProductView> Products { get; set; }

        /// <summary>
        /// Total number of Products
        /// </summary>
        public int TotalCount { get; set; }

        public string ErrorMessage { get; set; }
    }
}