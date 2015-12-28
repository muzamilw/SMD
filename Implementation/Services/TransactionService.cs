using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Transaction Service Implementation 
    /// </summary>
    public class TransactionService : ITransactionService
    {
        #region Private

        private readonly ITransactionRepository transactionRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        #endregion
        #region Public
        public void SurveyApprovalTransaction(long sQid)
        {

        }
        #endregion
    }
}
