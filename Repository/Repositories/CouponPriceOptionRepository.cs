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
    public class CouponPriceOptionRepository : BaseRepository<CouponPriceOption>, ICouponPriceOptionRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponPriceOptionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponPriceOption> DbSet
        {
            get { return db.CouponPriceOption; }
        }
        #endregion
        #region Public

        public CouponPriceOption Find(long id)
        {
            return DbSet.Where(g => g.CouponPriceOptionId == id).SingleOrDefault();
        }


        public List<CouponPriceOption> GetCouponPriceOptions(long CouponId)
        {
            return DbSet.Where(g => g.CouponId == CouponId).ToList();
        }

      
       
     
        #endregion
    }
}
