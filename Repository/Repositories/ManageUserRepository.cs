using SMD.Interfaces.Repository;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.IdentityModels;
using Microsoft.Practices.Unity;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    public class ManageUserRepository : BaseRepository<User>, IManageUserRepository
    {

        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManageUserRepository(IUnityContainer container)
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

        /// <summary>
        /// Find Queston by Id 
        /// </summary>
        public User Find(int id)
        {
            return DbSet.Find(id);
        }
        #endregion

        public List<User> getManageUsers()
        {

            User user = db.Users.Where(c => c.Id == LoggedInUserIdentity).FirstOrDefault();

            if (user != null)
            {
                return db.Users.Where(c => c.CompanyId == user.CompanyId).ToList();
            }
            else
            {
                return null;
            }
           
        }

    }
}
