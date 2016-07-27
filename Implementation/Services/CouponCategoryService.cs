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

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCategoryService(ICouponCategoryRepository couponRepository)
        {
            this.couponRepository = couponRepository;
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
        #endregion
    }
}
