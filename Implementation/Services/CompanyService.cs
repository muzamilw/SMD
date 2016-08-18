using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion
    }
}
