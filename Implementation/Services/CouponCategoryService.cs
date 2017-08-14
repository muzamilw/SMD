using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{

    public class CouponCategoryService : ICouponCategoryService
    {
        #region Private

        private readonly ICouponCategoryRepository couponRepository;

        private readonly IUserCouponCategoryClickRepository userCouponCategoryClickRepository;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCategoryService(ICouponCategoryRepository couponRepository, IUserCouponCategoryClickRepository userCouponCategoryClickRepository)
        {
            this.couponRepository = couponRepository;
            this.userCouponCategoryClickRepository = userCouponCategoryClickRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// List of Country's Cities
        /// </summary>
        public IEnumerable<CouponCategory> GetAllCategories()
        {
            return couponRepository.GetAllCoupons().Where(g=>g.Status == true).OrderBy( g=> g.SortOrder).ToList();
        }


        public bool InsertUserCouponCategoryClick(int couponCategoryId, string userId)
        {
            var click = new UserCouponCategoryClick { ClickDateTime = DateTime.Now, CouponCategoryId =couponCategoryId, UserId = userId};
            userCouponCategoryClickRepository.Add(click);
            userCouponCategoryClickRepository.SaveChanges();
            return true;

        }
        #endregion
    }
}
