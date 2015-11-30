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
    public sealed class ClaimsSecurityService : MarshalByRefObject, IClaimsSecurityService
    {
        #region Private

        /// <summary>
        /// Add the user's name to the claims
        /// </summary>
        private void CheckName(SmdUser user, ClaimsIdentity claimsIdentity)
        {
            Claim nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (nameClaim == null)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
                claimsIdentity.AddClaim(new Claim(SmdClaimTypes.MisUser,
                                        ClaimHelper.Serialize(
                                            new NameClaimValue
                                            {
                                                Name = user.FullName
                                            }),
                                        typeof(NameClaimValue).AssemblyQualifiedName));
                
                if (string.IsNullOrEmpty(user.Id))
                {
                    return;
                }
            }
        }
        
        /// <summary>
        /// Add Access Right claims
        /// </summary>
       private static void AddAccessRightClaims(SmdUser user, ClaimsIdentity claimsIdentity)
        {
           
            
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Add security role claims
        /// </summary>
        private static void AddRoleClaims(SmdUser user, ClaimsIdentity claimsIdentity)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            Claim claim = new Claim(SmdClaimTypes.MisRole,
// ReSharper restore SuggestUseVarKeywordEvident
                                        ClaimHelper.Serialize(
                                            new SmdRoleClaimValue { Role = user.Role }),
                                        typeof(SmdRoleClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }
        
        #endregion

        #region Public
        
        /// <summary>
        /// Add User Claims
        /// </summary>
        private void AddUserClaims(SmdUser user, ClaimsIdentity claimsIdentity)
        {
            AddRoleClaims(user, claimsIdentity);
            AddAccessRightClaims(user, claimsIdentity);
        }

        #endregion
        
        #region Public
        
        /// <summary>
        /// Lookup name id and provider name
        /// </summary>
        public void LookupIdentityClaimValues(ClaimsIdentity claimsIdentity, out string providerName, out string nameIdentifier)
        {
            Claim nameIdentifierClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimsSecurityService_MissingNameIdentifierClaim);
            }

            Claim identityProviderClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == IdentityProviderClaimType);
            if (identityProviderClaim == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimsSecurityService_IdentityProviderClaim);
            }

            providerName = identityProviderClaim.Value;
            nameIdentifier = nameIdentifierClaim.Value;
        }

        /// <summary>
        /// Lookup identity using the claims identity
        /// </summary>
        public ClaimsIdentity LookupIdentity(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            string providerName;
            string nameIdentifier;

            LookupIdentityClaimValues(claimsIdentity, out providerName, out nameIdentifier);

            return claimsIdentity;
        }

        /// <summary>
        /// Add claims to the identity
        /// </summary>
        public void AddClaimsToIdentity(UserIdentityModel identity, ClaimsIdentity claimsIdentity)
        {
        }

        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        #endregion
    }
}
