using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICityRepository : IBaseRepository<City, int>
    {
        /// <summary>
        /// Get List of City 
        /// </summary>
        IEnumerable<City> GetAllCities();
        /// <summary>
        /// Get List of City 
        /// </summary>
        IEnumerable<City> GetSearchCities(string searchText);
    }
}
