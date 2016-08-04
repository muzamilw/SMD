using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System.Data.Entity;

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

        #endregion
    }
}
