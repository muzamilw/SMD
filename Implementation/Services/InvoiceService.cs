using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Invoice Detail Service Model 
    /// </summary>
    public class InvoiceDetailService : IInvoiceDetailService
    {
        #region Private

        private readonly IInvoiceDetailRepository invoiceDetailRepository;
        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public InvoiceDetailService(IInvoiceDetailRepository invoiceDetailRepository)
        {
            this.invoiceDetailRepository = invoiceDetailRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Detail by Id 
        /// </summary>
        public IEnumerable<InvoiceDetail> GetInvoiceDetailByInvoiceId(long invoiceId)
        {
            return invoiceDetailRepository.GetInvoiceDetailByInvoiceId(invoiceId);
        }
      
        #endregion
    }
}
