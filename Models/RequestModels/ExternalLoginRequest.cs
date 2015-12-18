namespace SMD.Models.RequestModels
{
    /// <summary>
    /// External Login Request Model 
    /// </summary>
    public class ExternalLoginRequest
    {
        /// <summary>
        /// User Full Name
        /// </summary>
        public string FullName { get; set; }
        
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
