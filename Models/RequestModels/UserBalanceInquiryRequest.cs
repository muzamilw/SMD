namespace SMD.Models.RequestModels
{
    /// <summary>
    /// User Balance Inquiry Request Model 
    /// </summary>
    public class UserTransactionInquiryRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int CompanyId { get; set; }
    }



    public class UserBalanceInquiryRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
