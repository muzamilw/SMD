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
    public class UserPurchasedCouponRepository : BaseRepository<UserPurchasedCoupon>, IUserPurchasedCouponRepository
    {
        #region Private
       
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public UserPurchasedCouponRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserPurchasedCoupon> DbSet
        {
            get { return db.UserPurchasedCoupon; }
        }
        public UserPurchasedCoupon GetPurchasedCouponById(long CouponPurchaseId)
        {
            return DbSet.FirstOrDefault(i => i.CouponPurchaseId == CouponPurchaseId);
        }
         public IEnumerable<Coupon> GetPurchasedCouponByUserId(string UserId)
        {

            DateTime endDate = DateTime.Today.AddHours(24);

            var result = from c in db.Coupons
                         join uc in db.UserPurchasedCoupon on c.CouponId equals uc.CouponId
                         where uc.UserId == UserId && c.CouponExpirydate > endDate && (uc.IsRedeemed == false || uc.IsRedeemed == null)
                         orderby uc.PurchaseDateTime
                        select c;
                         //select new Coupon { CouponId = c.CouponId, CouponTitle = c.CouponTitle, couponImage1 = c.couponImage1, Price = c.Price, Savings = c.Savings, SwapCost = c.SwapCost, DaysLeft = (DateTime.Today - new DateTime(c.CouponActiveYear.Value, c.CouponActiveMonth.Value, 30)).Days, CompanyId= c.CompanyId, LogoUrl = c.LogoUrl };


            return result.ToList();
            //return DbSet.Where(i => i.UserId == UserId).ToList();
        }



        #endregion

    }
}
