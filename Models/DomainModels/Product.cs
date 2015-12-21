using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? CountryId { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public string ProductName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> SetupPrice { get; set; }
        public Nullable<double> ClickFeePercentage { get; set; }
        public Nullable<double> ClausePrice { get; set; }
        public Nullable<double> AffiliatePercentage { get; set; }

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
