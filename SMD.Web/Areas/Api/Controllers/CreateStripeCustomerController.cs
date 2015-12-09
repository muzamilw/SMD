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
    /// Create Stripe Customer Api Controller 
    /// </summary>
    public class CreateStripeCustomerController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;

        /// <summary>
        /// Create Charge With Token
        /// </summary>
        private async Task<string> CreateCustomer(StripeChargeCustomerRequest request)
        {
            // Create the charge on Stripe's servers - this will charge the user's card
            var stripeSourceOptions = new StripeSourceOptions { TokenId = request.Token };
            
            // Create Customer For Later Use
            var myCustomer = new StripeCustomerCreateOptions
            {
                Email = request.Email,
                Source = stripeSourceOptions
            };
            var customerService = new StripeCustomerService();
            try
            {
                var stripeCustomer = customerService.Create(myCustomer);
                // Save Customer For Later Use
                await webApiUserService.SaveStripeCustomerId(stripeCustomer.Id);
                // Return Customer Id
                return stripeCustomer.Id;
            }
            catch (Exception exp)
            {
                throw new SMDException(exp.Message);
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateStripeCustomerController(IWebApiUserService webApiUserService)
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
        public async Task Post(StripeChargeCustomerRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            string customerId = await webApiUserService.GetStripeCustomerId();
            // Check if Stripe Customer Id Exists then use that to Create Charge
            if (string.IsNullOrEmpty(customerId))
            {
                await CreateCustomer(request);
            }
        }
        
        #endregion
    }
}