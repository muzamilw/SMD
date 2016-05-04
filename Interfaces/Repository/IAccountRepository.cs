using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Account Repository Interface 
    /// </summary>
    public interface IAccountRepository : IBaseRepository<Account, long>
    {
        /// <summary>
        /// Get Account By user id
        /// </summary>
        Account GetByCompanyId(int commpanyId, AccountType accountType);

        Account GetByUserId(string userId, AccountType accountType);
        /// <summary>
        /// Get Account By name
        /// </summary>
        Account GetByName(string name);

        /// <summary>
        /// Get Account By ID
        /// </summary>
        Account GetById(long accountId);
    }
}
