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
        private readonly IAdvertService advertService;
        private readonly ISurveyQuestionService surveyQuestionService;

        /// <summary>
        /// Create Charge With Customer Id
        /// </summary>
        private bool CreateChargeWithCustomerId(StripeChargeCustomerRequest request)
        {
            // Get Item Id
            long itemId = request.AdCampaignId.HasValue
                ? request.AdCampaignId.Value
                : request.SqId.HasValue ? request.SqId.Value : long.MinValue;

            if (itemId == long.MinValue)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            // To check whether it is adCampaign or survey 
            bool isAdCampaignRequest = request.AdCampaignId.HasValue;
            string stripeCustomerId;
            if (isAdCampaignRequest)
            {
                // If Item is Ad Campaign 
                var adCampaign = advertService.GetAdCampaignById(itemId);
                var user = webApiUserService.GetUserByUserId(adCampaign.UserId);
                stripeCustomerId = user.Company.StripeCustomerId;
            }
            else
            {
                // If Item is Survey Question 
                var survey = surveyQuestionService.GetSurveyQuestionById(itemId);
                var user = webApiUserService.GetUserByUserId(survey.UserId);
                stripeCustomerId = user.Company.StripeCustomerId;
            }

            // Check if Stripe Customer Id Exists then use that to Create Charge
            if (string.IsNullOrEmpty(stripeCustomerId))
            {
                throw new SMDException(LanguageResources.Stripe_CustomerNotFound);
            }

            var resposne = stripeService.ChargeCustomer(request.Amount, stripeCustomerId);
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
        public ChargeCustomerController(IWebApiUserService webApiUserService, IStripeService stripeService, IAdvertService advertService, ISurveyQuestionService surveyQuestionService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }
            if (stripeService == null) throw new ArgumentNullException("stripeService");

            this.webApiUserService = webApiUserService;
            this.stripeService = stripeService;
            this.advertService = advertService;
            this.surveyQuestionService = surveyQuestionService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Charge Customer
        /// </summary>
        [ApiException]
        public bool Post(StripeChargeCustomerRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            
            // Charge Customer
           return CreateChargeWithCustomerId(request);
        }
        
        #endregion
    }
}