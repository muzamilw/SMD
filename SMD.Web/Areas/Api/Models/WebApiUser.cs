using System;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// WebApi User 
    /// </summary>
    public class WebApiUser
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Job Title
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// UserTimeZone
        /// </summary>
        public string UserTimeZone { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Phone 1
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone 2
        /// </summary>
        public string Phone2 { get; set; }
      

        /// <summary>
        /// Contact Notes
        /// </summary>
        public string ContactNotes { get; set; }
        
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
        /// Industry Id
        /// </summary>
        public int? IndustryId { get; set; }

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
        /// EDucation Id
        /// </summary>
        public long? EducationId { get; set; }

        /// <summary>
        /// Stripe Account
        /// </summary>
        public string StripeId { get; set; }

        /// <summary>
        /// PayPAl Account
        /// </summary>
        public string PayPal { get; set; }

        /// <summary>
        /// Goole Walet 
        /// </summary>
        public string GoogleVallet { get; set; }

        /// <summary>
        /// Account Balance
        /// </summary>
        public double? AccountBalance { get; set; }

        public string CountryName { get; set; }
        public string CityName { get; set; }
        public int CompanyId { get; set; }
        public string AuthenticationToken { get; set; }

        public string Password { get; set; }
        public string RoleId { get; set; }
        public string VoucherSecretKey { get; set; }

        public string Title { get; set; }
    }
}