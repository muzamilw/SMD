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
        public bool createCompany(string userId, string email, string fullname, string guid)
        {
            int UserCompanyId = 0;
           
                Company company = new Company();
                company.ReplyEmail = email;
                company.CompanyName = fullname;
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
            return true;
        }
        public bool updateCompany(UpdateUserProfileRequest request)
        {
            var company = db.Companies.Where(g => g.CompanyId == request.companyId).SingleOrDefault();
            if(company != null)
            {
                company.CompanyName = request.CompanyName;
                company.AddressLine1 = request.Address1;
                company.AddressLine2 = request.Address2;
                company.CityId = request.CityId;
                company.CountryId = request.CountryId;
                company.State = request.State;
                company.ZipCode = request.ZipCode;
                company.Tel1 = request.AdvertContactPhone;
                company.ReplyEmail = request.AdvertContactEmail;
                company.PaypalCustomerId = request.PayPal;
                company.PreferredPayoutAccount = 1;
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
