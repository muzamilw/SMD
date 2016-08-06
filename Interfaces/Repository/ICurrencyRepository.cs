using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Country Repository Interface 
    /// </summary>
    public interface ICurrencyRepository : IBaseRepository<Currency, int>
    {
        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        IEnumerable<Currency> GetAllCurrencies();

        IEnumerable<Currency> GetSearchedCurrencies(string searchString);
     
    }
}
