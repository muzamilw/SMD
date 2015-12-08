using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Audit Log Repository Interface 
    /// </summary>
    public interface IAuditLogRepository : IBaseRepository<AuditLog, long>
    {
    }
}
