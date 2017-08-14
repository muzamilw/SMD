using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// AuditLog Repository 
    /// </summary>
    public class AuditLogRepository : BaseRepository<AuditLog>, IAuditLogRepository
    {
        #region Private
      
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public AuditLogRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AuditLog> DbSet
        {
            get { return db.AuditLogs; }
        }

        #endregion

        #region Public
      
        #endregion
    }
}
