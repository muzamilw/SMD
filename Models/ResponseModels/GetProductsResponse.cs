using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Get Ads,Surveys,Questions | API Reposne | Domain
    /// </summary>
    public class GetProductsResponse : BaseApiResponse
    {
        /// <summary>
        /// List of Products
        /// </summary>
        public IEnumerable<GetProducts_Result> Products { get; set; }

        /// <summary>
        /// Total number of Products
        /// </summary>
        public int TotalCount { get; set; }
    }
}
