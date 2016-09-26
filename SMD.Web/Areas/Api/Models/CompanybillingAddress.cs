using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CompanybillingAddress
    {
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingState { get; set; }
        public Nullable<int> BillingCountryId { get; set; }
        public Nullable<int> BillingCityId { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string BillingCity { get; set; }

     
    }
}