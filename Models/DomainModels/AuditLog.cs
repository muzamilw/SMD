using System;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Audit Log Domain Model
    /// </summary>
    public class AuditLog
    {
        public long Id { get; set; }
        public int? AuditType { get; set; }
        public string UserId { get; set; }
        public DateTime? AuditDateTime { get; set; }
        public long? AuditEntity { get; set; }
        public string AuditEntityDescription { get; set; }
        public string TableName { get; set; }
    }
}
