using System.Collections.Generic;
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
    /// Country Repository 
    /// </summary>
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CurrencyRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Currency> DbSet
        {
            get { return db.Currencies; }
        }
        #endregion

        
        #region Public


        public Currency Find(int id)
        {
            return DbSet.Where(currency => currency.CurrencyId == id).SingleOrDefault();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<Currency> GetAllCurrencies()
        {
            return DbSet.Select(currency => currency).ToList();
        }
        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<Currency> GetSearchedCurrencies(string searchString)
        {
            return DbSet.Where(currency => currency.Name.StartsWith(searchString)).ToList();
        }
        
        #endregion
    }
}
