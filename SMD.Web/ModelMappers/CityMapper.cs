using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;

namespace SMD.MIS.ModelMappers
{
    public static class CityMapper
    {
        public static CityDropDown CreateFrom(this City source)
        {
            return new CityDropDown
            {
                CityId = source.CityId,
                CountryId = source.CountryId,
                CityName = source.CityName
            };
        }
    }
}