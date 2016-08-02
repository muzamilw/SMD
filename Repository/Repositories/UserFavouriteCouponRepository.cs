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
    public class UserFavouriteCouponRepository : BaseRepository<UserFavouriteCoupon>, IUserFavouriteCouponRepository
    {
        #region Private
       
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public UserFavouriteCouponRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserFavouriteCoupon> DbSet
        {
            get { return db.UserFavouriteCoupons; }
        }
        public UserFavouriteCoupon GetByCouponId(long CouponId)
        {
            return DbSet.FirstOrDefault(i => i.CouponId == CouponId);
        }
        public IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId)
        {

            var result = from c in db.Coupons
                          join uc in db.UserFavouriteCoupons on c.CouponId equals uc.CouponId
                          where uc.UserId == UserId
                            
                          select c;


            return result.ToList();
            //return DbSet.Where(i => i.UserId == UserId).ToList();
        }
        #endregion

    }
}
