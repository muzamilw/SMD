using System;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Update User Profile Request Model 
    /// </summary>
    public class UpdateUserProfileRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public int? Gender { get; set; }

       
        /// <summary>
        /// Authentication Token
        /// </summary>
        public string AuthenticationToken { get; set; }


        /// <summary>
        /// User Time ZOne
        /// </summary>
        public string Title { get; set; }

        public string FullName { get; set; }

        public int ProfessionID { get; set; }
             
    }
}
