using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SMD.Models.IdentityModels;
namespace SMD.Repository.Repositories
{
    public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
    {
           #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Coupon> DbSet
        {
            get { return db.Coupons; }
        }
        #endregion

        
        #region Publi


        public Coupon Find(long id)
        {
            return null;
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<Coupon> GetAllCoupons()
        {
            return DbSet.Select(coupon => coupon).ToList();
        }
        public IEnumerable<Coupon> SearchCampaign(AdCampaignSearchRequest request, out int rowCount)
        {


            bool isAdmin = false;
            var users = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
          
            if (request == null)
            {




                const int fromRow = 0;
                const int toRow = 10;
                rowCount = DbSet.Select(c=>c).Count();
                if (isAdmin)
                {
                    IEnumerable<Coupon> adCampaigns = DbSet
                           .Skip(fromRow)
                           .Take(toRow)
                           .ToList();

                    
                    return adCampaigns;


                }
                else
                {
                    IEnumerable<Coupon> adCampaigns = DbSet.OrderByDescending(g => g.CouponId)
                           .Skip(fromRow)
                           .Take(toRow)
                           .ToList();
                    
                    return adCampaigns;

                }
            }
            else   //searching mode
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<Coupon, bool>> query;
                if(request.status==0)
                {
                    query =
                        campaign =>
                            (string.IsNullOrEmpty(request.SearchText) ||
                             (campaign.CouponTitle.Contains(request.SearchText)))
                             && (campaign.CompanyId == CompanyId || isAdmin) && campaign.Status != 7;
                }
                else{
                    query =
                        campaign =>
                            (string.IsNullOrEmpty(request.SearchText) ||
                             (campaign.CouponTitle.Contains(request.SearchText))) && (campaign.Status == 0 || campaign.Status == request.status)
                             && (campaign.CompanyId == CompanyId || isAdmin) && campaign.Status != 7;
                }
                 
                IEnumerable<Coupon> adCampaigns = null;
                if (request.ShowCoupons != null && request.ShowCoupons == true)
                {
                   
                    adCampaigns = DbSet.Select(c=>c).Where(query).OrderByDescending(g => g.CouponId)
                      .Skip(fromRow)
                      .Take(toRow)
                      .ToList();
                }
                else
                {
                    adCampaigns = DbSet.Where(query).OrderByDescending(g => g.CouponId)
                         .Skip(fromRow)
                         .Take(toRow)
                         .ToList();
                }

                rowCount = DbSet.Count(query);

                adCampaigns.ToList().ForEach(a => a.CouponViewCount = GetViewdCount(a.CouponId));
                              

                //if (adCampaigns != null && adCampaigns.Count() > 0)
                //{
                //    foreach (var ad in adCampaigns)
                //    {
                     
                //    }

                //}
                return adCampaigns;
            }
        }
        private int GetViewdCount(long couponId)
        {
            return db.UserCouponView.ToList().Count(a => a.CouponId == couponId);
        }
        public IEnumerable<Coupon> GetCouponById(long campaignId)
        {


         

            Expression<Func<Coupon, bool>> query =
                ad => ad.CouponId == campaignId;

            var coupon =  DbSet.Where(query);

            return coupon;



        }



        //SP cal for mobile apps
        public GetCouponByID_Result GetCouponByIdSP(long CouponId, string UserId, string Lat, string Lon)
        {
            return db.GetCouponByID(CouponId,UserId,Lat,Lon).First();

        }




        public Coupon GetCouponByIdSingle(long couponId)
        {


            return DbSet.Where(g => g.CouponId == couponId).SingleOrDefault();
        }


        public IEnumerable<SearchCoupons_Result> SearchCoupons(int categoryId, int type, int size, string keywords, int pageNo, int distance, string Lat, string Lon, string UserId)
        {
            int? fromRow = (pageNo - 1) * size;
            int? toRow = size;
            return db.SearchCoupons(categoryId, type, keywords, distance, Lat, Lon, UserId, fromRow, toRow);
        }


        public List<Coupon> GetCouponsByCompanyId(int CompanyId)
        {

            return db.Coupons.Where(g => g.CompanyId == CompanyId).ToList(); //.GetCouponsByCompanyId(CompanyId).ToList();
        }
        public IEnumerable<vw_Coupons> GetCouponsForApproval(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Coupon, bool>> query =
                c => c.Status == (Int32)AdCampaignStatus.SubmitForApproval;

            //if (request.ShowCoupons.HasValue && request.ShowCoupons.Value == true)
            //    query = c => c.Status == (Int32)AdCampaignStatus.SubmitForApproval;

            rowCount = DbSet.Count(query);

            var res = db.vw_Coupons.Where( g=> g.Status == 2)
                    .OrderByDescending(p=>p.SubmissionDateTime);


            return res.Skip(fromRow)
                    .Take(toRow);

        }
        public IEnumerable<getDealByCouponID_Result> getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity)
        {
            return db.getDealByCouponIDAnalytics(CouponID, dateRange, Granularity);
        }
        public IEnumerable<getDealByCouponIdRatioAnalytic_Result> getDealByCouponIdRatioAnalytic(int ID, int dateRange)
        {
            
            return db.getDealByCouponIdRatioAnalytic(ID, dateRange);
        }

        public int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type)
        {
              return db.getDealStatByCouponIdFormAnalytic(dealId, Gender, age, type).ToList().FirstOrDefault();
        }
        public DateTime getExpiryDate(int CouponId)
        {
            Coupon c = db.Coupons.Where(g=>g.CouponId == CouponId).FirstOrDefault();
           if(c != null){
               return (DateTime)c.CouponExpirydate;
           }
           return default(DateTime);
        }



        public bool PauseAllCoupons(int CompanyId)
        {
            db.Database.ExecuteSqlCommand("update coupon set status=4 where companyId=" + CompanyId);
            return true;

        }


        public bool CompleteAllCoupons(int CompanyId)
        {
            db.Database.ExecuteSqlCommand("update coupon set status=5 where companyId=" + CompanyId);
            return true;

        }
        public int GetCouponByBranchId(long id)
        {

            return db.Coupons.Where(c => c.LocationBranchId == id).ToList().Count;
        }
        public int GetFreeCouponCount()
        {
            return db.Coupons.Where(c => c.CompanyId==CompanyId && c.CouponListingMode==1&&(c.Status==2||c.Status==3)).ToList().Count;
        }
        public IEnumerable<vw_Coupons> GetMarketingDeals(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Coupon, bool>> query =
                c => c.Approved==true&& c.IsMarketingStories ==true;

            //if (request.ShowCoupons.HasValue && request.ShowCoupons.Value == true)
            //    query = c => c.Status == (Int32)AdCampaignStatus.SubmitForApproval;

            rowCount = DbSet.Count(query);

            var res = db.vw_Coupons.Where(g => g.Approved == true && g.IsMarketingStories == true)
                    .OrderByDescending(p => p.ApprovalDateTime);


            return res.Skip(fromRow)
                    .Take(toRow);

        }
        public List<GetRandom3Deal_Result> GetRandomDeals()
        {
            return db.GetRandom3Deal().ToList();
        }
        public List<CouponCategories> GetDealsByCtgId(int CategoryID)
        {
            return db.CouponCategories.Where(i => i.CategoryId == CategoryID).ToList();
        
        }

        public List<GetUsersCouponsForEmailNotification_Result> GetUsersCouponsForEmailNotification(int mode)
        {
            return db.GetUsersCouponsForEmailNotification(mode).ToList();
        }


        public List<GetUsersCouponsForEmailNotification_Result> GetDealsWhichHavejustExpired()
        {
            //mode 8 returns the expired deals
            return db.GetUsersCouponsForEmailNotification(8).ToList();
        }

        public List<GetUsersCouponsForEmailNotification_Result> GetDealsWhichWillExpirein3Days()
        {
            //mode 9 returns the deals which will expire in 3 days
            return db.GetUsersCouponsForEmailNotification(9).ToList();
        }

        public bool CompleteCoupons(long[] couponIds)
        {


            foreach (var item in couponIds)
            {
                db.Database.ExecuteSqlCommand("update dbo.coupon set status = 5 where couponid=" + item.ToString());
            }

            return true;
        }

        public CouponStatsResponse getDealStats(long Id)
        {
            DateTime d7date =System.DateTime.Now.AddDays(-7);
             DateTime d14date =System.DateTime.Now.AddDays(-14);
            CouponStatsResponse res = new CouponStatsResponse();
            res.ClickThruComparison = 0;
            res.CouponId = Id;
            res.DealLines = db.CouponPriceOption.Where(g => g.CouponId == Id).Count();
            res.ClickThruCount = db.UserPurchasedCoupon.Where(g => g.CouponId == Id).Count();
            res.DealReviewsCount = db.CouponRatingReview.Where(g => g.CouponId == Id && g.StarRating !=null).Count();
            if (res.DealReviewsCount > 0)
            {
                res.DealRating = (((double)db.CouponRatingReview.Where(g => g.CouponId == Id).Sum(t => t.StarRating)) / res.DealReviewsCount)+5;
            }

            long C7daysOpen = db.UserCouponView.Where(g => g.CouponId == Id && g.ViewDateTime >= d7date).Count();
            long C14To7daysOpen = db.UserCouponView.Where(g => g.CouponId == Id && g.ViewDateTime >= d14date && g.ViewDateTime <= d7date).Count();
            if (C14To7daysOpen > 0 && C7daysOpen >0)
            {
                res.DealsOpenedComparison = Math.Abs(((double) C7daysOpen / (double)C14To7daysOpen));
                if (C7daysOpen == C14To7daysOpen)
                {
                    res.DealsOpenedComparison = 0;
                    res.DealsOpenedDirection = 0;
                }
                else if (C7daysOpen > C14To7daysOpen)
                {
                    res.DealsOpenedDirection = 1;
                }
                else
                {
                    res.DealsOpenedDirection = -1;
                }


            }
            else if (C14To7daysOpen == 0 && C7daysOpen == 0)
            {
                res.DealsOpenedDirection = 0;
                res.DealsOpenedComparison = 0;
            }
            else if (C14To7daysOpen > 0)
            {
                res.DealsOpenedComparison = C14To7daysOpen * 100;
                res.DealsOpenedDirection = -1;
            }
            else if (C7daysOpen > 0)
            {
                res.DealsOpenedComparison = C7daysOpen * 100;
                res.DealsOpenedDirection = 1;
            }
           //Click Thrus Comparison
            long C7daysClickThru = db.UserPurchasedCoupon.Where(g => g.CouponId == Id && g.PurchaseDateTime >= d7date ).Count();
            long C14To7ClickThru = db.UserPurchasedCoupon.Where(g => g.CouponId == Id && g.PurchaseDateTime >= d14date && g.PurchaseDateTime <= d7date).Count();
            if (C14To7ClickThru > 0 && C7daysClickThru > 0)
            {
                res.ClickThruComparison = Math.Abs(((double)C7daysClickThru / (double)C14To7ClickThru));
                if (C7daysClickThru == C14To7ClickThru)
                {
                    res.ClickThruComparison = 0;
                    res.ClickThruDirection = 0;
                }
                else if (C7daysClickThru > C14To7ClickThru)
                {
                    res.ClickThruDirection = 1;
                }
                else
                {
                    res.ClickThruDirection = -1;
                }
            }
            else if (C14To7ClickThru == 0 && C7daysClickThru == 0)
            {
                res.ClickThruDirection = 0;
                res.ClickThruComparison = 0;
            }
            else if (C14To7ClickThru > 0)
            {
                res.ClickThruComparison = C14To7ClickThru * 100;
                res.ClickThruDirection = -1;
            }
            else if (C7daysClickThru > 0)
            {
                res.ClickThruComparison = C7daysClickThru * 100;
                res.ClickThruDirection = 1;
            }
           



            return res;

        }

        #endregion
    }
}
