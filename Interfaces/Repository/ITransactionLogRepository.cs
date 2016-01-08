using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Transaction Log Repository Interface 
    /// </summary>
    public interface ITransactionLogRepository : IBaseRepository<TransactionLog, long>
    {
        
    }
}
