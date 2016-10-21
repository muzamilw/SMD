using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
    public class StripeSubscriptionResponse
    {
        
        public decimal? ApplicationFeePercent { get; set; }
        
        public bool CancelAtPeriodEnd { get; set; }
      
        public DateTime? CanceledAt { get; set; }
       
        public string Customer { get; set; }
        public string CustomerId { get; set; }
      
        public DateTime? EndedAt { get; set; }
      
        public Dictionary<string, string> Metadata { get; set; }
       
               
        public DateTime? PeriodEnd { get; set; }
      
        public DateTime? PeriodStart { get; set; }
       
        public int Quantity { get; set; }
       
        public DateTime? Start { get; set; }
        
        public string Status { get; set; }
       
     
      
        public string StripePlan { get; set; }
        
        public decimal? TaxPercent { get; set; }
      
    }
}
