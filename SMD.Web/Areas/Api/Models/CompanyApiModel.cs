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
        public string ReplyEmail { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Logo { get; set; }
        public string StripeCustomerId { get; set; }
        public string ChargeBeesubscriptionID { get; set; }
        public Nullable<bool> RegisteredViaReferral { get; set; }
        public int? ReferringCompanyID { get; set; }
        public string PaypalCustomerId { get; set; }
        public string GoogleWalletCustomerId { get; set; }
        public Nullable<int> PreferredPayoutAccount { get; set; }
        public string SalesEmail { get; set; }
        public string ReferralCode { get; set; }
        public Nullable<bool> AfilliatianStatus { get; set; }
        public string WebsiteLink { get; set; }
        public int? CompanyType { get; set; }
        public string VoucherSecretKey { get; set; }

        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingState { get; set; }
        public Nullable<int> BillingCountryId { get; set; }
        public Nullable<int> BillingCityId { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string TwitterHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string InstagramHandle { get; set; }
        public string PinterestHandle { get; set; }

        
        /// <summary>
        /// Image Url
        /// </summary>
     

    }
}