using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Invoice Detail Repository Interface 
    /// </summary>
    public interface IInvoiceDetailRepository : IBaseRepository<InvoiceDetail, int>
    {
        /// <summary>
        /// Get Details by Id 
        /// </summary>
        IEnumerable<InvoiceDetail> GetInvoiceDetailByInvoiceId(long invoiceId);   
    }
}
