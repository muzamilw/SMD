﻿using SMD.Interfaces.Repository;
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


        public List<Role> getUserRoles()
        {
            List<Role> roles = new List<Role>();

            Role role1 = new Role();
            role1.Id = "useradmin";
            role1.Name = "User Administrator";


            Role role2 = new Role();
            role2.Id = "userapprovers";
            role2.Name = "User Approvers";

            Role role3 = new Role();
            role3.Id = "usereditors";
            role3.Name = "User Editor";


            Role role4 = new Role();
            role4.Id = "usermarketing";
            role4.Name = "User Marketing";

            roles.Add(role1);
            roles.Add(role2);
            roles.Add(role3);
            roles.Add(role4);

            return roles;
        }

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
    }
}
