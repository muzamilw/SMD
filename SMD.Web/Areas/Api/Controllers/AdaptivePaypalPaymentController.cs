using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Adaptive Paypal Payment Api Controller 
    /// </summary>
    public class AdaptivePaypalPaymentController : ApiController
    {

        #region Private

        private readonly IPaypalService paypalService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdaptivePaypalPaymentController(IPaypalService paypalService)
        {
            if (paypalService == null)
            {
                throw new ArgumentNullException("paypalService");
            }

            this.paypalService = paypalService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Paypal Payment Redirects
        /// </summary>
        public void Get()
        {
            
        }
        
        /// <summary>
        /// Make Paypal Payment
        /// </summary>
        public void Post(MakePaypalPaymentRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            paypalService.MakeAdaptiveImplicitPayment(request);
        }
        #endregion
    }
}