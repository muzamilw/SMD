using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Tax Repository 
    /// </summary>
    public class TaxRepository : BaseRepository<Tax>, ITaxRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public TaxRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Tax> DbSet
        {
            get { return db.Taxes; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Get Tax By Country 
        /// </summary>
        public Tax GetTaxByCountryId(int? countryId)
        {
           return DbSet.FirstOrDefault(tax => tax.CountryId == countryId);
        }
        #endregion
    }
}
