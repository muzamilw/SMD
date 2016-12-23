using SMD.Models.DomainModels;
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
        Dictionary<string, int> GetStatusesCounters();
        int GetUserCompany(string userId);
        int createCompany(string userId, string email, string fullName, string guid);

        Company GetCompanyById(int CompanyId);

        Company GetCompanyByStripeCustomerId(string StripeCustomerId);

        Company GetCurrentCompany();


        bool UpdateCompany(Company company, byte[] LogoImageBytes);

        bool UpdateCompany(Company company);

        Company GetCompanyForAddress();
        CompanyResponseModel GetCompanyDetails(int companyId = 0, string userId = "");
        bool UpdateCompanyProfile(CompanyResponseModel company, byte[] logoImageBytes);
        List<vw_ReferringCompanies> GetRefferalComponiesByCid();
        RegisteredUsersResponseModel GetRegisterdUsers(RegisteredUsersSearchRequest request);

        Boolean UpdateCompanyStatus(int status, string userId, string comments, int companyId);
        List<Dashboard_analytics_Result> GetDashboardAnalytics(string UserID);
        Company GetCompanyInfo();
        CompanySubscription GetCompanySubscription();
        String UpdateCompanySubscription(CompanySubscription comSub);
        EmailResponseModel GetEmails(GetPagedListRequest request);
        bool UpdateSystemEmail(SystemMail email);
    }
}
