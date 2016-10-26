using SMD.Models.Common;
using System.Security.Claims;

namespace SMD.Interfaces
{

    /// <summary>
    /// Service that adds security claims to the 
    /// </summary>
    public interface IClaimsSecurityService
    {
        /// <summary>
        /// Add claims to the identity
        /// </summary>
        void AddClaimsToIdentity(UserIdentityModel identity, ClaimsIdentity claimsIdentity);

        void AddCompanyIdClaimToIdentity(ClaimsIdentity identity, int CompanyId, string CompanyName, string CompanyLogo, string RoleName, string userName, string MobileNumber,string email);
    }
}
