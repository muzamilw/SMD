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

        public UserFavouriteCoupon GetByCouponId(long CouponId, string UserId)
        {
            return DbSet.FirstOrDefault(i => i.CouponId == CouponId && i.UserId == UserId);
        }
        public IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId)
        {

            var result = from c in db.Coupons
                         join uc in db.UserFavouriteCoupons on c.CouponId equals uc.CouponId
                         where uc.UserId == UserId
                        select c;
                         //select new Coupon { CouponId = c.CouponId, CouponTitle = c.CouponTitle, couponImage1 = c.couponImage1, Price = c.Price, Savings = c.Savings, SwapCost = c.SwapCost, DaysLeft = (DateTime.Today - new DateTime(c.CouponActiveYear.Value, c.CouponActiveMonth.Value, 30)).Days, CompanyId= c.CompanyId, LogoUrl = c.LogoUrl };


            return result.ToList();
            //return DbSet.Where(i => i.UserId == UserId).ToList();
        }



        public bool CheckCouponFlaggedByUser(long CouponId, string UserId)
        {
            var res=  DbSet.SingleOrDefault(i => i.CouponId == CouponId && i.UserId == UserId);
            if (res != null)
                return true;
            else
                return false;
        }
        #endregion

    }
}
