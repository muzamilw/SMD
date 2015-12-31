using SMD.MIS.Areas.Api.Models;
using System.Linq;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Invoice Mapper
    /// </summary>
    public static class InvoiceMapper
    {

        /// <summary>
        /// Domain Resposne To Web Response 
        /// </summary>
        public static InvoiceSearchRequestResponse CreateFrom(
            this Models.ResponseModels.InvoiceSearchRequestResponse source)
        {
            return new InvoiceSearchRequestResponse
            {
                TotalCount = source.TotalCount,
                Invoices = source.Invoices.Select(invo => invo.CreateFrom()).ToList()
            };
        }


        /// <summary>
        /// Domain to Web 
        /// </summary>
        public static Invoice CreateFrom(this Models.DomainModels.Invoice source)
        {
            return new Invoice
            {
                InvoiceDate = source.InvoiceDate,
                InvoiceId = source.InvoiceId,
                CreditCardRef = source.CreditCardRef,
                Total = source.Total,
                NetTotal = source.NetTotal,
                UserName= source.User.FullName
            };
        }
             
    }
}