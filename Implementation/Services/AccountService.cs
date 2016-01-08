using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;

namespace SMD.Implementation.Services
{
    public class AccountService : IAccountService
    {
        #region Private

        private readonly IAccountRepository accountRepository;

       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Adds four native accounts for new user 
        /// </summary>
        public void AddAccountsForNewUser(string newUserId)
        {
            #region Stripe
            // Adding Stripe account 
            accountRepository.Add(new Account
            {
                UserId = newUserId,
                AccountBalance = 0,
                AccountName = "Stripe Account",
                AccountType = (int?) AccountType.Stripe            });

            #endregion
            #region PayPal
            // Adding PayPal Account
            accountRepository.Add(new Account
            {
                UserId = newUserId,
                AccountBalance = 0,
                AccountName = "PayPal Account",
                AccountType = (int?)AccountType.Paypal
            });

            #endregion
            #region Google Wallet
            // Adding Google Wallet
            accountRepository.Add(new Account
            {
                UserId = newUserId,
                AccountBalance = 0,
                AccountName = "Google Wallet",
                AccountType = (int?)AccountType.GoogleWallet
            });
            #endregion
            #region Virtual Account
            // Adding Virtual Account
            accountRepository.Add(new Account
            {
                UserId = newUserId,
                AccountBalance = 0,
                AccountName = "Virtual Account",
                AccountType = (int?)AccountType.VirtualAccount
            });
            #endregion
            accountRepository.SaveChanges();
        }
        #endregion
    }
}
