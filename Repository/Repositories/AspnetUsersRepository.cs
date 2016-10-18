using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{

    public class AspnetUsersRepository : BaseRepository<User>, IAspnetUsersRepository
    {
        #region Private

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public AspnetUsersRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<User> DbSet
        {
            get { return db.Users; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Get List of Language 
        /// </summary>
        public int GetUserProfileCompletness(string UserId)
        {
            return db.GetUserProfileCompletness(UserId).FirstOrDefault();
        }
        public String GetUserEmail(int companyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Users.Where(g => g.CompanyId == companyId).SingleOrDefault().Email;
            
        }
        public String GetUserid(int companyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Users.Where(g => g.CompanyId == companyId).SingleOrDefault().Id;

        }


      

        #endregion
    }
}
