﻿using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        #region Private

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Company> DbSet
        {
            get { return db.Companies; }
        }
        #endregion


        #region Public


        public Company Find(int id)
        {
            return DbSet.Where(g => g.CompanyId == id).SingleOrDefault();
        }

        public int GetUserCompany(string userId)
        {
            var user = db.Users.Where(g => g.Id == userId).SingleOrDefault();
            if (user == null || user.CompanyId.HasValue == false)
                return 0;
            return user.CompanyId.Value;
        }
        public bool updateCompanyLogo(string url, int companyId)
        {
            var company = db.Companies.Where(g => g.CompanyId == companyId).FirstOrDefault();
            if (company != null)
            {
                company.Logo = url;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public int createCompany(string userId, string email, string fullname, string guid)
        {
            int UserCompanyId = 0;

            Company company = new Company();
            company.ReplyEmail = email;
            company.CompanyName = fullname;
            company.ReferralCode = Guid.NewGuid().ToString();
            company.Status = 1;
            db.Companies.Add(company);
            db.SaveChanges();
            UserCompanyId = company.CompanyId;


            var user = db.Users.Where(g => g.Id == userId).SingleOrDefault();
            if (user != null)
            {
                user.CompanyId = UserCompanyId;
                user.AuthenticationToken = guid;
                db.SaveChanges();
            }
            return UserCompanyId;
        }
        public bool updateCompany(Company request)
        {
            var company = db.Companies.Where(g => g.CompanyId == request.CompanyId).SingleOrDefault();
            if (company != null)
            {

                company.CompanyName = request.CompanyName;
                company.Tel1 = request.Tel1;
                company.Tel2 = request.Tel2;

                company.StripeCustomerId = request.StripeCustomerId;
                company.SalesEmail = request.SalesEmail;
                company.WebsiteLink = request.WebsiteLink;
                company.VoucherSecretKey = request.VoucherSecretKey;
                company.BillingAddressLine1 = request.BillingAddressLine1;
                company.BillingAddressLine2 = request.BillingAddressLine2;
                company.BillingState = request.BillingState;
                company.BillingCountryId = request.BillingCountryId;
                company.BillingCity = request.BillingCity;
                company.BillingZipCode = request.BillingZipCode;
                company.BillingPhone = request.BillingPhone;
                company.BillingEmail = request.BillingEmail;
                company.TwitterHandle = request.TwitterHandle;
                company.FacebookHandle = request.FacebookHandle;
                company.InstagramHandle = request.InstagramHandle;
                company.PinterestHandle = request.PinterestHandle;


                company.StripeCustomerId = request.StripeCustomerId;
                company.StripeSubscriptionId = request.StripeSubscriptionId;
                company.StripeSubscriptionStatus = request.StripeSubscriptionStatus;


                db.SaveChanges();
                return true;
            }
            return false;
        }
        public User getUserBasedOnAuthenticationToken(string token)
        {
            return db.Users.Where(g => g.AuthenticationToken == token).SingleOrDefault();

        }
        public string GetCompanyNameByID(int CompanyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.Where(c => c.CompanyId == CompanyId).FirstOrDefault().CompanyName;
        }
        public Company GetCompanyById()
        {
            return DbSet.Where(g => g.CompanyId == CompanyId).SingleOrDefault();
        }

        public Company GetCompanyWithoutChilds(int companyId = 0)
        {
            int compId = companyId > 0 ? companyId : CompanyId;
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.FirstOrDefault(c => c.CompanyId == compId);
        }
        public List<vw_ReferringCompanies> GetReferralCompaniesByCID()
        {
            return db.vwvw_ReferringCompanies.Where(re => re.ReferringCompanyID == CompanyId).ToList();
        }
        public bool updateCompanyForProfile(CompanyResponseModel RequestData, Company Target)
        {
            var target = db.Companies.Where(g => g.CompanyId == Target.CompanyId).SingleOrDefault();

            if (target != null)
            {

                target.CompanyName = RequestData.CompanyName;
                target.AboutUsDescription = RequestData.AboutUs;
                target.BillingAddressLine1 = RequestData.BillingAddressLine1;
                target.BillingAddressLine2 = RequestData.BillingAddressLine2;
                target.BillingAddressName = RequestData.BillingBusinessName;
                target.BillingCity = RequestData.BillingCity;
                target.BillingCountryId = RequestData.BillingCountryId;
                target.BillingEmail = RequestData.BillingEmail;
                target.BillingPhone = RequestData.BillingPhone;
                target.BillingState = RequestData.BillingState;
                target.BillingZipCode = RequestData.BillingZipCode;
                target.NoOfBranches = RequestData.BranchesCount;
                target.CompanyRegNo = RequestData.CompanyRegistrationNo;
                target.CompanyType = RequestData.CompanyType;
                target.CreationDateTime = RequestData.BusinessStartDate;
                target.PaypalCustomerId = RequestData.PayPalId;
                target.StripeCustomerId = RequestData.StripeCustomerId;
                target.SalesEmail = RequestData.SalesEmail;
                target.WebsiteLink = RequestData.WebsiteLink;
                target.TaxRegNo = RequestData.VatNumber;
                target.Tel1 = RequestData.SalesPhone;
                target.CompanyType = RequestData.CompanyType;

                db.Companies.Attach(target);

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();


            }
            return true;

        #endregion

        }

        public GetApprovalCount_Result GetApprovalCount()
        {
            var appCount = db.GetApprovalCount();
            return appCount != null ? appCount.FirstOrDefault() : null;
        }

        public Boolean UpdateCompanyStatus(int status, string userId, string comments, int companyId)
        {
            db.UpdateCompanyStatus(status, userId, comments, companyId);
            return true;
        }



        public Company GetCompanyByStripeCustomerId(string StripeCustomerId)
        {
            return DbSet.Where(g => g.StripeCustomerId == StripeCustomerId).SingleOrDefault();
        }

        public List<Dashboard_analytics_Result> GetDashboardAnalytics(string UserID)
        {

            return db.GetMainDashboardAnalytics(UserID).ToList();

          
        }

        public Dictionary<string, int> GetStatusesCounters()
        {
            Dictionary<string, int> StDictionary = new Dictionary<string, int>();
            StDictionary.Add("liveVidCamp", db.AdCampaigns.Where(i => i.Status == 3 && i.Type ==1 && i.UserId==this.LoggedInUserIdentity).ToList().Count);
            StDictionary.Add("liveDisplayCamp", db.AdCampaigns.Where(i => i.Status == 3 && i.Type == 4 && i.UserId == this.LoggedInUserIdentity).ToList().Count);
            StDictionary.Add("Deals", db.Coupons.Where(i => i.Status == 3 && i.UserId == this.LoggedInUserIdentity).ToList().Count);
            StDictionary.Add("Polls", db.SurveyQuestions.Where(i => i.Status == 3 && i.UserId == this.LoggedInUserIdentity).ToList().Count);

            return StDictionary;
        }
        public Company GetCompanyInfo()
        {
            return db.Companies.Where(i => i.CompanyId == this.CompanyId).FirstOrDefault();
        }
        public CompanySubscription GetCompanySubscription()
        {
            db.Configuration.LazyLoadingEnabled = false;
            CompanySubscription obj = new CompanySubscription();
            var query = from company in db.Companies where company.CompanyId ==CompanyId  select new { company.StripeSubscriptionId, company.StripeCustomerId, company.StripeSubscriptionStatus };
            obj.StripeCustomerId = query.FirstOrDefault().StripeCustomerId;
            obj.StripeSubscriptionId = query.FirstOrDefault().StripeSubscriptionId;
            obj.StripeSubscriptionStatus = query.FirstOrDefault().StripeSubscriptionStatus;
            return obj;
        }
    }
}
