using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SMD.MIS.StripeWebhooks
{
    /// <summary>
    /// Summary description for StripeHandler
    /// </summary>
    public class StripeHandler : IHttpHandler
    {

        [Dependency]
        private static IAccountService accountService { get; set; }

        public void ProcessRequest(HttpContext context)
        {

             accountService = UnityConfig.UnityContainer.Resolve<IAccountService>();

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");


            var json = new StreamReader(context.Request.InputStream).ReadToEnd();

            var stripeEvent = StripeEventUtility.ParseEvent(json);



            //var eventService = new StripeEventService();
            //StripeEvent response = eventService.Get(stripeEvent.Id);


            var subscriptionService = new StripeSubscriptionService();

            string StripeCustoemrID, StripeSubscriptionID;

            StripeInvoice inv = null;



            switch (stripeEvent.Type)
            {
                case StripeEvents.ChargeRefunded:  // all of the types available are listed in StripeEvents
                    var stripeCharge = Stripe.Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                    break;

                case StripeEvents.InvoiceCreated:  // ts
                      inv = Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                    StripeCustoemrID = inv.CustomerId;
                    StripeSubscriptionID = inv.SubscriptionId;

                    //send the invoice subscription event email
                    break;

                case StripeEvents.InvoicePaymentSucceeded :
                    inv = Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                    StripeCustoemrID = inv.CustomerId;
                    StripeSubscriptionID = inv.SubscriptionId;



                    
                    break;
                case StripeEvents.InvoicePaymentFailed:
                    inv= Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                      StripeCustoemrID = inv.CustomerId;
                    StripeSubscriptionID = inv.SubscriptionId;
                    if ( inv.AttemptCount == 3)// last attempt
                    {
                        
                        var subs = subscriptionService.Get(StripeCustoemrID, StripeSubscriptionID);
                        if ( subs != null && subs.Status== "canceled")
                        {

                        }

                     
                    }


                    break;

                case StripeEvents.CustomerSubscriptionDeleted:

                    break;

            }



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}