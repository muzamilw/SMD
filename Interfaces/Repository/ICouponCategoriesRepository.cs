using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICouponCategoriesRepository : IBaseRepository<CouponCategories, long>
    {
        IEnumerable<CouponCategories> GetCategoriesByCouponId(long CouponId);
    }
}
