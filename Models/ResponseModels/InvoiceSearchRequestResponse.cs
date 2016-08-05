using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Resposne Model | Domain 
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
