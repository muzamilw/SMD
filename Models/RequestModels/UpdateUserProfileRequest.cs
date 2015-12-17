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
        /// Phone 1
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone 2
        /// </summary>
        public string Phone2 { get; set; }

        /// <summary>
        /// Job Title 
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Contact Notes
        /// </summary>
        public string ContactNotes { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Address 1 
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// CityId
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Country Id
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Zip Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Industry Id
        /// </summary>
        public int? IndustryId { get; set; }
    }
}
