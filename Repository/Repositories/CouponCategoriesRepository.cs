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
    public class CouponCategoriesRepository : BaseRepository<CouponCategories>, ICouponCategoriesRepository
    {
           #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCategoriesRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponCategories> DbSet
        {
            get { return db.CouponCategories; }
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
       public IEnumerable<CouponCategories> GetCategoriesByCouponId(long CouponId)
        {
            return DbSet.Where(g=> g.CouponId == CouponId).ToList();
        }

        #endregion
    }
}
