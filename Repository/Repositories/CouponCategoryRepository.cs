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
    public class CouponCategoryRepository : BaseRepository<CouponCategory>, ICouponCategoryRepository
    {
           #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCategoryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponCategory> DbSet
        {
            get { return db.CouponCategory; }
        }
        #endregion

        
        #region Public


        public CouponCategory Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<CouponCategory> GetAllCoupons()
        {
            return DbSet.Select(coupon => coupon).ToList();
        }

        #endregion
    }
}
