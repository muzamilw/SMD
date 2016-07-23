namespace SMD.Common
{
    /// <summary>
    /// Smd Claim Types
    /// </summary>
    public class SmdClaimTypes
    {
        /// <summary>
        /// Access right
        /// </summary>
        public const string AccessRight = "http://schemas.smd.com/2015/12/identity/claims/accessRight";

        /// <summary>
        /// System User Id
        /// </summary>
        public const string SystemUser = "http://schemas.smd.com/2015/12/identity/claims/systemUser";

        /// <summary>
        /// User Timezone Offset
        /// </summary>
        public const string UserTimezoneOffset = "http://schemas.smd.com/2015/12/identity/claims/userTimeZoneOffset";

        /// <summary>
        /// Logged in CompanyId
        /// </summary>
        public const string CompanyId = "http://schemas.smd.com/2015/12/identity/claims/CompanyId";
    }
}
