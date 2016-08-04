
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Service interface 
    /// </summary>
    public interface IInvoiceDetailService
    {
        /// <summary>
        /// Get Detail by Id 
        /// </summary>
        IEnumerable<InvoiceDetail> GetInvoiceDetailByInvoiceId(long invoiceId);
    }
}
