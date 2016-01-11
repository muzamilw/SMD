using SMD.Interfaces.Repository;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get City By Country 
    /// </summary>
    public class GetCityByCountryController : ApiController
    {
        #region Private

        private readonly ICityService cityService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCityByCountryController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get User's Profile 
        /// </summary>
        public IEnumerable<CityDropDown> Get(long countryId)
        {
            return cityService.GetCitiesByCountryId(countryId).Select(city => city.CreateFrom()).ToList();
        }
        #endregion
    }
}