using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
   public class CompanySubscription
    {
        public string StripeCustomerId { get; set; }
        public string StripeSubscriptionId { get; set; }
        public string StripeSubscriptionStatus { get; set; }


    }
}
