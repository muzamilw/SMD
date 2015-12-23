using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Domain Model Invoice
    /// </summary>
    public class Invoice
    {
        #region Public

        public long InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime InvoiceDueDate { get; set; }

        public string UserId { get; set; }

        public double? TaxPercentage { get; set; }

        public double Total { get; set; }

        public double NetTotal { get; set; }

        public double? TaxValue { get; set; }

        public string CompanyName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string CreditCardRef { get; set; }
        #endregion
        #region Navigational
        public virtual User User { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        #endregion
    }
}
