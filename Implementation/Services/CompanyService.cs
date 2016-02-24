using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
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
        #endregion
    }
}
