using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
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
        private static ICompanyService companyService { get; set; }

        [Dependency]
        private static IInvoiceRepository invoiceRepository { get; set; }

        [Dependency]
        private static IInvoiceDetailRepository invoiceDetailRepository { get; set; }


        [Dependency]
        private static ICouponService couponService { get; set; }

        public void ProcessRequest(HttpContext context)
        {

             companyService = UnityConfig.UnityContainer.Resolve<ICompanyService>();
             invoiceRepository = UnityConfig.UnityContainer.Resolve<IInvoiceRepository>();
             invoiceDetailRepository = UnityConfig.UnityContainer.Resolve<IInvoiceDetailRepository>();
             couponService = UnityConfig.UnityContainer.Resolve<ICouponService>();

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");


            var json = new StreamReader(context.Request.InputStream).ReadToEnd();

            var stripeEvent = StripeEventUtility.ParseEvent(json);



            //var eventService = new StripeEventService();
            //StripeEvent response = eventService.Get(stripeEvent.Id);


            var subscriptionService = new StripeSubscriptionService();

            string StripeCustoemrID, StripeSubscriptionID;

            StripeInvoice inv = null;

            Company comp = null;


            switch (stripeEvent.Type)
            {
                case StripeEvents.ChargeRefunded:  // all of the types available are listed in StripeEvents
                    var stripeCharge = Stripe.Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                    break;

                case StripeEvents.InvoiceCreated:  // ts
                    {
                        inv = Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                        StripeCustoemrID = inv.CustomerId;
                        StripeSubscriptionID = inv.SubscriptionId;


                       


                        break;
                    }

                case StripeEvents.InvoicePaymentSucceeded :
                    inv = Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                    //handle only in case of subscription.
                    if (inv.SubscriptionId != "")
                    {
                        StripeCustoemrID = inv.CustomerId;
                        StripeSubscriptionID = inv.SubscriptionId;

                        //generate invoice and send email

                        comp = companyService.GetCompanyByStripeCustomerId(StripeCustoemrID);
                        //generate invoice

                        //send the invoice subscription event email
                        #region Add Invoice

                        // Add invoice data
                        var invoice = new Invoice
                        {
                            //Country = comp.count.ToString(),
                            Total = (double)inv.Total,
                            NetTotal = (double)inv.Subtotal,
                            InvoiceDate = DateTime.Now,
                            //InvoiceDueDate = DateTime.Now.AddDays(7),
                            Address1 = comp.BillingAddressLine1,

                            CompanyId = comp.CompanyId,
                            CompanyName = comp.CompanyName,
                            StripeInvoiceId = inv.Id,
                            StripeReceiptNo = inv.ReceiptNumber

                        };
                        invoiceRepository.Add(invoice);

                        #endregion
                        #region Add Invoice Detail

                        // Add Invoice Detail Data 
                        var invoiceDetail = new InvoiceDetail
                        {
                            InvoiceId = invoice.InvoiceId,
                            SqId = null,
                            PQID = null,
                            ProductId = 8,
                            ItemName = inv.StripeInvoiceLineItems.Data[0].Description,
                            ItemAmount = (double)inv.Charge.Amount,
                            ItemTax = (double)inv.Tax,
                            ItemDescription = inv.StripeInvoiceLineItems.Data[0].Description,
                            ItemGrossAmount = (double)inv.StripeInvoiceLineItems.Data[0].Amount,
                            CouponID = null,

                        };
                        invoiceDetailRepository.Add(invoiceDetail);
                        invoiceDetailRepository.SaveChanges();

                        #endregion


                    }

                    
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
                            comp = companyService.GetCompanyByStripeCustomerId(StripeCustoemrID);
                            if (comp != null)
                            {
                                comp.StripeSubscriptionId = null;
                                comp.StripeSubscriptionStatus = "canceled";

                                companyService.UpdateCompany(comp, null);

                                couponService.PauseAllCoupons(comp.CompanyId);

                            }

                        }

                     
                    }

                    //send payment failed email.


                    break;

                case StripeEvents.CustomerSubscriptionDeleted:

                    inv= Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                      StripeCustoemrID = inv.CustomerId;
                    StripeSubscriptionID = inv.SubscriptionId;
                    comp = companyService.GetCompanyByStripeCustomerId(StripeCustoemrID);
                    if (comp != null)
                    {
                        comp.StripeSubscriptionId = null;
                        comp.StripeSubscriptionStatus = "deleted";

                        companyService.UpdateCompany(comp, null);

                        couponService.PauseAllCoupons(comp.CompanyId);

                    }

                    break;
                default:
                    //do nothing
                    var x = "";
                    break;

            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("OK");



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