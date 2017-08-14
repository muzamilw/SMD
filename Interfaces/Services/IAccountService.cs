
using SMD.Models.IdentityModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Account Interface Service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Adds four native accounts for new user 
        /// </summary>
        void AddAccountsForNewUser(int newCompanyId);
    }
}
