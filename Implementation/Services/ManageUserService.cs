using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.IdentityModels;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    public class ManageUserService : IManageUserService
    {
         #region Private

        private readonly IManageUserRepository managerUserRepository;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManageUserService(IManageUserRepository managerUserRepository)
        {
            this.managerUserRepository = managerUserRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public List<vw_CompanyUsers> GetManageUsersList(int CompanyId)
        {
            return managerUserRepository.getManageUsers(CompanyId);
        }
       
        #endregion
    }
}
