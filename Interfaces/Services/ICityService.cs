
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
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
        IEnumerable<City> GetCities();
        List<string> GetTargetCitiesPerCampaign(long id);
    }
}
