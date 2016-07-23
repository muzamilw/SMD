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
        private readonly IWebApiUserService userService;
        private readonly ICompanyService companyService;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManageUserService(IManageUserRepository managerUserRepository, IWebApiUserService userService, ICompanyService companyService)
        {
            this.managerUserRepository = managerUserRepository;
            this.userService = userService;
            this.companyService = companyService;
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


        public List<vw_CompanyUsers> GetCompaniesByUserId(string UserId)
        {
            //special case of getting the main company of this user
            var usr = userService.GetUserByUserId(UserId);

            var company = companyService.GetCompanyById(usr.CompanyId.Value);

            var result = managerUserRepository.GetCompaniesByUserId(UserId);

            //addding the main company to the result.
            var rec = new vw_CompanyUsers{
        id = -999,
        companyid = company.CompanyId,
        CompanyName = company.CompanyName,
        CreatedOn = usr.CreatedDateTime.HasValue == true ? usr.CreatedDateTime.Value:DateTime.Now,
        email = usr.Email,
        FullName = usr.FullName,
        RoleName = "Administrator",
        status = "active",
        UserId = UserId
            
        };

            result.Add(rec);

            return result;
        }
       
        #endregion
    }
}
