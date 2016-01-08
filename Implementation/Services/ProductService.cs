using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Product Service Implementation 
    /// </summary>
    public class ProductService : IProductService
    {
        #region Private

        private readonly IProductRepository productRepository;
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        #endregion
        #region Public

        
        #endregion
    }
}
