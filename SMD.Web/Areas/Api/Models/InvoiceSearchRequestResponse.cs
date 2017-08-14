using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Response Web Model 
    /// </summary>
    public class InvoiceSearchRequestResponse
    {
        #region Public
        /// <summary>
        ///  Invoices List
        /// </summary>
        public IEnumerable<Invoice> Invoices { get; set; }

        /// <summary>
        /// Total Count of Invoices
        /// </summary>
        public int TotalCount { get; set; }
        #endregion
    }
}