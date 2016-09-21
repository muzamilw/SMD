using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class ActiveUser : IActiveUser
    {
        #region Private

        private readonly ICompanyAspNetUsersRepository companyAspNetUsersRepository;


        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ActiveUser(ICompanyAspNetUsersRepository _companyAspNetUsersRepository)
        {
            this.companyAspNetUsersRepository = _companyAspNetUsersRepository;
        }

        #endregion


        public IEnumerable<GetActiveVSNewUsers_Result> GetActiveVSNewUsers()
        {
            return companyAspNetUsersRepository.GetActiveVSNewUsers();
        }
    }
}
