using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface ICouponService
    {
        IEnumerable<Coupon> GetAllCoupons();

        CampaignResponseModel GetCouponById(long CampaignId);

        CampaignResponseModel GetCoupons(AdCampaignSearchRequest request);
    }
}
