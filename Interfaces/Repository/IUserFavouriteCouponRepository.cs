using SMD.Models.Common;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IUserFavouriteCouponRepository : IBaseRepository<UserFavouriteCoupon, long>
    {
        UserFavouriteCoupon GetByCouponId(long CouponId);
        IEnumerable<Coupon> GetAllFavouriteCouponByUserId(string UserId);

        bool CheckCouponFlaggedByUser(long CouponId, string UserId);
    }
}
