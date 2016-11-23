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

        public string City { get; set; }


        public string Country { get; set; }

        public string ProfilePicturePath { get; set; }

        public string Client { get; set; }

        public string NotificationToken { get; set; }
    }
}
