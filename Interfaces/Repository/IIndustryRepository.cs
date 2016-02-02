using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IIndustryRepository : IBaseRepository<Industry, int>
    {
        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        IEnumerable<Industry> SearchIndustries(string input);

        /// <summary>
        /// Get All Active ones
        /// </summary>
        IEnumerable<Industry> GetAllAvailable();
    }


}
