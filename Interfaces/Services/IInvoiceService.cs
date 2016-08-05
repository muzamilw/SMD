
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Invoice Service Interface
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Search Invoices for LV
        /// </summary>
        InvoiceSearchRequestResponse SearchInvoices(InvoiceSearchRequest request);
    }
}
