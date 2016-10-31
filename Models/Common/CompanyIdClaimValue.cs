namespace SMD.Models.Common
{
    /// <summary>
    /// Role Claim Value
    /// </summary>
    public class CompanyIdClaimValue
    {
        /// <summary>
        /// Role
        /// </summary>
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string RoleId { get; set; }

    }
}
