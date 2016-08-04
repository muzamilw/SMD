using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Invoice Repository Interface 
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice, int>
    {
        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        IEnumerable<Invoice> SearchInvoices(InvoiceSearchRequest request, out int rowCount);
    }
}
