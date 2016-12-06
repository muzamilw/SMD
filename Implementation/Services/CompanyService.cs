using System.Collections.ObjectModel;
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
using SMD.Models.RequestModels;

namespace SMD.Implementation.Services
{
    public class CompanyService : ICompanyService
    {
        #region Private

        private readonly ICompanyRepository companyRepository;
        private readonly IManageUserRepository _manageUserRepository;
        private readonly ICompanyBranchRepository _companyBranchRepository;
        private readonly IBranchCategoryRepository _branchCategoryRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IAspnetUsersRepository _IAspnetUsersRepository;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, IManageUserRepository managerUserRepository, ICompanyBranchRepository companyBranchRepository, IBranchCategoryRepository branchCategoryRepository,
            ICountryRepository countryRepository, IAspnetUsersRepository _IAspnetUsersRepository)
        {
            this.companyRepository = companyRepository;
            this._manageUserRepository = managerUserRepository;
            this._companyBranchRepository = companyBranchRepository;
            this._branchCategoryRepository = branchCategoryRepository;
            this._countryRepository = countryRepository;
            this._IAspnetUsersRepository = _IAspnetUsersRepository;
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

        public Company GetCompanyByStripeCustomerId(string StripeCustomerId)
        {
            return companyRepository.GetCompanyByStripeCustomerId(StripeCustomerId);
        }


        public Company GetCurrentCompany()
        {
            return companyRepository.Find(companyRepository.CompanyId);
        }

        public CompanyResponseModel GetCompanyDetails(int companyId = 0, string userId = "")
        {
            Company company = companyRepository.GetCompanyWithoutChilds(companyId);
            User loginUser = null;

            if (userId.Equals("abc"))
            {
                loginUser = _manageUserRepository.GetLoginUser(_manageUserRepository.LoggedInUserIdentity);
            }
            else
            {
                 loginUser = _manageUserRepository.GetLoginUser(userId);
            }
            var defaultBranch = _companyBranchRepository.GetDefaultCompanyBranch(companyId);
            var currencyCode = _countryRepository.GetCurrencyCode(company.BillingCountryId!=null?company.BillingCountryId ?? 0:1);

            return new CompanyResponseModel
            {
                CompanyId = company.CompanyId,
                AboutUs = company.AboutUsDescription,
                CompanyAddressline1=company.AddressLine1,
                Companyphone1=company.Tel1,

                BillingAddressLine1 = defaultBranch != null ? defaultBranch.BranchAddressLine1 : string.Empty,
                BillingAddressLine2 = defaultBranch != null ? defaultBranch.BranchAddressLine2 : string.Empty,
                BillingBusinessName = company.BillingAddressName,
                BillingCity = defaultBranch != null ? defaultBranch.BranchCity : string.Empty,
                BillingCountryId = company != null ? company.BillingCountryId : null,
                BillingEmail = company.BillingEmail,
                BillingPhone = company.BillingPhone,
                BillingState = defaultBranch != null ? defaultBranch.BranchState : string.Empty,
                BillingZipCode = defaultBranch != null ? defaultBranch.BranchZipCode : string.Empty,
                BranchName = defaultBranch != null ? defaultBranch.BranchTitle : string.Empty,
                CompanyName = company.CompanyName,
                CompanyRegistrationNo = company.CompanyRegNo,
                CompanyType = company.CompanyType ?? 0,
                StripeCustomerId = company.StripeCustomerId,
                PayPalId = company.PaypalCustomerId,
                FirstName = loginUser.FullName,
                Gender = loginUser.Gender,
                Email = loginUser.Email,
                Solutation = loginUser.Title == "Mr." ? 1 : (loginUser.Title == "Ms." ? 2 : 3),
                Mobile = loginUser.Phone1,
                DateOfBirth = loginUser.DOB,
                IsReceiveDeals = loginUser.optDealsNearMeEmails ?? false,
                IsReceiveLatestServices = loginUser.optLatestNewsEmails ?? false,
                IsReceiveWeeklyUpdates = loginUser.optMarketingEmails ?? false,
                Logo = company.Logo,
                WebsiteLink = company.WebsiteLink,
                SalesEmail = company.SalesEmail,
                SalesPhone = company.Tel1,
                VatNumber = company.TaxRegNo,
                BranchesCount = company.NoOfBranches ?? 0,
                BusinessStartDate = company.CreationDateTime,
                Profession = loginUser.IndustryId ?? 0,
                PassportNumber = loginUser.PassportNo,
                UserId = loginUser.Id,
                BillingCountryName = _countryRepository.GetCountryNameById(defaultBranch != null ? defaultBranch.CountryId ?? 0 : 0),
                BranchId = defaultBranch != null ? defaultBranch.BranchId : 0,
                CurrencyID = currencyCode
            };
        }




        public bool UpdateCompany(Company company, byte[] LogoImageBytes)
        {




            if (company.Logo != null && company.Logo != string.Empty)
            {

                var currentCompany = companyRepository.Find(companyRepository.CompanyId);

                var logoUrl = UpdateCompanyProfileImage(company, LogoImageBytes, currentCompany.Logo);

                companyRepository.updateCompanyLogo(logoUrl, companyRepository.CompanyId);

            }

            companyRepository.updateCompany(company);



            return true;

        }

        public bool UpdateCompany(Company company)
        {

            companyRepository.updateCompany(company);

            return true;

        }


        public bool UpdateCompanyProfile(CompanyResponseModel requestData, byte[] logoImageBytes)
        {
            var currentCompany = companyRepository.GetCompanyWithoutChilds();
            if (currentCompany != null)
            {

                string smdContentPath = ConfigurationManager.AppSettings["SMD_Content"];
                HttpServerUtility server = HttpContext.Current.Server;
                string mapPath = server.MapPath(smdContentPath + "/Users/" + requestData.CompanyId);

                // Create directory if not there
                if (!Directory.Exists(mapPath))
                {
                    Directory.CreateDirectory(mapPath);
                }

                if (requestData.LogoChanged)
                    currentCompany.Logo = ImageHelper.SaveImage(mapPath, requestData.Logo, string.Empty, string.Empty,
                         "blah", logoImageBytes, requestData.CompanyId);



                var defaultBranch = _companyBranchRepository.GetDefaultCompanyBranch();
                if (defaultBranch == null)
                {
                    CreateNewCompanyBranch(requestData);
                }
                else
                {
                    updateCompanyBranch(defaultBranch, requestData);
                }

                //UpdatedCurrentCompany(requestData, currentCompany);


                companyRepository.updateCompanyForProfile(requestData, currentCompany);

                UpdateUserProfile(requestData);


            }
            return true;
        }

        private Company UpdatedCurrentCompany(CompanyResponseModel source, Company target)
        {

            target.CompanyName = source.CompanyName;
            target.AboutUsDescription = source.AboutUs;
            target.BillingAddressLine1 = source.BillingAddressLine1;
            target.BillingAddressLine2 = source.BillingAddressLine2;
            target.BillingAddressName = source.BillingBusinessName;
            target.BillingCity = source.BillingCity;
            target.BillingCountryId = source.BillingCountryId;
            target.BillingEmail = source.BillingEmail;
            target.BillingPhone = source.BillingPhone;
            target.BillingState = source.BillingState;
            target.BillingZipCode = source.BillingZipCode;
            target.NoOfBranches = source.BranchesCount;
            target.CompanyRegNo = source.CompanyRegistrationNo;
            target.CompanyType = source.CompanyType;
            target.CreationDateTime = source.BusinessStartDate;
            target.PaypalCustomerId = source.PayPalId;
            target.StripeCustomerId = source.StripeCustomerId;
            target.SalesEmail = source.SalesEmail;
            target.WebsiteLink = source.WebsiteLink;
            target.TaxRegNo = source.CompanyRegistrationNo;
            target.Tel1 = source.SalesPhone;
            target.CountryId = source.BillingCountryId;

            return target;
        }
        private CompanyBranch CreateNewCompanyBranch(CompanyResponseModel source)
        {
            long branchCategoryId = _branchCategoryRepository.ExistedCompanyBranchCategory();
            if (branchCategoryId > 0)
            {
                CompanyBranch itemTarget = _companyBranchRepository.Create();
                itemTarget = updateCompanyBranch(itemTarget, source);
                itemTarget.BranchCategoryId = branchCategoryId;
                _companyBranchRepository.Add(itemTarget);
                _companyBranchRepository.SaveChanges();
                return itemTarget;
            }
            else
            {
                BranchCategory category = _branchCategoryRepository.Create();
                category.BranchCategoryName = "Default Category";
                category.CompanyBranches = new Collection<CompanyBranch>();
                CompanyBranch itemTarget = _companyBranchRepository.Create();
                itemTarget = updateCompanyBranch(itemTarget, source);
                category.CompanyBranches.Add(itemTarget);
                _branchCategoryRepository.Add(category);
                _branchCategoryRepository.SaveChanges();
                return itemTarget;
            }

        }

        private CompanyBranch updateCompanyBranch(CompanyBranch target, CompanyResponseModel source)
        {

            target.BranchCity = source.BillingCity;
            target.BranchAddressLine1 = source.BillingAddressLine1;
            target.BranchAddressLine2 = source.BillingAddressLine2;
            target.BranchState = source.BillingState;
            target.BranchZipCode = source.BillingZipCode;
            target.BranchPhone = source.BillingPhone;
            target.CountryId = source.BillingCountryId;
            target.BranchTitle = source.BranchName;
            target.BranchLocationLat = source.BranchLocationLat;
            target.BranchLocationLong = source.BranchLocationLong;
            target.CompanyId = source.CompanyId;

            return target;
        }

        private void UpdateUserProfile(CompanyResponseModel source)
        {

            User currentUser = _manageUserRepository.GetLoginUser();

            if (source.Solutation == 1)
            {
                currentUser.Title = "Mr.";
                currentUser.Gender = 1;
            }
            else if (source.Solutation == 2)
            {
                currentUser.Title = "Ms.";
                currentUser.Gender = 2;
            }
            else if (source.Solutation == 3)
            {
                currentUser.Title = "Mrs.";
                currentUser.Gender = 2;
            }

            currentUser.FullName = source.FirstName;

            // currentUser.Email = source.Email;
            currentUser.DOB = source.DateOfBirth;
            currentUser.IndustryId = source.Profession;
            currentUser.Phone1 = source.Mobile;
            currentUser.PassportNo = source.PassportNumber;
            currentUser.optDealsNearMeEmails = source.IsReceiveDeals;
            currentUser.optLatestNewsEmails = source.IsReceiveLatestServices;
            currentUser.optMarketingEmails = source.IsReceiveWeeklyUpdates;

            _manageUserRepository.SaveChanges();

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

            return ImageHelper.SaveImage(mapPath, request.Logo, string.Empty, string.Empty,
                "blah", LogoImageBytes, request.CompanyId);

        }
        public Company GetCompanyForAddress()
        {
            return companyRepository.GetCompanyById();
        }
        public List<vw_ReferringCompanies> GetRefferalComponiesByCid()
        {
            return companyRepository.GetReferralCompaniesByCID();
        }

        public RegisteredUsersResponseModel GetRegisterdUsers(RegisteredUsersSearchRequest request)
        {
            int rowCount;
            return new RegisteredUsersResponseModel
            {
                RegisteredUser = _IAspnetUsersRepository.GetRegisteredUsers(request, out rowCount),
                TotalCount = rowCount

            };
        }

        public Boolean UpdateCompanyStatus(int status, string userId, string comments, int companyId)
        {
            companyRepository.UpdateCompanyStatus(status, userId, comments, companyId);
            return true;
        }

        public List<Dashboard_analytics_Result> GetDashboardAnalytics(string UserID)
        {
           return  companyRepository.GetDashboardAnalytics(UserID);
        }
        public Dictionary<string, int> GetStatusesCounters()
        {
            return companyRepository.GetStatusesCounters();
        
        }
        public Company GetCompanyInfo()
        {
            return companyRepository.GetCompanyInfo();
        }
         
        #endregion
    }
}
