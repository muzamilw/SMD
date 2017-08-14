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
        public System.DateTime InvoiceDate { get; set; }
        public System.DateTime InvoiceDueDate { get; set; }
        public Nullable<double> TaxPercentage { get; set; }
        public double Total { get; set; }
        public double NetTotal { get; set; }
        public Nullable<double> TaxValue { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CreditCardRef { get; set; }



           public string StripeReceiptNo { get; set; }

           public string StripeInvoiceId { get; set; }


        public Nullable<int> CompanyId { get; set; }

    
       
        #endregion
        #region Navigational
        public virtual Company Company { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        #endregion
    }
}
