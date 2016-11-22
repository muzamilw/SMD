using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICouponRatingReviewRepository : IBaseRepository<CouponRatingReview, long>
    {



        List<CouponRatingReviewResponse> GetAllCouponRatingReviewByCompany(GetPagedListRequest request, out int rowCount);


        CouponRatingReviewOverallResponse GetPublishedCouponRatingReview(long CouponId);
        int CouponReviewCount();
    }
}
