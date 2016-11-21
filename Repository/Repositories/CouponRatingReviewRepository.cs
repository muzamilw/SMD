using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CouponRatingReviewRepository : BaseRepository<CouponRatingReview>, ICouponRatingReviewRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponRatingReviewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponRatingReview> DbSet
        {
            get { return db.CouponRatingReview; }
        }
        #endregion
        #region Public

        public CouponRatingReview Find(long id)
        {
            return DbSet.Where(g => g.CouponReviewId == id).SingleOrDefault();
        }


        public CouponRatingReviewOverallResponse GetPublishedCouponRatingReview(long CouponId)
        {

            var result = new CouponRatingReviewOverallResponse { CouponId = CouponId, OverAllStarRating = db.CouponRatingReview.Where(r => r.CouponId == CouponId).Average(r => r.StarRating) };

            if (result.OverAllStarRating.HasValue == false)
                result.OverAllStarRating = 0;

            result.CouponRatingReviewResponses = (from r in db.CouponRatingReview
                         join c in db.Coupons on r.CouponId equals c.CouponId
                         join u in db.Users on r.UserId equals u.Id
                         where r.Status == 2 && r.CouponId == CouponId
                         select new CouponRatingReviewResponse {  CouponId = c.CouponId, CouponReviewId = r.CouponReviewId, FullName = u.FullName, CouponTitle = c.CouponTitle, RatingDateTime = r.RatingDateTime, Review = r.Review, CompanyId = c.CompanyId, ReviewImage1 = r.ReviewImage1, ReviewImage2 = r.ReviewImage2, Reviewimage3 = r.Reviewimage3, StarRating = r.StarRating, Status = r.Status, UserId = r.UserId, ProfileImage = u.ProfileImage  }).ToList();


            return result;
           
        }

        public List<CouponRatingReviewResponse> GetAllCouponRatingReviewByCompany(int CompanyId, int Status)
        {
            var result = from r in db.CouponRatingReview
                         join c in db.Coupons on r.CouponId equals c.CouponId
                         join u in db.Users on r.UserId equals u.Id
                         where r.Status == Status && c.CompanyId == CompanyId
                         select new CouponRatingReviewResponse { CouponId = c.CouponId, CouponReviewId = r.CouponReviewId, FullName = u.FullName, CouponTitle = c.CouponTitle, RatingDateTime = r.RatingDateTime, Review = r.Review, CompanyId = c.CompanyId, ReviewImage1 = r.ReviewImage1, ReviewImage2 = r.ReviewImage2, Reviewimage3 = r.Reviewimage3, StarRating = r.StarRating, Status = r.Status, UserId = r.UserId, ProfileImage = u.ProfileImage };


            return result.ToList();
        }

      
       
     
        #endregion
    }
}
