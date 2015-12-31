using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Invoice Details Mapper
    /// </summary>
    public static class InvoiceDetailMapper
    {
        public static InvoiceDetail CreateFrom(this Models.DomainModels.InvoiceDetail source)
        {
            return new InvoiceDetail
            {
                InvoiceId = source.InvoiceId,
                InvoiceDetailId = source.InvoiceDetailId,
                ItemAmount = source.ItemAmount,
                ItemGrossAmount = source.ItemGrossAmount,
                ItemName = source.Product.ProductName
            };
        }
    }
}