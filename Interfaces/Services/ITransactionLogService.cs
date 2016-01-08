

using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Transaction Log Service INterface
    /// </summary>
    public interface ITransactionLogService
    {
        /// <summary>
        /// Add Transaction Log On transcatoin 
        /// </summary>
        void AddTransactionLog(TransactionLog transactionLog);
    }
}
