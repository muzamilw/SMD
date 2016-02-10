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
        /// Country name
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        public string City { get; set; }
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
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Industry Id
        /// </summary>
        public int? IndustryId { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Profile Image
        /// Base64 representation of image
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Profile Image Name
        /// </summary>
        public string ProfileImageName { get; set; }

        /// <summary>
        /// Profile Image Bytes
        /// </summary>
        public byte[] ProfileImageBytes
        {
            get
            {
                if (string.IsNullOrEmpty(ProfileImage))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = ProfileImage.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (ProfileImage.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = ProfileImage.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// Authentication Token
        /// </summary>
        public string ProfileImageBytesString { get; set; }

        /// <summary>
        /// Authentication Token
        /// </summary>
        public string AuthenticationToken { get; set; }

      

        /// <summary>
        /// Advertising Contact
        /// </summary>
        public string AdvertContact { get; set; }

        /// <summary>
        /// Advertising Contact phone
        /// </summary>
        public string AdvertContactPhone { get; set; }

        /// <summary>
        /// Advertising Contact Email
        /// </summary>

        public string AdvertContactEmail { get; set; }

        /// <summary>
        /// User Time ZOne
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// EDucation Id
        /// </summary>
        public long? EducationId { get; set; }

        /// <summary>
        /// PayPAl Account
        /// </summary>
        public string PayPal { get; set; }
    }
}
