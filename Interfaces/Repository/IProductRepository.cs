using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Product Repository Interface 
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product, long>
    {
        /// <summary>
        /// Get Product By Country 
        /// </summary>
        Product GetProductByCountryId(string productCode);
    }
}
