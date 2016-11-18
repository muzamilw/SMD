using System.Collections.Generic;

using SMD.Models.DomainModels;
using SMD.Interfaces.Services;
using SMD.Interfaces.Repository;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// City Service
    /// </summary>
    public class CityService : ICityService
    {
        #region Private

        private readonly ICityRepository cityRepository;
        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public IEnumerable<City> GetCitiesByCountryId(long countryId)
        {
            return cityRepository.GetAllCitiesOfCountry(countryId);
        }
        public IEnumerable<City> GetCities()
        {
            return cityRepository.GetCities();
        }
        #endregion
    }
}
