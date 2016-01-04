
namespace SMD.MIS.Areas.Api.Models
{
    public class LocationDropDown
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }
        public bool IsCity { get; set; }
        public bool IsCountry { get; set; }
        public long CityId { get; set; }
        public long CountryId { get; set; }
        public string parentCountryName { get; set; } //contains country id if locationName is city
    }
}