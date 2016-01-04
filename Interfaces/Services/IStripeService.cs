
using System.Threading.Tasks;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Stripe Service Interface
    /// </summary>
    public interface IStripeService
    {
        /// <summary>
        /// Make Payment with Stripe 
        /// </summary>
        string ChargeCustomer(int? amount, string customerId);

        /// <summary>
        /// Create Charge With Token
        /// </summary>
        Task<string> CreateCustomer(StripeChargeCustomerRequest request);
    }
}
