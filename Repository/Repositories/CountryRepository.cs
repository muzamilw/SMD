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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CountryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Country> DbSet
        {
            get { return db.Countries; }
        }
        #endregion
        #region Public
        public Country Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<Country> GetAllCountries()
        {
           return DbSet.Select(country => country).ToList();
        }
        #endregion
    }
}
