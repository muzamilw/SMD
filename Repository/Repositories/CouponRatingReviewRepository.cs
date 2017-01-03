using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
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


        public CouponRatingReviewOverallResponse GetPublishedCouponRatingReview(long CouponId, out int Count)
        {

            var result = new CouponRatingReviewOverallResponse { CouponId = CouponId, OverAllStarRating = db.CouponRatingReview.Where(r => r.CouponId == CouponId).Average(r => r.StarRating) };


            result.OverAllStarRating += 5;

            if (result.OverAllStarRating.HasValue)
                result.OverAllStarRating = Convert.ToDouble( Math.Round(result.OverAllStarRating.Value, 1, MidpointRounding.AwayFromZero));

            if (result.OverAllStarRating.HasValue == false)
                result.OverAllStarRating = 0;

            var allrows = (from r in db.CouponRatingReview
                         join c in db.Coupons on r.CouponId equals c.CouponId
                         join u in db.Users on r.UserId equals u.Id
                         where r.Status == 2 && r.CouponId == CouponId
                         select new CouponRatingReviewResponse {  CouponId = c.CouponId, CouponReviewId = r.CouponReviewId, FullName = u.FullName, CouponTitle = c.CouponTitle, RatingDateTime = r.RatingDateTime, Review = r.Review, CompanyId = c.CompanyId, ReviewImage1 = r.ReviewImage1, ReviewImage2 = r.ReviewImage2, Reviewimage3 = r.Reviewimage3, StarRating = r.StarRating, Status = r.Status, UserId = r.UserId, ProfileImage = u.ProfileImage  });

            Count = allrows.Count();

            result.CouponRatingReviewResponses = allrows.OrderBy(r => Guid.NewGuid()).Take(6).ToList();

            return result;
           
        }

        public List<CouponRatingReviewResponse> GetAllCouponRatingReviewByCompany(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            var result = from r in db.CouponRatingReview
                         join c in db.Coupons on r.CouponId equals c.CouponId
                         join u in db.Users on r.UserId equals u.Id
                         where r.Status == request.ReviewStatus && c.CompanyId == CompanyId 
                         orderby (r.RatingDateTime)
                         select new CouponRatingReviewResponse { CouponId = c.CouponId, CouponReviewId = r.CouponReviewId, FullName = u.FullName, CouponTitle = c.CouponTitle, RatingDateTime = r.RatingDateTime, Review = r.Review, CompanyId = c.CompanyId, ReviewImage1 = r.ReviewImage1, ReviewImage2 = r.ReviewImage2, Reviewimage3 = r.Reviewimage3, StarRating = r.StarRating, Status = r.Status, UserId = r.UserId, ProfileImage = u.ProfileImage };
            rowCount =result.Count();
           
            return result.Skip(fromRow)
                    .Take(toRow).ToList();
            
        }
        public int CouponReviewCount ()
        {
            var result = from r in db.CouponRatingReview
                         join c in db.Coupons on r.CouponId equals c.CouponId
                         where c.CompanyId == CompanyId && r.Status==1 && !string.IsNullOrEmpty(r.Review)
                         select r;

            return result.Count();         // db.CouponRatingReview.ToList().Count(a => a.CompanyId == CompanyId && a.Status==1 && !string.IsNullOrEmpty(a.Review));
        }

      
       
     
        #endregion
    }
}
