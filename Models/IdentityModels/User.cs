using System;
using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.IdentityModels
{
    /// <summary>
    /// User Domain Model
    /// </summary>
    public partial class User
    {
        #region Persisted Properties
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AlternateEmail { get; set; }
        public string IsEmailVerified { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Jobtitle { get; set; }
        public string ContactNotes { get; set; }
        public bool IsSubscribed { get; set; }
        public Nullable<int> AppID { get; set; }
        public Nullable<bool> IsCompanyRepresentative { get; set; }
        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public Nullable<int> CityId { get; set; }
        //public Nullable<int> CountryId { get; set; }
        //public string State { get; set; }
        //public string ZipCode { get; set; }
        public string UserTimeZone { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public Nullable<int> IndustryId { get; set; }
        public Nullable<long> EducationId { get; set; }
        public string ProfileImage { get; set; }
        public string UserCode { get; set; }
        public string SmsCode { get; set; }
        public string WebsiteLink { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string AuthenticationToken { get; set; }

        public virtual Company Company { get; set; }
        public virtual Industry Industry { get; set; }
        public virtual Language Language { get; set; }

        public Nullable<int> DevicePlatform { get; set; }

        public Nullable<bool> optDealsNearMeEmails { get; set; }
        public Nullable<bool> optLatestNewsEmails { get; set; }
        public Nullable<bool> optMarketingEmails { get; set; }

      

        public string Title { get; set; }

        public string DeleteConfirmationToken { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<ProfileQuestion> ProfileQuestions { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }

        public virtual ICollection<AspNetUsersNotificationToken> AspNetUsersNotificationTokens { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
       
        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<CompaniesAspNetUser> CompaniesAspNetUsers { get; set; }

        public virtual Country Country { get; set; }

        public Nullable<int> Phone1CodeCountryID { get; set; }


        public virtual Education Education { get; set; }

        public string PassportNo { get; set; }

        //public virtual City City { get; set; }
        //public virtual Country Country { get; set; }

        //city name and country name
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        
        #endregion

        #region Public

       

        #endregion
    }
}
