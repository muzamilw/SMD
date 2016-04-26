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
    public class CouponCodeRepository : BaseRepository<CouponCode>, ICouponCodeRepository
    {
         #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCodeRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponCode> DbSet
        {
            get { return db.CouponCodes; }
        }

        public void RemoveAll(List<CouponCode> categories)
        {

            db.CouponCodes.RemoveRange(categories);
            db.SaveChanges();

        }
        #endregion

    }
}
