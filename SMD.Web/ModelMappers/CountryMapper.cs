using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Country Mapper
    /// </summary>
    public static class CountryMapper
    {
        public static CountryDropdown CreateFrom(this Country source)
        {
            return new CountryDropdown
            {
                CountryId = source.CountryId,
                CountryName = source.CountryName
            };
        }
    }
}