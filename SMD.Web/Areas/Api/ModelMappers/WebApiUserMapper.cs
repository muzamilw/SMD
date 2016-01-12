using System.Linq;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Web;
using SMD.MIS.ModelMappers;
using SMD.Models.ResponseModels;
using LoginResponse = SMD.Models.ResponseModels.LoginResponse;

namespace SMD.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// WebApi User Mapper
    /// </summary>
    public static class WebApiUserMapper
    {
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static WebApiUser CreateFrom(this SMD.Models.IdentityModels.User source)
        {
            return new WebApiUser
                   {
                       UserId = source.Id,
                       FullName = source.FullName,
                       Address1 = source.Address1,
                       CompanyName = source.CompanyName,
                       Email = source.Email,
                       JobTitle = source.Jobtitle,
                       UserTimeZone = source.UserTimeZone,
                       Gender = source.Gender,
                       Address2 = source.Address2,
                       Age =  (source.Age ?? 0),
                       CityId = source.CityId,
                       ContactNotes = source.ContactNotes,
                       CountryId = source.CountryId,
                       IndustryId = source.IndustryId,
                       Phone1 = source.Phone1,
                       Phone2 = source.Phone2,
                       State = source.State,
                       ZipCode= source.ZipCode,
                       ImageUrl = !string.IsNullOrEmpty(source.ProfileImage) ? HttpContext.Current.Request.Url.Scheme + "://" + 
                       HttpContext.Current.Request.Url.Host + "/" + source.ProfileImage + "?" + DateTime.Now : string.Empty
                   };
        }

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static Models.LoginResponse CreateFrom(this LoginResponse source)
        {
            return new Models.LoginResponse
            {
                Status = source.Status,
                Message = source.Message,
                User = source.User != null ? source.User.CreateFrom() : null,
                AuthenticationToken = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Base Data Create From Domin mOdels
        /// </summary>
        public static UserProfileBaseResponse CreateFrom(this UserProfileBaseResponseModel source)
        {
            return new UserProfileBaseResponse
            {
                CityDropDowns = source.Cities.Select(city => city.CreateFrom()),
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                IndusteryDropdowns = source.Industries.Select(industery => industery.CreateForDd())
            };
        }
    }
}