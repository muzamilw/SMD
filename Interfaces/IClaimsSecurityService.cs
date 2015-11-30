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
        /// Lookup identity using the claims identity
        /// </summary>
        ClaimsIdentity LookupIdentity(ClaimsIdentity claimsIdentity);

        /// <summary>
        /// Add claims to the identity
        /// </summary>
        void AddClaimsToIdentity(UserIdentityModel identity, ClaimsIdentity claimsIdentity);

        /// <summary>
        /// Lookup name id and provider name
        /// </summary>
        void LookupIdentityClaimValues(ClaimsIdentity claimsIdentity, out string providerName, out string nameIdentifier);
    }
}
