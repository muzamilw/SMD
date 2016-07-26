﻿using SMD.Interfaces.Repository;
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


        public bool RemoveManagedUser(string id)
        {
            return companyAspNetUsersRepository.RemoveManagedUser(id);
        }



        public vw_CompanyUsers ComanyUserExists(string email)
        {
            return companyAspNetUsersRepository.CompanyUserExists(email);
        }


        public CompaniesAspNetUser AddUserInvitation(string email, string RoleId)
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



            if ( user != null)
            {
                //send simple email with acceptance link
                emailManagerService.SendEmailToInviteUser(email, invitteuser.InvitationCode,true,invitteuser.AspNetRole.Name);
            }
            else
            {

                //send email with acceptance link on registration page.
                emailManagerService.SendEmailToInviteUser(email, invitteuser.InvitationCode, false, invitteuser.AspNetRole.Name);
            }
            return invitteuser;

        }


        //public bool AcceptInvitation(string invitationcode)
        //{
        //    rer
        //}
        #endregion
    }
}
