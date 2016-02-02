using System.Collections.Generic;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Make Paypal Payment Request Model 
    /// </summary>
    public class MakePaypalPaymentRequest
    {
        /// <summary>
        /// Sender's Email
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Reciever's Email 
        /// </summary>
        public List<string> RecieverEmails { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
