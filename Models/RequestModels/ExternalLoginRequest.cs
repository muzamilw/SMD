namespace SMD.Models.RequestModels
{
    /// <summary>
    /// External Login Request Model 
    /// </summary>
    public class ExternalLoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Login Provider Name 
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Login Provider Key
        /// </summary>
        public string LoginProviderKey { get; set; }
    }
}
