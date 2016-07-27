﻿using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IManageUserService
    {
        List<vw_CompanyUsers> GetManageUsersList();

        List<vw_CompanyUsers> GetCompaniesByUserId(string UserId);


        bool RemoveManagedUser(string id);


        vw_CompanyUsers ComanyUserExists(string email);


        CompaniesAspNetUser AddUserInvitation(string email, string RoleId);

        bool AcceptInvitation(string InvitationCode);
    }
}
