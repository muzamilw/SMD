using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
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
        public IEnumerable<GetRegisteredUserData_Result> GetRegisteredUsers(RegisteredUsersSearchRequest request, out int rowCount)
        {
            var RegisterdUsers = db.GetRegisteredUserData(request.status, request.SearchText, (request.PageNo - 1) * request.PageSize, request.PageSize).ToList();
            if (RegisterdUsers.Count() > 0)
            {
                //var firstrec = RegisterdUsers.First();
                rowCount = RegisterdUsers[0].TotalItems.Value;
            }
            else
                rowCount = 0;
            return RegisterdUsers;
        }


        public User GetUserbyPhoneNo(string phoneNo)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Users.Where(g => g.Phone1 == phoneNo).SingleOrDefault();

        }
        public String GetUserName(string id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Users.Where(g =>g.Id==id).SingleOrDefault().FullName;

        }


      

        #endregion
    }
}
