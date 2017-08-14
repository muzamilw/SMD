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
    public class UserCouponViewRepository : BaseRepository<UserCouponView>, IUserCouponViewRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public UserCouponViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserCouponView> DbSet
        {
            get { return db.UserCouponView; }
        }
        #endregion
        #region Public
        public List<CampaignResponseLocation> getDealUserLocationByCId(long CouponId)
        {
            var result = DbSet.Where(g => g.CouponId == CouponId)
                            .GroupBy(ac => new
                            {
                                ac.userLocationLAT,
                                ac.userLocationLONG,

                            })
                            .Select(ac => new CampaignResponseLocation
                            {
                                lat = ac.Key.userLocationLAT,
                                lng = ac.Key.userLocationLONG,
                                count = ac.Count()
                            }).ToList();


            return result;
        }
      
       
        #endregion
    }
}
