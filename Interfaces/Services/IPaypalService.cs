using SMD.Models.RequestModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Paypal Service
    /// </summary>
    public interface IPaypalService
    {
        /// <summary>
        /// Sends Payment over Paypal implicitly
        /// </summary>
        void MakeAdaptiveImplicitPayment(MakePaypalPaymentRequest request);
    }
}
