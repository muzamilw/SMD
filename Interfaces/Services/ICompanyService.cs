using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface ICompanyService
    {
        int GetUserCompany(string userId);
        bool createCompany(string userId, string email, string fullName, string guid);

        Company GetCompanyById(int CompanyId);
    }
}
