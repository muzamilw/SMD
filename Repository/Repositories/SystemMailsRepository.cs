using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// System Mails Repository 
    /// </summary>
    public class SystemMailsRepository : BaseRepository<SystemMail>, ISystemMailsRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SystemMailsRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SystemMail> DbSet
        {
            get { return db.SystemMails; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find System Mail by Id 
        /// </summary>
        public SystemMail Find(int id)
        {
            return DbSet.Find(id);
        }
        public IEnumerable<SystemMail> GetEmails(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            rowCount = DbSet.Count();
            var res = DbSet.OrderBy(c=>c.MailId);
            return res.Skip(fromRow)
                .Take(toRow);
        }

        #endregion
    }
}
