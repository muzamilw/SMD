using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// System Mails Repository Interface 
    /// </summary>
    public interface ISystemMailsRepository : IBaseRepository<SystemMail, int>
    {
        IEnumerable<SystemMail> GetEmails(GetPagedListRequest request, out int rowCount);
    }
}
