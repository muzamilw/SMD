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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<CompaniesAspNetUser> GetUsersByCompanyId(int CompanyId)
        {
            return DbSet.Where( g=> g.CompanyId == CompanyId).ToList();
        }

        #endregion
    }
}
