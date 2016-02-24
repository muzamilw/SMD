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
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Company> DbSet
        {
            get { return db.Companies; }
        }
        #endregion

        
        #region Public


        public Company Find(int id)
        {
            return DbSet.Where(g => g.CompanyId == id).SingleOrDefault();
        }

        public int GetUserCompany(string userId)
        {
            var user = db.Users.Where(g => g.Id == userId).SingleOrDefault();
            if (user == null || user.CompanyId.HasValue == false)
                return 0;
            return user.CompanyId.Value;
        }
        #endregion
    }
}
