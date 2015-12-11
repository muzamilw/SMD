using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Location Mapper
    /// </summary>
    public static class LocationMapper
    {
        public static LocationDropDown CreateCountryLocationFrom(this Country source)
        {
            return new LocationDropDown
            {
                LocationId = source.CountryId + "_Country",
                LocationName = source.CountryName
            };
        }

        public static LocationDropDown CreateCityLocationFrom(this City source)
        {
            return new LocationDropDown
            {
                LocationId = source.CityId + "_City",
                LocationName = source.CityName
            };
        }
    }
}