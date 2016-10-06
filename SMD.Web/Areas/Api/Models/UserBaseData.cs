using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class UserBaseData
    {
        //public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? LanguageId { get; set; }
        public int? IndustryId { get; set; }
        public long? EducationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Industry { get; set; }
        public string Education { get; set; }
        public string CurrencySymbol { get; set; }
        public bool isStripeIntegrated { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool isUserAddmin { get; set; }

        public bool? IsSpecialAccount { get; set; }
    }
}