using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Interfaces.Services;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// City Service
    /// </summary>
    public class CountryService : ICountryService
    {
        #region Private

        private readonly ICountryRepository countryRepository;
        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public List<Country> GetCountries()
        {
            return countryRepository.GetAllCountries();
        }
        #endregion
    }
}
