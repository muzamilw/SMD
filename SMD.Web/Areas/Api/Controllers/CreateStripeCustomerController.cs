using System.Threading.Tasks;
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
    /// Create Stripe Customer Api Controller 
    /// </summary>
    public class CreateStripeCustomerController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;
        private readonly IStripeService stripeService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateStripeCustomerController(IWebApiUserService webApiUserService, IStripeService stripeService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.stripeService = stripeService;
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
                await stripeService.CreateCustomer(request);
            }
        }
        
        #endregion
    }
}