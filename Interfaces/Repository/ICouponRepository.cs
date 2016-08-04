using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICouponRepository : IBaseRepository<Coupon, long>
    {
        IEnumerable<Coupon> GetAllCoupons();
        Coupon Find(long id);
        IEnumerable<Coupon> SearchCampaign(AdCampaignSearchRequest request, out int rowCount);
        IEnumerable<Coupon> GetCouponById(long campaignId);



        IEnumerable<SearchCoupons_Result> SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId);

        List<Coupon> GetCouponsByCompanyId(int CompanyId);
    }
}
