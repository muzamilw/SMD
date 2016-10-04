using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
    public class CompanyResponseModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string SalesPhone { get; set; }
        public string Tel2 { get; set; }

        public string Logo { get; set; }
        public string StripeCustomerId { get; set; }
        public string SalesEmail { get; set; }
        public string WebsiteLink { get; set; }
        public string VoucherSecretKey { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingState { get; set; }
        public int? BillingCountryId { get; set; }
        public string BillingCity { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string BillingCountryName { get; set; }


        #region Additional User Profile Properties
        
        public int Profession { get; set; }
        public int Solutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }

        public string PassportNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsReceiveDeals { get; set; }
        public bool IsReceiveWeeklyUpdates { get; set; }
        public bool IsReceiveLatestServices { get; set; }

        #endregion
        #region Branch Properties
        public string BranchName { get; set; }
        public int BranchesCount { get; set; }
        public int CompanyType { get; set; }
        public string CompanyTypeName { get; set; }
        public string AboutUs { get; set; }
        public string BranchLocationLat { get; set; }
        public string BranchLocationLong { get; set; }
        #endregion
        #region Money Properties
        public string PayPalId { get; set; }
        public string BillingBusinessName { get; set; }
        public string CompanyRegistrationNo { get; set; }
        public DateTime? BusinessStartDate { get; set; }
        public string VatNumber { get; set; }
        public long BranchId { get; set; }
        
        #endregion


        public string LogoImageBase64 { get; set; }
        public byte[] LogoImageBytes
        {
            get
            {
                if (string.IsNullOrEmpty(LogoImageBase64))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = LogoImageBase64.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (LogoImageBase64.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = LogoImageBase64.Substring(firtsAppearingCommaIndex + 1);

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

        public bool LogoChanged { get; set; }
    }
}
