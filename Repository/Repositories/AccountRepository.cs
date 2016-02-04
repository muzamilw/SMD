using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Account Repository 
    /// </summary>
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        #region Private
       
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public AccountRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Account> DbSet
        {
            get { return db.Accounts; }
        }

        #endregion

        
        #region Public

        /// <summary>
        /// Get Account by user id
        /// </summary>
        public Account GetByUserId(string userId, AccountType accountType)
        {
            return DbSet.FirstOrDefault(account => account.UserId == userId && account.AccountType == (int)accountType);
        }

        /// <summary>
        /// Get Account by name
        /// </summary>
        public Account GetByName(string name)
        {
            return DbSet.FirstOrDefault(account => account.AccountName == name);
        }


        /// <summary>
        /// Get Account By ID
        /// </summary>
        public Account GetById(long accountId)
        {
            return DbSet.FirstOrDefault(account => account.AccountId == accountId);
        }
        #endregion

        
    }
}
