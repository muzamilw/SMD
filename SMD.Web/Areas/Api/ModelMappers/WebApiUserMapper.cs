using System.Linq;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Web;
using SMD.MIS.ModelMappers;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using LoginResponse = SMD.Models.ResponseModels.LoginResponse;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// WebApi User Mapper
    /// </summary>
    public static class WebApiUserMapper
    {
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static WebApiUser CreateFrom(this SMD.Models.IdentityModels.User source)
        {

            bool isProfilecomplete = true;

            if (source.DOB.HasValue == false || source.Title == "" || source.Title == null || source.Phone1 == null || source.Phone1 == "")
            {
                isProfilecomplete = false;
            }

            var user = new WebApiUser
                   {
                       UserId = source.Id,
                       FullName = source.FullName,
                       Address1 = source.Company.AddressLine1,
                       CompanyName = source.Company.CompanyName,
                       Email = source.Email,
                       JobTitle = source.Jobtitle,
                       UserTimeZone = source.UserTimeZone,
                       Gender = source.Gender,
                       Address2 = source.Company.AddressLine2,
                       DOB =  source.DOB.HasValue ? source.DOB.Value.ToShortDateString() : "",
                       //CityId = source.Company.CityId,
                       ContactNotes = source.ContactNotes,
                       CountryId = source.Company.CountryId,
                       IndustryId = source.IndustryId,
                       Phone1 = source.Phone1,
                       Phone2 = source.Phone2,
                       State = source.Company.State,
                       ZipCode = source.Company.ZipCode,
                       ImageUrl = !string.IsNullOrEmpty(source.Company.Logo) ? HttpContext.Current.Request.Url.Scheme + "://" + 
                       HttpContext.Current.Request.Url.Host + "/" + source.ProfileImage + "?" + DateTime.Now : string.Empty,
                       AdvertContact = source.Company.CompanyName,
                       AdvertContactEmail = source.Company.ReplyEmail,
                       AdvertContactPhone = source.Company.Tel1,
                       EducationId = source.EducationId,
                       StripeId = source.Company.StripeCustomerId,
                       GoogleVallet = source.Company.GoogleWalletCustomerId,
                       PayPal = source.Company.PaypalCustomerId,
                       AccountBalance = CreateFromForAccount(source),
                       CityName = source.Company.City == null?null: source.Company.City,
                       CountryName = source.Company.Country == null?null: source.Company.Country.CountryName,
                       CompanyId = source.Company.CompanyId,
                       AuthenticationToken = source.AuthenticationToken,
                       Password = source.PasswordHash,
                       RoleId = source.Roles.Select(c => c.Id).FirstOrDefault(),
                       VoucherSecretKey = source.Company.VoucherSecretKey,
                       Phone1CodeCountryID = source.Phone1CodeCountryID.HasValue ? source.Phone1CodeCountryID.Value : 0,
                       Phone1Code = source.Country != null ? source.Country.CountryName + " " + source.Country.CountryPhoneCode : "",
                       ProfileComplete = isProfilecomplete
                   };

            return user;
        }


        public static WebApiUser CreateFromUserForMobile(this SMD.Models.IdentityModels.User source)
        {

            bool isProfilecomplete = true;

            if (source.DOB.HasValue == false || source.Title == "" || source.Title == null || source.Phone1 == null || source.Phone1 == "")
            {
                isProfilecomplete = false;
            }

            
            var user = new WebApiUser
            {
                UserId = source.Id,
                FullName = source.FullName,
            
                Email = source.Email,
             
                Gender = source.Gender.HasValue ? source.Gender.Value : 1,

                DOB = source.DOB.HasValue ? source.DOB.Value.ToString("dd-MMM-yyyy") : "",
          
                IndustryId = source.IndustryId.HasValue ? source.IndustryId : 0,

                IndustryName = source.Industry != null ? source.Industry.IndustryName : "",

                GenderString = source.Gender == 1 ? "Male" : "Female",

                Phone1 = string.IsNullOrEmpty(source.Phone1) ? "" : source.Phone1,
         
                ImageUrl = !string.IsNullOrEmpty(source.Company.Logo) ? HttpContext.Current.Request.Url.Scheme + "://" +
                HttpContext.Current.Request.Url.Host + "/" + source.ProfileImage + "?" + DateTime.Now : string.Empty,
                Title = source.Title == null ? "": source.Title,
                Phone1CodeCountryID = source.Phone1CodeCountryID.HasValue == true ?  source.Phone1CodeCountryID.Value : 0,
                Phone1Code = source.Country != null? source.Country.CountryName + " " + source.Country.CountryPhoneCode : "",
                ProfileComplete = isProfilecomplete
               
            };

            return user;
        }

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static UserBalanceInquiryResponse CreateFromForBalance(this LoginResponse source)
        {
            return new UserBalanceInquiryResponse
            {
                Status = source.Status,
                Message = source.Message,
                Balance = CreateFromForAccount(source.User)
            };
        }
        //public static StatementInquiryResponse CreateFromForStatementBalance(this LoginResponse source)
        //{
        //    return new StatementInquiryResponse
        //    {
        //        Status = source.Status,
        //        Message = source.Message,
        //        Balance = CreateFromForAccount(source.User),
        //        Transactions = CreateFromForTransactionAccount(source.User)
        //    };
        //}

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static double? CreateFromForAccount(this SMD.Models.IdentityModels.User source)
        {
            if (source.Company.Accounts == null || !source.Company.Accounts.Any())
            {
                return null;
            }

            Account account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
            if (account == null)
            {
                return null;
            }

            return account.AccountBalance;
        }
        ////public static List<StatementTrasaction> CreateFromForTransactionAccount(this SMD.Models.IdentityModels.User source)
        ////{
        ////    List<Transaction> transactions = new List<Transaction>();
        ////    List<StatementTrasaction> statements = new List<StatementTrasaction>();
        ////    if (source.Company.Accounts == null || !source.Company.Accounts.Any())
        ////    {
        ////        return null;
        ////    }

        ////    Account account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.GoogleWallet);
        ////    if (account != null)
        ////    {
        ////        transactions.AddRange(account.Transactions);
        ////    }
        ////    account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Paypal);
        ////    if (account != null)
        ////    {
        ////        transactions.AddRange(account.Transactions);
        ////    }
        ////    account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.Stripe);
        ////    if (account != null)
        ////    {
        ////        transactions.AddRange(account.Transactions);
        ////    }

        ////    account = source.Company.Accounts.FirstOrDefault(acc => acc.AccountType == (int)AccountType.VirtualAccount);
        ////    if (account != null)
        ////    {
        ////        transactions.AddRange(account.Transactions);
        ////    }




        ////    transactions = transactions.OrderByDescending(g => g.TxId).ToList();
        ////    foreach (var item in transactions)
        ////    {
        ////        statements.Add(item.CreateFrom());
        ////    }
        ////    return statements;
        ////}
        ////public static StatementTrasaction CreateFrom(this Transaction source)
        ////{
        ////    string accName = "";
        ////    if (source.Account.AccountType == (int)AccountType.Stripe)
        ////        accName = "Stripe";
        ////    else if (source.Account.AccountType == (int)AccountType.Paypal)
        ////        accName = "Paypal";
        ////    else if (source.Account.AccountType == (int)AccountType.GoogleWallet)
        ////        accName = "Google Wallet";
        ////    else if (source.Account.AccountType == (int)AccountType.VirtualAccount)
        ////        accName = "Centz";

        ////    string TransactionDetails = "";
        ////    if ( source.Type == 1)
        ////    {
        ////        TransactionDetails = "Ad Viewed :" + source.AdCampaignId.ToString();
        ////    }

        ////    return new StatementTrasaction
        ////    {
        ////       DebitAmount  = source.DebitAmount,
        ////       CreditAmount = source.CreditAmount,
        ////        Date = source.TransactionDate.ToString(),
        ////       PaymentMethod = accName,
        ////       TransactionDetails = TransactionDetails
        ////        //AuthenticationToken = Guid.NewGuid()
        ////    };
        ////}
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static Models.LoginResponse CreateFrom(this LoginResponse source)
        {
            return new Models.LoginResponse
            {
                Status = source.Status,
                Message = source.Message,
                User = source.User != null ? source.User.CreateFrom() : null//,
                //AuthenticationToken = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Base Data Create From Domin mOdels
        /// </summary>
        public static UserProfileBaseResponse CreateFrom(this UserProfileBaseResponseModel source)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select(timeZoneInfo => new TimeZoneDropDown
            {
                TimeZoneId = timeZoneInfo.Id,
                TimeZoneName = timeZoneInfo.BaseUtcOffset + "  [ " + timeZoneInfo.Id + " ]"
            }).ToList();

            return new UserProfileBaseResponse
            {
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                //CityDropDowns = source.Cities.Select(city=>city.CreateFrom()),
                IndusteryDropdowns = source.Industries.Select(industery => industery.CreateForDd()),
                EducationDropdowns = source.Educations.Select(edu => edu.CreateFromDd()),
                UserRoles = source.UserRoles.Select(role => role.CreateFromDd()),
                TimeZoneDropDowns = timeZones,
                GetApprovalCount = source.GetApprovalCount.CreateFrom(),
                GetCouponReviewCount = source.GetCouponreviewCount
                
            };
        }

        private static SMD.MIS.Areas.Api.Models.GetApprovalCount_Result CreateFrom(this SMD.Models.DomainModels.GetApprovalCount_Result source)
        {
            return new Models.GetApprovalCount_Result
            {
                AdCmpaignCount = source.AdCmpaignCount,
                CouponCount =source.CouponCount,
                DisplayAdCount = source.DisplayAdCount,
                SurveyQuestionCount = source.SurveyQuestionCount,
                ProfileQuestionCount = source.ProfileQuestionCount
            };
        }
    }
}