using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ITaxRepository : IBaseRepository<Tax, long>
    {
        /// <summary>
        /// Get Tax By Country 
        /// </summary>
        Tax GetTaxByCountryId(int? countryId);
    }
}
