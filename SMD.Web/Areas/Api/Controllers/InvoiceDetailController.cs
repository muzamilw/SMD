using System.Collections.Generic;
using System.Linq;
using SMD.Implementation.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Invoice Detail Api Controller 
    /// </summary>
    [Authorize]
    public class InvoiceDetailController : ApiController
    {
        #region Public

        private readonly InvoiceDetailService invoiceDetailService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public InvoiceDetailController(InvoiceDetailService invoiceDetailService)
        {
            this.invoiceDetailService = invoiceDetailService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Invoice Details
        /// </summary>
        public IEnumerable<InvoiceDetail> Get([FromUri] GetInvoiceDetailRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response= invoiceDetailService.GetInvoiceDetailByInvoiceId(request.InvoiceId);
            return response.Select(invoDetail => invoDetail.CreateFrom());
        }
        #endregion
    }
}