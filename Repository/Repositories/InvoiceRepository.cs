using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Invoice Repository 
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
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
        public InvoiceRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Invoice> DbSet
        {
            get { return db.Invoices; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Find By ID
        /// </summary>
        public Invoice Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        public IEnumerable<Invoice> SearchInvoices(InvoiceSearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Invoice, bool>> query =
                invo => ((request.FromDate == DateTime.MinValue && request.ToDate == DateTime.MinValue) ||
                         (request.FromDate <= invo.InvoiceDate && request.ToDate >= invo.InvoiceDate));

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(invoiceOrderByClause[request.InvoiceOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(invoiceOrderByClause[request.InvoiceOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList(); 
        }
        #endregion
    }
}
