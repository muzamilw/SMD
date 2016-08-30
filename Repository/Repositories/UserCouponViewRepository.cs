using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Tax Repository 
    /// </summary>
    public class UserCouponViewRepository : BaseRepository<UserCouponView>, IUserCouponViewRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public UserCouponViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserCouponView> DbSet
        {
            get { return db.UserCouponView; }
        }
        #endregion
        #region Public

       
        #endregion
    }
}
