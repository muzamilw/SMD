using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Company
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
        
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string TwitterHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string InstagramHandle { get; set; }
        public string PinterestHandle { get; set; }

        public Nullable<bool> IsSpecialAccount { get; set; }
        


        public string City { get; set; }
        public string BillingCity { get; set; }
        public string AboutUsDescription { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> PaymentMethodStatus { get; set; }
        public Nullable<System.DateTime> LastPaymentMethodErrorDate { get; set; }
        public string LastPaymentMethodError { get; set; }

        public string CompanyRegNo { get; set; }
        public string TaxRegNo { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<int> NoOfBranches { get; set; }
        public string BillingAddressName { get; set; }

        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
       
        public virtual Country Country { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }

        //public virtual CompaniesAspNetUser CompaniesAspNetUsers { get; set; }
        public virtual ICollection<User> AspNetUsers { get; set; }

        public virtual ICollection<CompanyBranch> CompanyBranches { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }

        public virtual ICollection<BranchCategory> BranchCategories { get; set; }

        public virtual ICollection<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }

        public virtual ICollection<DamImage> DamImages { get; set; }

        public virtual ICollection<ProfileQuestion> ProfileQuestions { get; set; }

        public virtual ICollection<PayOutHistory> PayOutHistories { get; set; }

    }
}
