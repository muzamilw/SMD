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
                LocationName = source.CountryName,
                IsCountry = true,
                CountryId = source.CountryId,
                bindedValue = source.CountryName
                
            };
        }

        public static LocationDropDown CreateCityLocationFrom(this City source)
        {
            return new LocationDropDown
            {
                LocationId = source.CityId + "_City",
                LocationName = source.CityName,
                GeoLat = source.GeoLat,
                GeoLong = source.GeoLong,
                IsCity = true,
                CityId = source.CityId,
                CountryId = source.CountryId.Value,
                parentCountryName = source.Country.CountryName,
                bindedValue = source.CityName + " , " + source.Country.CountryName
            };
        }
    }
}