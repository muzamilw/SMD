using System;
using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Audit Log Service
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository auditLogRepository;

        #region Private

        private readonly Dictionary<AuditLogEntityType, string> tableNames = new Dictionary<AuditLogEntityType, string>
            {
                { AuditLogEntityType.ProfileQuestion, "ProfileQuestion"},
                { AuditLogEntityType.SurvryQuestion, "SurveyQuestion"},
                { AuditLogEntityType.AdCampaign, "AdCampaign"},
                { AuditLogEntityType.All, ""}
            };

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            if (auditLogRepository == null)
            {
                throw new ArgumentNullException("auditLogRepository");
            }

            this.auditLogRepository = auditLogRepository;
        }

        #endregion

        #region Public

        /// <summary>
        /// Create Audit Trail
        /// </summary>
        public void CreateAuditTrail(AuditLogEntityType entityType, long entityKey, string entityDescription)
        {
            AuditLog autoAudit = auditLogRepository.Create();
            autoAudit.AuditDateTime = DateTime.Now;
            autoAudit.AuditEntityDescription = entityDescription;
            autoAudit.TableName = tableNames[entityType];
            autoAudit.AuditEntity = entityKey;
            autoAudit.UserId = auditLogRepository.LoggedInUserIdentity;
            auditLogRepository.Add(autoAudit);
            auditLogRepository.SaveChanges();
        }

        #endregion

    }
}
