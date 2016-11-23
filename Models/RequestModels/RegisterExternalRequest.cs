namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Register External Request Model 
    /// </summary>
    public class RegisterExternalRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Login Provider Name 
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Login Provider Key
        /// </summary>
        public string LoginProviderKey { get; set; }

        public string ProfilePicturePath { get; set; }

        public string Client { get; set; }

        public string NotificationToken { get; set; }
    }
}
