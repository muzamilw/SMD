using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICouponCodeRepository : IBaseRepository<CouponCode, long>
    {
        void RemoveAll(List<CouponCode> categories);
        bool IsCodeExist(string Code);
        List<string> GetUserCoupons(string UserId);
        string UpdateCouponSettings(string VoucherCode, string SecretKey, string UserId);
    }
}
