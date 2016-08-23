using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
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
            if(company != null)
            {

              company.CompanyName= request.CompanyName;
              company.Tel1= request.Tel1;
              company.Tel2= request.Tel2;
              
              company.StripeCustomerId= request.StripeCustomerId;
              company.SalesEmail= request.SalesEmail;
              company.WebsiteLink= request.WebsiteLink;
              company.VoucherSecretKey= request.VoucherSecretKey;
              company.BillingAddressLine1= request.BillingAddressLine1;
              company.BillingAddressLine2= request.BillingAddressLine2;
              company.BillingState= request.BillingState;
              company.BillingCountryId= request.BillingCountryId;
              company.BillingCityId= request.BillingCityId;
              company.BillingZipCode= request.BillingZipCode;
              company.BillingPhone= request.BillingPhone;
              company.BillingEmail= request.BillingEmail;
              company.TwitterHandle= request.TwitterHandle;
              company.FacebookHandle= request.FacebookHandle;
              company.InstagramHandle= request.InstagramHandle;
              company.PinterestHandle= request.PinterestHandle;


                db.SaveChanges();
                return true;
            }
            return false;
        }
        public User getUserBasedOnAuthenticationToken(string token)
        {
            return db.Users.Where(g => g.AuthenticationToken == token).SingleOrDefault();
            
        }
        #endregion
    }
}
