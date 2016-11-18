
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// City Interface
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// List of Country's Cities
        /// </summary>
        IEnumerable<City> GetCitiesByCountryId(long countryId);
    }
}
