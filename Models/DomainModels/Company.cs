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
        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }

    }
}
