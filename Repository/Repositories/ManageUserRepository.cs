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
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

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

        public List<vw_CompanyUsers> getManageUsers()
        {

            //User user = db.Users.Where(c => c.Id == LoggedInUserIdentity).FirstOrDefault();

            
                return db.vw_CompanyUsers.Where(c => c.companyid == CompanyId).ToList();
           
        }


        public List<vw_CompanyUsers> GetCompaniesByUserId(string UserId)
        {
            var query = from c in db.vw_CompanyUsers
                      
                        where c.UserId == UserId
                        select c;

            return query.ToList();
        }


        //public List<Role> getUserRoles()
        //{

            

        //    List<Role> roles = new List<Role>();

        //    Role role1 = new Role();
        //    role1.Id = "useradmin";
        //    role1.Name = "User Administrator";


        //    Role role2 = new Role();
        //    role2.Id = "userapprovers";
        //    role2.Name = "User Approvers";

        //    Role role3 = new Role();
        //    role3.Id = "usereditors";
        //    role3.Name = "User Editor";


        //    Role role4 = new Role();
        //    role4.Id = "usermarketing";
        //    role4.Name = "User Marketing";

        //    roles.Add(role1);
        //    roles.Add(role2);
        //    roles.Add(role3);
        //    roles.Add(role4);

        //    return roles;
        //}

        public void UpdateRoles(string Id, UpdateUserProfileRequest SourceUser)
        {
          //User ss = db.Users.Where(c => c.Id == sourceUser.Id).FirstOrDefault();

            User user = db.Users.Where(c => c.Id == SourceUser.UserId).FirstOrDefault();

            if(user.Roles != null && user.Roles.Count > 0)
            {
                Role role = user.Roles.FirstOrDefault();

                user.Roles.Remove(role);

                Role newRole = db.Roles.Where(c => c.Id == Id).FirstOrDefault();

                user.Roles.Add(newRole);

                db.SaveChanges();
            }

           
              
        }

        public string getCompanyName(out string UserName, out int companyid)
        {
            User user = db.Users.Where(c => c.Id == LoggedInUserIdentity).FirstOrDefault();

            if(user != null)
            {
                UserName = user.FullName;
                companyid = user.CompanyId ?? 0;
                return db.Companies.Where(c => c.CompanyId == user.CompanyId).Select(c => c.CompanyName).FirstOrDefault();
            }
            else
            {
                UserName = string.Empty;
                companyid = 0;
                return string.Empty;
            }
            
        }
        public string getCompanyName(out string UserName,  int companyid)
        {
            User user = db.Users.Where(c => c.CompanyId == companyid).FirstOrDefault();

            if (user != null)
            {
                UserName = user.FullName;
                return db.Companies.Where(c => c.CompanyId == user.CompanyId).Select(c => c.CompanyName).FirstOrDefault();
            }
            else
            {
                UserName = string.Empty;
                return string.Empty;
            }

        }
        public string getUserName()
        {
            return db.Users.Where(c => c.Id == LoggedInUserIdentity).Select(c => c.FullName).FirstOrDefault();
        }

        public User GetByUserId(string userId)
        {
            var user = db.Users.Where(g => g.Id == userId).SingleOrDefault();
            return user;
        }

        public User GetLoginUser()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.FirstOrDefault(u => u.Id == LoggedInUserIdentity);
        }


    }
}
