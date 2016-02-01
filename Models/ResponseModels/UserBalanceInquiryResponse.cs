namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// User Balance Inquiry Response 
    /// </summary>
    public class UserBalanceInquiryResponse : BaseApiResponse
    {
        /// <summary>
        /// Balance
        /// </summary>
        public double? Balance { get; set; }
    }
}
