using SMD.Common;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SMD.Models.ResponseModels;

namespace SMD.Implementation.Services
{
    public class CompanyService : ICompanyService
    {
       #region Private

        private readonly ICompanyRepository companyRepository;
        private readonly IManageUserRepository _manageUserRepository;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, IManageUserRepository managerUserRepository)
        {
            this.companyRepository = companyRepository;
            this._manageUserRepository = managerUserRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public int GetUserCompany(string userId)
        {
           return companyRepository.GetUserCompany(userId);
        }


        public int createCompany(string userId, string email, string fullName, string guid)
        {
            return companyRepository.createCompany(userId, email, fullName, guid);
        }


        public Company GetCompanyById(int CompanyId)
        {
            return companyRepository.Find(CompanyId);
        }


        public Company GetCurrentCompany()
        {
            return companyRepository.Find(companyRepository.CompanyId);
        }

        public CompanyResponseModel GetCompanyDetails()
        {
            Company company = companyRepository.GetCompanyWithoutChilds();
            User loginUser = _manageUserRepository.GetLoginUser();
            return new CompanyResponseModel
            {
                CompanyId = company.CompanyId,
                AboutUs = company.AboutUsDescription,
                BillingAddressLine1 = company.BillingAddressLine1,
                BillingAddressLine2 = company.BillingAddressLine2,
                BillingBusinessName = company.CompanyName,
                BillingCity = company.BillingCity,
                BillingCountryId = company.BillingCountryId,
                BillingEmail = company.BillingEmail,
                BillingPhone = company.BillingPhone,
                BillingState = company.BillingState,
                BillingZipCode = company.BillingZipCode,
                CompanyName = company.CompanyName,
                CompanyRegistrationNo = company.CompanyRegNo,
                CompanyType = company.CompanyType??0,
                StripeCustomerId = company.StripeCustomerId,
                PayPalId =  company.PaypalCustomerId,
                FirstName = loginUser.FullName,
                Email = loginUser.Email,
                Solutation = loginUser.Gender == 1? 1 : 2,
                Mobile = loginUser.Phone1,
                DateOfBirth = loginUser.DOB,
                IsReceiveDeals = loginUser.optDealsNearMeEmails??false,
                IsReceiveLatestServices = loginUser.optLatestNewsEmails??false,
                IsReceiveWeeklyUpdates = loginUser.optMarketingEmails??false,
                Logo = company.Logo,
                WebsiteLink = company.WebsiteLink,
                SalesEmail = company.SalesEmail,
                SalesPhone = company.Tel1,
                VatNumber = company.TaxRegNo

            };
        }




        public bool UpdateCompany(Company company, byte[] LogoImageBytes)
        {




            if (company.Logo != null && company.Logo!=string.Empty)
            {

                var currentCompany = companyRepository.Find(companyRepository.CompanyId);

                var logoUrl = UpdateCompanyProfileImage(company, LogoImageBytes, currentCompany.Logo);

                companyRepository.updateCompanyLogo(logoUrl, companyRepository.CompanyId);

            }

            companyRepository.updateCompany(company);
            


            return true;

        }


        private string UpdateCompanyProfileImage(Company request, byte[] LogoImageBytes, string existingFileName)
        {
            string smdContentPath = ConfigurationManager.AppSettings["SMD_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(smdContentPath + "/Users/" + request.CompanyId);

            // Create directory if not there
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            return ImageHelper.SaveImage(mapPath,request.Logo, string.Empty, string.Empty,
                "blah", LogoImageBytes,request.CompanyId);

        }
        public Company GetCompanyForAddress()
        {
            return companyRepository.GetCompanyById();
        }
        #endregion
    }
}
