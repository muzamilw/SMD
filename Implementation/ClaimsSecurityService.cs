using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Add  general claims 
        /// </summary>
        public void AddClaimsToIdentity(UserIdentityModel userIdentity, ClaimsIdentity identity)
        {
            //ClaimHelper.AddClaim(new Claim(CaresUserClaims.Role, defaultRoleName), identity);   // role claim
            //ClaimHelper.AddClaim(new Claim(CaresUserClaims.Name, userName), identity);         // user name claim
        }
        #endregion
    }
}
