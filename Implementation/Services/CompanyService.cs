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

namespace SMD.Implementation.Services
{
    public class CompanyService : ICompanyService
    {
       #region Private

        private readonly ICompanyRepository companyRepository;
        private readonly IManageUserRepository _manageUserRepository;
        private readonly ICompanyBranchRepository _companyBranchRepository;
        private readonly IBranchCategoryRepository _branchCategoryRepository;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, IManageUserRepository managerUserRepository, ICompanyBranchRepository companyBranchRepository, IBranchCategoryRepository branchCategoryRepository)
        {
            this.companyRepository = companyRepository;
            this._manageUserRepository = managerUserRepository;
            this._companyBranchRepository = companyBranchRepository;
            this._branchCategoryRepository = branchCategoryRepository;
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

        public CompanyResponseModel GetCompanyDetails(int companyId = 0, string userId = "")
        {
            Company company = companyRepository.GetCompanyWithoutChilds(companyId);
            User loginUser = _manageUserRepository.GetLoginUser(userId);
            return new CompanyResponseModel
            {
                CompanyId = company.CompanyId,
                AboutUs = company.AboutUsDescription,
                BillingAddressLine1 = company.BillingAddressLine1,
                BillingAddressLine2 = company.BillingAddressLine2,
                BillingBusinessName = company.BillingAddressName,
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
                VatNumber = company.TaxRegNo,
                BranchesCount =  company.NoOfBranches??0,
                BusinessStartDate = company.CreationDateTime,
                Profession = loginUser.IndustryId??0,
                PassportNumber = loginUser.PassportNo

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

        public bool UpdateCompanyProfile(CompanyResponseModel company, byte[] logoImageBytes)
        {
            var currentCompany = companyRepository.GetCompanyWithoutChilds();
            if (currentCompany != null)
            {
                if (logoImageBytes != null)
                {
                    string smdContentPath = ConfigurationManager.AppSettings["SMD_Content"];
                    HttpServerUtility server = HttpContext.Current.Server;
                    string mapPath = server.MapPath(smdContentPath + "/Users/" + company.CompanyId);

                    // Create directory if not there
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }

                   currentCompany.Logo = ImageHelper.SaveImage(mapPath, company.Logo, string.Empty, string.Empty,
                        "blah", logoImageBytes, company.CompanyId);
                }
                if (!_companyBranchRepository.IsCompanyBranchExist())
                {
                    CreateNewCompanyBranch(company);
                }
                UpdatedCurrentCompany(company, currentCompany);
                UpdateUserProfile(company);
                companyRepository.SaveChanges();
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
            
            return target;
        }

        private void UpdateUserProfile(CompanyResponseModel source)
        {
            User currentUser = _manageUserRepository.GetLoginUser();
            currentUser.FullName = source.FirstName;
            currentUser.Gender = source.Solutation == 1 ? 1 : 2;
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
