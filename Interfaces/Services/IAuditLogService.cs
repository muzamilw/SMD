using SMD.Models.Common;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Audit Log Service Interface
    /// </summary>
    public interface IAuditLogService
    {
        /// <summary>
        /// Create Audit Trails
        /// </summary>
        void CreateAuditTrail(AuditLogEntityType entityType, long entityKey, string entityDescription);
    }
}
