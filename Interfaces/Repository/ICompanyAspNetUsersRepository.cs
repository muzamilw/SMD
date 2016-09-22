using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICompanyAspNetUsersRepository : IBaseRepository<CompaniesAspNetUser, long>
    {
        IEnumerable<CompaniesAspNetUser> GetUsersByCompanyId(int CompanyId);

        bool RemoveManagedUser(string id);


        vw_CompanyUsers CompanyUserExists(string Email);

        bool VerifyInvitationCode(string InvitationCode);

        bool AcceptInvitationCode(string InvitationCode);
        bool AcceptInvitationCode(string InvitationCode, string UserId);
        IEnumerable<GetActiveVSNewUsers_Result> GetActiveVSNewUsers();
    }
}
