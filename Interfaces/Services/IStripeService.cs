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
        string ChargeCustomer(int? amount, string customerStripeId);

        /// <summary>
        /// Create Charge With Token
        /// </summary>
        Task<string> CreateCustomer(StripeChargeCustomerRequest request);
        Task<string> UpdateCustomer(StripeChargeCustomerRequest request, string CustomerId);
    }
}
