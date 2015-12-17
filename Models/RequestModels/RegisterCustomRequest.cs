namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Register Custom Request Model 
    /// </summary>
    public class RegisterCustomRequest
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
        /// Password 
        /// </summary>
        public string Password { get; set; }
    }
}
