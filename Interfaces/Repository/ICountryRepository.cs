using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Country Repository Interface 
    /// </summary>
    public interface ICountryRepository : IBaseRepository<Country, int>
    {
        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        IEnumerable<Country> GetAllCountries();
    }
}
