using System.Collections.Generic;



namespace SMD.MIS.Areas.Api.Models
{
    public class UserProfileBaseResponse
    {
        /// <summary>
        /// List of Countries
        /// </summary>
        public IEnumerable<CountryDropdown> CountryDropdowns { get; set; }

        /// <summary>
        /// List of Cities 
        /// </summary>
        public IEnumerable<CityDropDown> CityDropDowns { get; set; }

        /// <summary>
        /// List of Industries 
        /// </summary>
        public IEnumerable<IndusteryDropdown> IndusteryDropdowns { get; set; }

        /// <summary>
        /// List of Educations
        /// </summary>
        public IEnumerable<EducationDropdown> EducationDropdowns { get; set; }

        /// <summary>
        /// List of time zones 
        /// </summary>
        public IEnumerable<TimeZoneDropDown> TimeZoneDropDowns { get; set; }

        public IEnumerable<RoleDropDown> UserRoles { get; set; }
    }
}