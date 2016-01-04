using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using Stripe;

namespace SMD.Implementation.Services
{
    public class StripeService : IStripeService
    {
        #region Private
        /// <summary>
        /// App Manager
        /// </summary>
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        #region Func
        /// <summary>
        /// Save Stripe Customer
        /// </summary>
        public async Task SaveStripeCustomerId(string customerId)
        {
            User user = await UserManager.FindByIdAsync(UserManager.LoggedInUserId);
            if (user == null)
            {
                throw new SMDException(LanguageResources.WebApiUserService_LoginInfoNotFound);
            }

            user.StripeCustomerId = customerId;
            await UserManager.UpdateAsync(user);
        }

        #endregion
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public StripeService()
        {
            
        }
        #endregion
        #region Public

        /// <summary>
        /// Make Payment with Stripe 
        /// </summary>
        public string ChargeCustomer(int? amount, string customerId)
        {
            // Verify If Credit Card is not expired
            var customerService = new StripeCustomerService();
            var customer = customerService.Get(customerId);
            if (customer == null)
            {
                throw new SMDException("Customrt Not Found!");
            }

            // If Card has been expired then skip payment
            if (customer.SourceList != null && customer.SourceList.Data != null && customer.DefaultSourceId != null)
            {
                var defaultStripeCard = customer.SourceList.Data.FirstOrDefault(card => card.Id == customer.DefaultSourceId);
                if (defaultStripeCard != null && (Convert.ToInt32(defaultStripeCard.ExpirationMonth) < DateTime.Now.Month ||
                    Convert.ToInt32(defaultStripeCard.ExpirationYear) < DateTime.Now.Year))
                {
                    throw new SMDException("Card Expired!");
                }
            }

            var stripeChargeCreateOptions = new StripeChargeCreateOptions
            {
                CustomerId = customerId,
                Amount = amount,
                Currency = "usd",
                Capture = true
                // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            };
            var chargeService = new StripeChargeService();
            var resposne = chargeService.Create(stripeChargeCreateOptions);
            if (resposne.Status == "succeeded")
            {
                return resposne.BalanceTransactionId;
            }
            return "failed";  
        }


        /// <summary>
        /// Create Charge With Token
        /// </summary>
        public async Task<string> CreateCustomer(StripeChargeCustomerRequest request)
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
                await SaveStripeCustomerId(stripeCustomer.Id);
                // Return Customer Id
                return stripeCustomer.Id;
            }
            catch (Exception exp)
            {
                throw new SMDException(exp.Message);
            }
        }
        #endregion
    }
}
