using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class ActiveUserRepository : BaseRepository<CompaniesAspNetUser>, IActiveUserRepository
    {

         #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ActiveUserRepository(IUnityContainer container)
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

        public ActiveUserResponseModel getActiveUser()
        {
            ActiveUserResponseModel activeUser = new ActiveUserResponseModel();
            DateTime dt = DateTime.Now.AddDays(-1);

           activeUser.Last1DayActiveUser = db.Users.Where(g => g.LastLoginTime > dt).Count();
            dt = DateTime.Now.AddDays(-7);
           activeUser.Last7DayActiveUser = db.Users.Where(g => g.LastLoginTime > dt).Count();
           dt = DateTime.Now.AddDays(-14);
           activeUser.Last14DayActiveUser = db.Users.Where(g => g.LastLoginTime > dt).Count();
           dt = DateTime.Now.AddDays(-30);
           activeUser.Last30DayActiveUser = db.Users.Where(g => g.LastLoginTime > dt).Count();
           dt = DateTime.Now.AddMonths(-3);
           activeUser.Last3MonthsActiveUser = db.Users.Where(g => g.LastLoginTime > dt).Count();


            return activeUser;

        }
    }
}
