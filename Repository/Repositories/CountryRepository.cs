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
            return DbSet.Where(c => c.CountryId == id).FirstOrDefault();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public List<Country> GetAllCountries()
        {
            db.Configuration.LazyLoadingEnabled = false;

           return DbSet.Select(country => country).ToList();
        }
        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<Country> GetSearchedCountries(string searchString)
        {
            return DbSet.Where(country => country.CountryName.StartsWith(searchString)).ToList();
        }
        public int GetCountryId(string name)
        {
            int countryId = 0;
            Country country = DbSet.Where(g => g.CountryName.ToLower() == name.ToLower()).SingleOrDefault();
            if(country != null)
            {
                countryId = country.CountryId;
            }
            else
            {
                country = new Country();
                country.CountryName = name;
                db.Countries.Add(country);
                db.SaveChanges();
                countryId = country.CountryId;
            }
            return countryId;
        }

        public string GetCountryNameById(int countryId)
        {
            var country = DbSet.FirstOrDefault(c => c.CountryId == countryId);
            return country != null ? country.CountryName : string.Empty;
        }
        public int GetCurrencyCode(int countryId)
        {
            return DbSet.Where(c => c.CountryId == countryId).FirstOrDefault().CurrencyID ??0;    
        }
        #endregion
    }
}
