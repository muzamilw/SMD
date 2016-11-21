using SMD.Models.DomainModels;
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
       


        List<CouponRatingReviewResponse> GetAllCouponRatingReviewByCompany(int CompanyId, int Status);


        CouponRatingReviewOverallResponse GetPublishedCouponRatingReview(long CouponId);
    }
}
