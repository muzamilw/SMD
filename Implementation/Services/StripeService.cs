using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using Stripe;
using SMD.Models.ResponseModels;
using System.Configuration;

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

            user.Company.StripeCustomerId = customerId;
            await UserManager.UpdateAsync(user);
        }

        #endregion
        #endregion
        #region Constructor

        #endregion
        #region Public

        /// <summary>
        /// Make Payment with Stripe 
        /// </summary>
        public string ChargeCustomer(int? amount, string customerStripeId)
        {
            if (amount <= 0)
            {
                //throw new ArgumentException("Amount");
                return "Failed, Amount is not sufficient!"; 
            }

            if (string.IsNullOrEmpty(customerStripeId))
            {
                //throw new ArgumentException("customerStripeId");
                return "Failed, Account not configured!";  
            }

            // Verify If Credit Card is not expired
            var customerService = new StripeCustomerService();
            var customer = customerService.Get(customerStripeId);
            if (customer == null)
            {
               // throw new SMDException("Customer Not Found!");
                return "Failed, Customer Not Found!";  
            }

            // If Card has been expired then skip payment
            if (customer.SourceList != null && customer.SourceList.Data != null && customer.DefaultSourceId != null)
            {
                var defaultStripeCard = customer.SourceList.Data.FirstOrDefault(card => card.Id == customer.DefaultSourceId);
                if (defaultStripeCard != null && Convert.ToInt32(defaultStripeCard.ExpirationYear) < DateTime.Now.Year
                 && (Convert.ToInt32(defaultStripeCard.ExpirationYear) == DateTime.Now.Year && Convert.ToInt32(defaultStripeCard.ExpirationMonth) < DateTime.Now.Month))
                {
                    //throw new SMDException("Card Expired!");
                    return "Failed, Card Expired!";  
                }
            }

            var stripeChargeCreateOptions = new StripeChargeCreateOptions
            {
                CustomerId = customerStripeId,
                Amount = amount *100,
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
            return "Failed";  
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

        /// <summary>
        /// uPDATE Charge With Token
        /// </summary>
        public async Task<string> UpdateCustomer(StripeChargeCustomerRequest request, string CustomerId)
        {
            // Create the charge on Stripe's servers - this will charge the user's card
            var stripeSourceOptions = new StripeSourceOptions { TokenId = request.Token };

            // Create Customer For Later Use
            var myCustomer = new StripeCustomerUpdateOptions
            {
                Email = request.Email,
                Source = stripeSourceOptions,
                
            };
            var customerService = new StripeCustomerService();
            try
            {
                var stripeCustomer = customerService.Update(CustomerId, myCustomer);
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





        public StripeSubscriptionResponse CreateCustomerSubscription(string StripeCustomerId)
        {

            //StripeConfiguration.SetApiKey("[your api key here]");

          
            var customerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = customerService.Get(StripeCustomerId);

            StripeSubscriptionService subscriptionSvc = new StripeSubscriptionService();
            var sub = subscriptionSvc.Create(StripeCustomerId, ConfigurationManager.AppSettings["CouponSubscriptionPlan"]); //use the PlanId you configured in the Stripe Portal to create a subscription
            //Do something here to give your customer what they are paying for
            //CHEERS!

            

            if (sub != null)
            {
                return new StripeSubscriptionResponse {SubscriptionId = sub.Id,  
                    ApplicationFeePercent = sub.ApplicationFeePercent.HasValue? sub.ApplicationFeePercent.Value : 0, 
                    CancelAtPeriodEnd = sub.CancelAtPeriodEnd, 
                    Customer = sub.Customer != null ? sub.Customer.Email : "",
                    CustomerId = sub.CustomerId, 
                    CanceledAt = sub.CanceledAt.HasValue ? sub.CanceledAt.Value : DateTime.Now, 
                    EndedAt = sub.EndedAt,
                    Metadata = sub.Metadata,
                    PeriodEnd = sub.PeriodEnd,
                    PeriodStart = sub.PeriodStart,
                    Quantity = sub.Quantity, 
                    Start = sub.Start, 
                    Status = sub.Status, 
                    StripePlan = sub.StripePlan.Name, 
                    TaxPercent = sub.TaxPercent };
            }
            else
                return null;

        }


        public StripeSubscriptionResponse GetCustomerSubscription(string StripeSubscriptionId, string StripeCustomerId)
    {
         StripeSubscriptionService subscriptionSvc = new StripeSubscriptionService();
         var sub = subscriptionSvc.Get(StripeCustomerId,StripeSubscriptionId); //use the PlanId you configured in the Stripe Portal to create a subscription

         if (sub != null)
         {
             return new StripeSubscriptionResponse
             {
                 SubscriptionId = sub.Id,
                 ApplicationFeePercent = sub.ApplicationFeePercent.HasValue ? sub.ApplicationFeePercent.Value : 0,
                 CancelAtPeriodEnd = sub.CancelAtPeriodEnd,
                 Customer = sub.Customer != null ? sub.Customer.Email : "",
                 CustomerId = sub.CustomerId,
                 CanceledAt = sub.CanceledAt.HasValue ? sub.CanceledAt.Value : DateTime.Now,
                 EndedAt = sub.EndedAt,
                 Metadata = sub.Metadata,
                 PeriodEnd = sub.PeriodEnd,
                 PeriodStart = sub.PeriodStart,
                 Quantity = sub.Quantity,
                 Start = sub.Start,
                 Status = sub.Status,
                 StripePlan = sub.StripePlan.Name,
                 TaxPercent = sub.TaxPercent
             };
         }
         else
             return null;



    }
        #endregion
    }
}
