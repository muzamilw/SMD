using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Transaction Log Repository 
    /// </summary>
    public class TransactionLogRepository : BaseRepository<TransactionLog>, ITransactionLogRepository
    {
        #region Private
       
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public TransactionLogRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TransactionLog> DbSet
        {
            get { return db.TransactionLogs; }
        }

        #endregion
        #region Public

        #endregion 
    }
}
