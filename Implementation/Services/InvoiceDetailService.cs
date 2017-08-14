using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Invoice Service Model 
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        #region Private

        private readonly IInvoiceRepository invoiceRepository;
        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Search Invoices for LV
        /// </summary>
        public InvoiceSearchRequestResponse SearchInvoices(InvoiceSearchRequest request)
        {
            int rowCount;
            return new InvoiceSearchRequestResponse
            {
                Invoices = invoiceRepository.SearchInvoices(request,out rowCount),
                TotalCount = rowCount
            };
        }
        #endregion
    }
}
