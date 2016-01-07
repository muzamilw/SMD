namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Stripe Charge Customer Request Model 
    /// </summary>
    public class StripeChargeCustomerRequest
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Double
        /// </summary>
        public int? Amount { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ad Campaign Id
        /// </summary>
        public long? AdCampaignId { get; set; }

        /// <summary>
        /// Survey question Id
        /// </summary>
        public long? SqId { get; set; }
    }
}
