using System.Collections.Generic;
using System.Web;
using PayPal.AdaptivePayments.Model;
using PayPal.Manager;
using SMD.ExceptionHandling;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using PayPal.AdaptivePayments;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Paypal Service
    /// </summary>
    public class PaypalService : IPaypalService
    {
        /// <summary>
        /// Makes implicit Adaptive payment
        /// </summary>
        public void MakeAdaptiveImplicitPayment(MakePaypalPaymentRequest request)
        {
            // # PayRequest
            // The code for the language in which errors are returned
            var envelopeRequest = new RequestEnvelope {errorLanguage = "en_US"};

            var listReceiver = new List<Receiver>();

            // Amount to be credited to the receiver's account
            var receive = new Receiver(request.Amount) {email = request.RecieverEmail};

            // A receiver's email address
            listReceiver.Add(receive);
            var listOfReceivers = new ReceiverList(listReceiver);

            string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                             "/Api/";

            string returnUrl = baseUrl + "AdaptivePaypalPayment";
            string cancelUrl = baseUrl + "AdaptivePaypalPayment?cancel=true";
            var sdkConfig = ConfigManager.Instance.GetProperties();
            var requestPay = new PayRequest(envelopeRequest, "PAY", cancelUrl, sdkConfig["currency"], listOfReceivers, returnUrl)
                             {
                                 senderEmail = request.SenderEmail,
                                 ipnNotificationUrl = "http://replaceIpnUrl.com"
                             };

            var adaptivePaymentsService = new AdaptivePaymentsService(sdkConfig);
            var payResponse = adaptivePaymentsService.Pay(requestPay);
            List<ErrorData> errorData = payResponse.error;
            if (errorData.Count > 0)
            {
                throw new SMDException(errorData[0].message);
            }
        }
    }
}
