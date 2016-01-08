using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Transation Service Impl
    /// </summary>
    public class TransactionLogService : ITransactionLogService
    {
        #region Private

        private readonly ITransactionLogRepository transactionLogRepository;

        #endregion
        #region Constructot
        /// <summary>
        /// Constructor 
        /// </summary>
        public TransactionLogService(ITransactionLogRepository transactionLogRepository)
        {
            this.transactionLogRepository = transactionLogRepository;
        }
        #endregion
        #region Private
        /// <summary>
        /// Add Transaction Log On transcatoin 
        /// </summary>
        public void AddTransactionLog(TransactionLog transactionLog)
        {
            transactionLogRepository.Add(transactionLog);
            transactionLogRepository.SaveChanges();
        }
        #endregion
    }
}
