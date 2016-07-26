using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CompanyAspNetUsersRepository : BaseRepository<CompaniesAspNetUser>, ICompanyAspNetUsersRepository
    {
           #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyAspNetUsersRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompaniesAspNetUser> DbSet
        {
            get { return db.CompaniesAspNetUsers; }
        }
        #endregion

        
        #region Public


        public CompaniesAspNetUser Find(int id)
        {
            return db.CompaniesAspNetUsers.Where(g => g.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<CompaniesAspNetUser> GetUsersByCompanyId(int CompanyId)
        {
            return DbSet.Where( g=> g.CompanyId == CompanyId).ToList();
        }



        public bool RemoveManagedUser(string id)
        {

            var mUser = this.Find(Convert.ToInt32(id));
            if (mUser != null)
            {

                this.Delete(mUser);
                this.SaveChanges();
                return true;
            }
            else
                return false;
        }



        public vw_CompanyUsers CompanyUserExists(string Email)
        {

            return db.vw_CompanyUsers.Where(g => g.email.Contains(Email)).FirstOrDefault();
        }

        #endregion
    }
}
