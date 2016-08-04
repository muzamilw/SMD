using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Invoice Dtails Repository 
    /// </summary>
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
    {
        #region Private
        /// <summary>
        /// Invoice Orderby clause
        /// </summary>
        private readonly Dictionary<InvoiceByColumn, Func<Invoice, object>> invoiceOrderByClause = new Dictionary<InvoiceByColumn, Func<Invoice, object>>
                    {
                        {InvoiceByColumn.Date, d => d.InvoiceDate}      ,
                        {InvoiceByColumn.Id, d => d.InvoiceId},
                        {InvoiceByColumn.Ref, d => d.CreditCardRef},
                        {InvoiceByColumn.Amount, d => d.Total}    
                    };
        #endregion
        #region Constuctor
        /// <summary>
        /// Constructor 
        /// </summary>
        public InvoiceDetailRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<InvoiceDetail> DbSet
        {
            get { return db.InvoiceDetails; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Find By ID
        /// </summary>
        public InvoiceDetail Find(int id)
        {
            return DbSet.Find(id);
        }

       /// <summary>
        /// Get Details by Id 
        /// </summary>
        public IEnumerable<InvoiceDetail> GetInvoiceDetailByInvoiceId(long invoiceId)   
        {
            return DbSet.Where(invo => invo.InvoiceId == invoiceId).ToList();
        }
        #endregion
    }
}
