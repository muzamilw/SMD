
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Get Ads For API
    /// </summary>
    public class GetAdsApiRequest:GetPagedListRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
