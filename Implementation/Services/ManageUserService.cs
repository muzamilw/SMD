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
        private readonly ICompanyAspNetUsersRepository companyAspNetUsersRepository;
        private IEmailManagerService emailManagerService;

        #endregion 
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManageUserService(IManageUserRepository managerUserRepository, IWebApiUserService userService, ICompanyService companyService, ICompanyAspNetUsersRepository companyAspNetUsersRepository, IEmailManagerService emailManagerService)
        {
            this.managerUserRepository = managerUserRepository;
            this.userService = userService;
            this.companyService = companyService;
            this.companyAspNetUsersRepository = companyAspNetUsersRepository;
            this.emailManagerService = emailManagerService;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public List<vw_CompanyUsers> GetManageUsersList()
        {
            return managerUserRepository.getManageUsers();
        }


        public List<vw_CompanyUsers> GetCompaniesByUserId(string UserId)
        {
            //special case of getting the main company of this user
            var usr = userService.GetUserByUserId(UserId);

            if (usr == null)
            {
                throw new Exception("Catestrophic errror, user is null. with userid = " + UserId);
            }

            var company = companyService.GetCompanyById(usr.CompanyId.Value);

            var result = managerUserRepository.GetCompaniesByUserId(UserId);


            if (company != null)
            {
                //addding the main company to the result.
                var rec = new vw_CompanyUsers
                {
                    id = -999,
                    companyid = company.CompanyId,
                    CompanyName = company.CompanyName,
                    CreatedOn = usr.CreatedDateTime.HasValue == true ? usr.CreatedDateTime.Value : DateTime.Now,
                    email = usr.Email,
                    FullName = usr.FullName,
                    RoleName = usr.Roles.First().Name,
                    status = "active",
                    UserId = UserId,
                    RoleId = usr.Roles.First().Id

                };

                result.Add(rec);
            }
            else
            {
                throw new Exception("Catestrophic errror, company is null. with companyid = " + usr.CompanyId.Value);
            }

            return result;
        }


        public bool RemoveManagedUser(string id)
        {
            return companyAspNetUsersRepository.RemoveManagedUser(id);
        }


        public bool UpdateManagedUser(string id, string RoleId)
        {

            var recordtoUpate = companyAspNetUsersRepository.Find(Convert.ToInt64(id));
            if (recordtoUpate != null)
            {
                recordtoUpate.RoleId = RoleId;
                companyAspNetUsersRepository.Update(recordtoUpate);
                companyAspNetUsersRepository.SaveChanges();
                return true;
            }
            else
                return false;
            

        }


        public vw_CompanyUsers ComanyUserExists(string email)
        {
            return companyAspNetUsersRepository.CompanyUserExists(email);
        }


        public CompaniesAspNetUser AddManageUserInvitation(string email, string RoleId)
        {

            var user = userService.GetUserByEmail(email);

            var invitteuser = new CompaniesAspNetUser { CompanyId = companyAspNetUsersRepository.CompanyId, CreatedOn = DateTime.Now, InvitationCode= Guid.NewGuid().ToString(), RoleId = RoleId, Status = 1};

            if ( user != null)
            {
                invitteuser.UserId = user.Id;
                
            }
            else
            {
                invitteuser.InvitationEmail =  email;
            }

            companyAspNetUsersRepository.Add(invitteuser);
            companyAspNetUsersRepository.SaveChanges();

            string RoleName = userService.GetRoleNameByRoleId(RoleId);

            if ( user == null)
            {
              

                //send simple email with acceptance link
               emailManagerService.SendEmailInviteToUserManage(email, invitteuser.InvitationCode, true, RoleName);
            }
            else
            {

                //send email with acceptance link on registration page.
                emailManagerService.SendEmailInviteToUserManage(email, invitteuser.InvitationCode, false, RoleName);
            }
            return invitteuser;

        }


        public bool AcceptInvitation(string InvitationCode)
        {
            var verifyResult = companyAspNetUsersRepository.VerifyInvitationCode(InvitationCode);
            if (verifyResult)
            {
                companyAspNetUsersRepository.AcceptInvitationCode(InvitationCode);
                return true;
            }
            else
                return false;

        }


        public bool AcceptInvitation(string InvitationCode, string UserId)
        {
            var verifyResult = companyAspNetUsersRepository.VerifyInvitationCode(InvitationCode);
            if (verifyResult)
            {
                companyAspNetUsersRepository.AcceptInvitationCode(InvitationCode,UserId);
                return true;
            }
            else
                return false;

        }
        public IEnumerable<GetUserCounts_Result> GetUserCounts()
        {
            return this.managerUserRepository.GetUserCounts();
        }

        public IEnumerable<getUserActivitiesOverTime_Result> getUserActivitiesOverTime(DateTime DateFrom, DateTime DateTo, int Granularity)
        {
            return managerUserRepository.getUserActivitiesOverTime(DateFrom, DateTo, Granularity);
        }
        #endregion
    }
}
