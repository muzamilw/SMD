using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Product Repository 
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ProductRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Product> DbSet
        {
            get { return db.Products; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Get Product By Country 
        /// </summary>
        public Product GetProductByCountryId(int? countryId, string productCode)
        {
            return DbSet.FirstOrDefault(pro => pro.CountryId == countryId && pro.ProductCode==productCode);
        }
        #endregion
    }
}
