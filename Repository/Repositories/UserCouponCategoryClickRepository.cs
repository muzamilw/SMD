using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Tax Repository 
    /// </summary>
    public class UserCouponCategoryClickRepository : BaseRepository<UserCouponCategoryClick>, IUserCouponCategoryClickRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public UserCouponCategoryClickRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserCouponCategoryClick> DbSet
        {
            get { return db.UserCouponCategoryClick; }
        }
        #endregion
        #region Public
        
      
       
        #endregion
    }
}
