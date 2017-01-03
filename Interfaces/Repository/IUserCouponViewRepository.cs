using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface IUserCouponViewRepository : IBaseRepository<UserCouponView, long>
    {
        List<CampaignResponseLocation> getDealUserLocationByCId(long CouponId);
    }
}
