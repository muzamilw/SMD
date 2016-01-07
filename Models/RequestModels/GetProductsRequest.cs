
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Get Ads, Surveys, Questions
    /// </summary>
    public class GetProductsRequest : GetPagedListRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
