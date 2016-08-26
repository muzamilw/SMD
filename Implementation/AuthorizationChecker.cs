using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SMD.Common;
using SMD.Interfaces.Data;
using SMD.Interfaces;
using SMD.Models.Common;

namespace SMD.Implementation
{
    /// <summary>
    /// Check that the current user has the required access rights
    /// </summary>
    public sealed class AuthorizationChecker : IAuthorizationChecker
    {
        #region Public

        /// <summary>
        /// Check that the user has the required rights
        /// </summary>
        public bool Check(AuthorizationCheckRequest request)
        {
            //if (IsPortalAdministrator())
            //{
            //    return true;
            //}

            return HasRequiredPortalRole(request.RequiredPortalRoles);
                //&& HasRequiredAccessRights(request.RequiredAccessRights);
        }
        
        /// <summary>
        /// True if the current user is a portal administrator (work across sites, ie. if applied to one site user then we are good)
        /// </summary>
        public bool IsPortalAdministrator()
        {
            IList<SmdRoleClaimValue> roles = ClaimHelper.GetClaimsByType<SmdRoleClaimValue>(SmdClaimTypes.Role);
            return roles.Any(role => role.Role == SecurityRoles.Supernova_Admin);
        }

        /// <summary>
        /// Check if the user has the required portal roles
        /// </summary>
        public bool HasRequiredPortalRole(IEnumerable<string> requiredPortalRoles)
        {
            if (requiredPortalRoles == null)
            {
                throw new ArgumentNullException("requiredPortalRoles");
            }
            IEnumerable<SmdRoleClaimValue> roles = ClaimHelper.GetClaimsByType<SmdRoleClaimValue>(SmdClaimTypes.Role);
            return !requiredPortalRoles.Any() || roles.Any(role => requiredPortalRoles.Contains(role.Role));
        }

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        public bool HasRequiredAccessRights(IEnumerable<SecurityAccessRight> requiredAccessRights)
        {
            if (requiredAccessRights == null)
            {
                throw new ArgumentNullException("requiredAccessRights");
            }
            if (!requiredAccessRights.Any())
            {
                return true;
            }
            
            IEnumerable<AccessRightClaimValue> userAccessRights = ClaimHelper.GetClaimsByType<AccessRightClaimValue>(SmdClaimTypes.AccessRight);
            return requiredAccessRights.All(accessRight => userAccessRights.Any(arc => arc.RightId == (long)accessRight));
        }

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        public bool HasRequiredAccessRight(SecurityAccessRight requiredAccessRight)
        {
            return HasRequiredAccessRights(new[] { requiredAccessRight });
        }

        #endregion
    }
}
