﻿using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.ResponseModels;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Services
{
    public interface ICompanyService
    {
        int GetUserCompany(string userId);
        int createCompany(string userId, string email, string fullName, string guid);

        Company GetCompanyById(int CompanyId);

        Company GetCompanyByStripeCustomerId(string StripeCustomerId);

        Company GetCurrentCompany();


        bool UpdateCompany(Company company, byte[] LogoImageBytes);

        Company GetCompanyForAddress();
        CompanyResponseModel GetCompanyDetails(int companyId = 0, string userId = "");
        bool UpdateCompanyProfile(CompanyResponseModel company, byte[] logoImageBytes);
        List<vw_ReferringCompanies> GetRefferalComponiesByCid();
        RegisteredUsersResponseModel GetRegisterdUsers(RegisteredUsersSearchRequest request);

        Boolean UpdateCompanyStatus(int status, string userId, string comments, int companyId);
    }
}
