
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// City Interface
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// List of Country's Cities
        /// </summary>
        List<Country> GetCountries();
    }
}
