using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CurrencyApprov
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<double> SMDCreditRatio { get; set; }
        public string CurrencySymbol { get; set; }
    }
}