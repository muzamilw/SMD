using SMD.Common;
using System.Security.Claims;
using SMD.Interfaces;
using SMD.Models.Common;

namespace SMD.Implementation
{
    /// <summary>
    /// Service that manages security claims
    /// </summary>
    public class ClaimsSecurityService : IClaimsSecurityService
    {
        #region Private


        #endregion
        #region Constructor
        
        #endregion
        #region Public
        
        /// <summary>
        /// Add general claims 
        /// </summary>
        public void AddClaimsToIdentity(UserIdentityModel userIdentity, ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim(SmdClaimTypes.UserTimezoneOffset, userIdentity.TimezoneOffset.ToString()));
        }
        #endregion
    }
}
