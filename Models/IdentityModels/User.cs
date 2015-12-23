using System;
using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

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
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AlternateEmail { get; set; }
        public string IsEmailVerified { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Jobtitle { get; set; }
        public string ContactNotes { get; set; }
        public bool IsSubscribed { get; set; }
        public int? AppId { get; set; }
        public string CompanyName { get; set; }
        public string SalesEmail { get; set; } // till here 
        public string CompanyRepresentative { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserTimeZone { get; set; }
        public string ReferralCode { get; set; }
        public bool? AfilliatianStatus { get; set; }
        public string StripeCustomerId { get; set; }
        public string ChargeBeesubscriptionId { get; set; }
        public bool? RegisteredViaReferral { get; set; }
        public string ReferringUserId { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int? LanguageId { get; set; }
        public int? IndustryId { get; set; }

        public virtual Industry Industry { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<ProfileQuestionUserAnswer> ProfileQuestionUserAnswers { get; set; }
        public virtual ICollection<AdCampaignResponse> AdCampaignResponses { get; set; }
        public virtual ICollection<ProfileQuestion> ProfileQuestions { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        #endregion

        #region Public

        /// <summary>
        /// Update user
        /// </summary>
        public void Update(UpdateUserProfileRequest source)
        {
            if (source == null)
            {
                return;
            }

            // Update only the ones that changed
            if (!string.IsNullOrEmpty(source.Phone1))
            {
                Phone1 = source.Phone1;
            }

            if (!string.IsNullOrEmpty(source.Phone2))
            {
                Phone2 = source.Phone2;
            }

            if (!string.IsNullOrEmpty(source.Address1))
            {
                Address1 = source.Address1;
            }

            if (!string.IsNullOrEmpty(source.Address2))
            {
                Address2 = source.Address2;
            }

            if (!string.IsNullOrEmpty(source.JobTitle))
            {
                Jobtitle = source.JobTitle;
            }

            if (!string.IsNullOrEmpty(source.CompanyName))
            {
                CompanyName = source.CompanyName;
            }

            if (!string.IsNullOrEmpty(source.ContactNotes))
            {
                ContactNotes = source.ContactNotes;
            }

            if (!string.IsNullOrEmpty(source.State))
            {
                State = source.State;
            }

            if (!string.IsNullOrEmpty(source.ZipCode))
            {
                ZipCode = source.ZipCode;
            }

            if (source.Age.HasValue)
            {
                Age = source.Age;
            }

            if (source.CityId.HasValue)
            {
                CityId = source.CityId;
            }

            if (source.CountryId.HasValue)
            {
                CountryId = source.CountryId;
            }

            if (source.Gender.HasValue)
            {
                Gender = source.Gender;
            }

            if (source.IndustryId.HasValue)
            {
                IndustryId = source.IndustryId;
            }

            if (!string.IsNullOrEmpty(source.FullName))
            {
                FullName = source.FullName;
            }
        }

        #endregion
    }
}
