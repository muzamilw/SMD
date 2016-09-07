using System.Collections.Generic;
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
    public class StatementInquiryResponse : BaseApiResponse
    {
        /// <summary>
        /// Balance
        /// </summary>
        public double? Balance { get; set; }
        public List<StatementTrasaction> Transactions { get; set; }

    }
    public class StatementTrasaction
    {
        /// <summary>
        /// Balance
        /// </summary>
        /// 
        public string description { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public string TransactionDate { get; set; }
    }
}
