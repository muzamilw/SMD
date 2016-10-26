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

        
        #region Public


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
                Expression<Func<Coupon, bool>> query =
                    campaign =>
                        (string.IsNullOrEmpty(request.SearchText) ||
                         (campaign.CouponTitle.Contains(request.SearchText)))
                         && (campaign.CompanyId == CompanyId || isAdmin) && campaign.Status != 7;


              
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

                //if (adCampaigns != null && adCampaigns.Count() > 0)
                //{
                //    foreach (var ad in adCampaigns)
                //    {
                     
                //    }

                //}
                return adCampaigns;
            }
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


        #endregion
    }
}
