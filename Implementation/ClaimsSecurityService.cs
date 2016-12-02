﻿using SMD.Common;
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




        public void AddCompanyIdClaimToIdentity(ClaimsIdentity identity, int CompanyId, string CompanyName, string CompanyLogo, string RoleName,string userName,string MobileNumber,string email, string sRoleId,string CompanyCity,string AddressLine1)
        {
            Claim claim = new Claim(SmdClaimTypes.CompanyId,
                // ReSharper restore SuggestUseVarKeywordEvident
                                        ClaimHelper.Serialize(
                                            new CompanyIdClaimValue { CompanyId = CompanyId, CompanyName = CompanyName, CompanyLogo = CompanyLogo, UserName = userName, MobileNumber = MobileNumber, Email = email, CompanyCity = CompanyCity, addressLine1 = AddressLine1 }),
                                        typeof(CompanyIdClaimValue).AssemblyQualifiedName);
            identity.AddClaim(claim);

            identity.AddClaim(new Claim(SmdClaimTypes.Role, ClaimHelper.Serialize(new SmdRoleClaimValue { Role = RoleName, RoleId= sRoleId }), typeof(SmdRoleClaimValue).AssemblyQualifiedName));
        }
        #endregion
    }
}
