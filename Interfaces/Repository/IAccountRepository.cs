using SMD.Models.DomainModels;

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
        Account GetByUserId(string userId);

        /// <summary>
        /// Get Account By name
        /// </summary>
        Account GetByName(string name);
    }
}
