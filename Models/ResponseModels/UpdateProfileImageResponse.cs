namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Update Profile Image Response 
    /// </summary>
    public class UpdateProfileImageResponse : BaseApiResponse
    {
        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
