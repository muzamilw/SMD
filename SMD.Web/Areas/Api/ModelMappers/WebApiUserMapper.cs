using System.Linq;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Web;
using SMD.MIS.ModelMappers;
using SMD.Models.Common;
using SMD.Models.DomainModels;
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
            var user = new WebApiUser
                   {
                       UserId = source.Id,
                       FullName = source.FullName,
                       Address1 = source.Company.AddressLine1,
                       CompanyName = source.Company.CompanyName,
                       Email = source.Email,
                       JobTitle = source.Jobtitle,
                       UserTimeZone = source.UserTimeZone,
                       Gender = source.Gender,
                       Address2 = source.Company.AddressLine2,
                       DOB =  source.DOB,
                       CityId = source.Company.CityId,
                       ContactNotes = source.ContactNotes,
                       CountryId = source.Company.CountryId,
                       IndustryId = source.IndustryId,
                       Phone1 = source.Phone1,
                       Phone2 = source.Phone2,
                       State = source.Company.State,
                       ZipCode = source.Company.ZipCode,
                       ImageUrl = !string.IsNullOrEmpty(source.Company.Logo) ? HttpContext.Current.Request.Url.Scheme + "://" + 
                       HttpContext.Current.Request.Url.Host + "/" + source.ProfileImage + "?" + DateTime.Now : string.Empty,
                       AdvertContact = source.Company.CompanyName,
                       AdvertContactEmail = source.Company.ReplyEmail,
                       AdvertContactPhone = source.Company.Tel1,
                       EducationId = source.EducationId,
                       StripeId = source.Company.StripeCustomerId,
                       GoogleVallet = source.Company.GoogleWalletCustomerId,
                       PayPal = source.Company.PaypalCustomerId,
                       AccountBalance = CreateFromForAccount(source),
                       CityName = source.Company.City == null?null: source.Company.City.CityName,
                       CountryName = source.Company.Country == null?null: source.Company.Country.CountryName,
                       CompanyId = source.Company.CompanyId,
                       AuthenticationToken = source.AuthenticationToken,
                       Password = source.PasswordHash,
                       RoleId = source.Roles.Select(c => c.Id).FirstOrDefault()
                   };

            return user;
        }

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static UserBalanceInquiryResponse CreateFromForBalance(this LoginResponse source)
        {
            return new UserBalanceInquiryResponse
            {
                Status = source.Status,
                Message = source.Message,
                Balance = CreateFromForAccount(source.User)
            };
        }

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static double? CreateFromForAccount(this SMD.Models.IdentityModels.User source)
        {
            if (source.Accounts == null || !source.Accounts.Any())
            {
                return null;
            }

            Account account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
            if (account == null)
            {
                return null;
            }

            return account.AccountBalance;
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
                User = source.User != null ? source.User.CreateFrom() : null//,
                //AuthenticationToken = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Base Data Create From Domin mOdels
        /// </summary>
        public static UserProfileBaseResponse CreateFrom(this UserProfileBaseResponseModel source)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select(timeZoneInfo => new TimeZoneDropDown
            {
                TimeZoneId = timeZoneInfo.Id,
                TimeZoneName = timeZoneInfo.BaseUtcOffset + "  [ " + timeZoneInfo.Id + " ]"
            }).ToList();

            return new UserProfileBaseResponse
            {
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                IndusteryDropdowns = source.Industries.Select(industery => industery.CreateForDd()),
                EducationDropdowns = source.Educations.Select(edu => edu.CreateFromDd()),
                UserRoles = source.UserRoles.Select(role => role.CreateFromDd()),
                TimeZoneDropDowns = timeZones
            };
        }
    }
}