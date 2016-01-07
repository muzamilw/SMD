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
    /// Transaction Repository 
    /// </summary>
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        #region Private
       
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public TransactionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Transaction> DbSet
        {
            get { return db.Transactions; }
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Un-debited transactions
        /// </summary>
        public IEnumerable<Transaction> GetUnprocessedTransactionsForDebit()
        {
            return DbSet.Where(trans => trans.DebitAmount != null && trans.CreditAmount == null 
                && (trans.isProcessed == null || trans.isProcessed == false)).ToList();
        }
        #endregion 
    }
}
