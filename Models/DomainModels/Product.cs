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
        public string ProductCode { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> SetupPrice { get; set; }
        public Nullable<double> ClickFeePercentage { get; set; }
        public Nullable<double> AgeClausePrice { get; set; }
        public Nullable<double> AffiliatePercentage { get; set; }
        public Nullable<double> GenderClausePrice { get; set; }
        public Nullable<double> LocationClausePrice { get; set; }
        public Nullable<double> OtherClausePrice { get; set; }
        public Nullable<double> ProfessionClausePrice { get; set; }
        public Nullable<double> EducationClausePrice { get; set; }

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
