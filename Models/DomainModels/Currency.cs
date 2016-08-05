using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<double> SMDCreditRatio { get; set; }
        public string CurrencySymbol { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
