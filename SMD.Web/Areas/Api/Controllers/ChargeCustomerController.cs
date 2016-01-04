using System.Threading.Tasks;
using SMD.ExceptionHandling;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Charge Customer Api Controller 
    /// </summary>
    public class ChargeCustomerController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;
        private readonly IStripeService stripeService;

        /// <summary>
        /// Create Charge With Customer Id
        /// </summary>
        private bool CreateChargeWithCustomerId(StripeChargeCustomerRequest request, string customerId)
        {
            var resposne = stripeService.ChargeCustomer(request.Amount, customerId);
            if (resposne != "failed")
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
        public ChargeCustomerController(IWebApiUserService webApiUserService, IStripeService stripeService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }
            if (stripeService == null) throw new ArgumentNullException("stripeService");

            this.webApiUserService = webApiUserService;
            this.stripeService = stripeService;
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