﻿namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// WebApi User 
    /// </summary>
    public class WebApiUser
    {
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
        public int Age { get; set; }
    }
}