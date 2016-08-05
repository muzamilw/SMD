using SMD.Models.Common;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IUserPurchasedCouponRepository : IBaseRepository<UserPurchasedCoupon, long>
    {


         UserPurchasedCoupon GetPurchasedCouponById(long CouponPurchaseId);

         IEnumerable<PurchasedCoupons> GetPurchasedCouponByUserId(string UserId);

     
    }
}
