namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Standard Login Request Model 
    /// </summary>
    public class StandardLoginRequest
    {
        /// <summary>
        /// UserName 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        public string Password { get; set; }
    }
}
