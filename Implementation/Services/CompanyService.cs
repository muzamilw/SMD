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

namespace SMD.Implementation.Services
{
    public class CompanyService : ICompanyService
    {
       #region Private

        private readonly ICompanyRepository companyRepository;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
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
