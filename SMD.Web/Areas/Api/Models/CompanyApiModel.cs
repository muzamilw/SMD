using System;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// CompanyApiModel  
    /// </summary>
    public class CompanyApiModel
    { 

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
       
        public string Logo { get; set; }
        public string StripeCustomerId { get; set; }
       
        public string SalesEmail { get; set; }
      
        public string WebsiteLink { get; set; }
     
        public string VoucherSecretKey { get; set; }

        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingState { get; set; }
        public Nullable<int> BillingCountryId { get; set; }
        public string BillingCity { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string TwitterHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string InstagramHandle { get; set; }
        public string PinterestHandle { get; set; }


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

     

    }
}