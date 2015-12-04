using SMD.MIS.Areas.Api.Models;
using SMD.Models.IdentityModels;

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
        public static WebApiUser CreateFrom(this User source)
        {
            return new WebApiUser
                   {
                       FullName = source.FullName,
                       Address1 = source.Address1,
                       CompanyName = source.CompanyName,
                       Email = source.Email,
                       JobTitle = source.Jobtitle,
                       UserTimeZone = source.UserTimeZone
                   };
        }
    }
}