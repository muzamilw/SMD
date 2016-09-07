using SMD.Models.DomainModels;
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Approve Survey Request Model 
    /// </summary>
    public class ApproveProfileQuestionRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Survey Question Id
        /// </summary>
        public long PQID { get; set; }

        /// <summary>
        /// Approval Amount
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Stripe Response
        /// </summary>
        public string StripeResponse { get; set; }

        public ProfileQuestion objQuestion { get; set; }

     
    }
}
