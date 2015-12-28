using System.Linq;
using System.Threading.Tasks;
using SMD.ExceptionHandling;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;
using Stripe;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Charge Customer Api Controller 
    /// </summary>
    public class ChargeCustomerController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;

        /// <summary>
        /// Create Charge With Customer Id
        /// </summary>
        private static bool CreateChargeWithCustomerId(StripeChargeCustomerRequest request, string customerId)
        {
            // Verify If Credit Card is not expired
            var customerService = new StripeCustomerService();
            var customer = customerService.Get(customerId);
            if (customer == null)
            {
                throw new SMDException(LanguageResources.Stripe_CustomerNotFound);
            }

            // If Card has been expired then skip payment
            if (customer.SourceList != null && customer.SourceList.Data != null && customer.DefaultSourceId != null)
            {
                var defaultStripeCard = customer.SourceList.Data.FirstOrDefault(card => card.Id == customer.DefaultSourceId);
                if (defaultStripeCard != null && (Convert.ToInt32(defaultStripeCard.ExpirationMonth) < DateTime.Now.Month ||
                    Convert.ToInt32(defaultStripeCard.ExpirationYear) < DateTime.Now.Year))
                {
                    throw new SMDException(LanguageResources.Stripe_CardExpired);
                }
            }
            
            var stripeChargeCreateOptions = new StripeChargeCreateOptions
            {
                CustomerId = customerId,
                Amount = request.Amount,
                Currency = "usd",
                Capture = true
                // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            };
            var chargeService = new StripeChargeService();
            var resposne=  chargeService.Create(stripeChargeCreateOptions);
            if (resposne.Status == "succeeded")
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ChargeCustomerController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Charge Customer
        /// </summary>
        [ApiException]
        public async Task<bool> Post(StripeChargeCustomerRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            string stripeCustomerId = await webApiUserService.GetStripeCustomerIdByEmail(request.Email);
            // Check if Stripe Customer Id Exists then use that to Create Charge
            if (string.IsNullOrEmpty(stripeCustomerId))
            {
                throw new SMDException(LanguageResources.Stripe_CustomerNotFound);
            }

            // Charge Customer
           return CreateChargeWithCustomerId(request, stripeCustomerId);
        }
        
        #endregion
    }
}