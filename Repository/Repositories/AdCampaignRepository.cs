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
    public class AdCampaignRepository : BaseRepository<AdCampaign>, IAdCampaignRepository
    {

        /// <summary>
        ///Ad Campaign Orderby clause
        /// </summary>
        private readonly Dictionary<AdCampaignByColumn, Func<AdCampaign, object>> addCampaignByClause =
            new Dictionary<AdCampaignByColumn, Func<AdCampaign, object>>
                    {
                        {AdCampaignByColumn.Name, d => d.CampaignName}  ,    
                        {AdCampaignByColumn.Description, d => d.Description} ,     
                        {AdCampaignByColumn.Type, d => d.Type} ,     
                        {AdCampaignByColumn.CreatedBy, d => d.CreatedBy}  ,    
                        {AdCampaignByColumn.CreationDate, d => d.CreatedDateTime} ,
                        {AdCampaignByColumn.ClickRate, d => d.ClickRate} 
                    };

        public AdCampaignRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AdCampaign> DbSet
        {
            get
            {
                return db.AdCampaigns;
            }
        }

        /// <summary>
        /// Page Size for Products Feed
        /// </summary>
        protected int PageSizeForProducts
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// Resets Users Responses for Ads, Surveys and Questions
        /// </summary>
        public void ResetUserProductsResponses()
        {
            db.ResetProductsUserResponses();
        }

        /// <summary>
        /// Gets Combination of Ads, Surveys, Questions in a paged view
        /// </summary>
        public IEnumerable<GetProducts_Result> GetProducts(GetProductsRequest request)
        {
            int fromRow = (request.PageNo - 1) * PageSizeForProducts;
            int toRow = PageSizeForProducts;
            return db.GetProducts(request.UserId, fromRow, toRow);
        }

        /// <summary>
        /// Get Ad Campaigns
        /// </summary>
        public IEnumerable<AdCampaign> SearchAdCampaignsForApproval(AdCampaignSearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<AdCampaign, bool>> query =
                ad => ad.Status == (Int32)AdCampaignStatus.SubmitForApproval && ad.Type==request.status;

            //if (request.ShowCoupons.HasValue && request.ShowCoupons.Value == true)
            //    query = ad => ad.Status == (Int32)AdCampaignStatus.SubmitForApproval && ad.Type == (int)AdCampaignType.Coupon;

            rowCount = DbSet.Count(query);
            //var res =  request.IsAsc
            //    ? DbSet.Where(query)
            //        .OrderBy(addCampaignByClause[request.AdCampaignOrderBy])
            //        .Skip(fromRow)
            //        .Take(toRow)
            //    : DbSet.Where(query)
            //        .OrderByDescending(addCampaignByClause[request.AdCampaignOrderBy])
            //        .Skip(fromRow)
            //        .Take(toRow)
            //        .ToList();
            //res = res.OrderByDescending(g => g.priority);
            var res = DbSet.Where(query)
                    .OrderByDescending(g => g.SubmissionDateTime);
            return res.Skip(fromRow)
                    .Take(toRow);

        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AdCampaign> SearchCampaign(AdCampaignSearchRequest request, out int rowCount)
        {

            


            bool isAdmin = false;
            var users = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
           
            if (request == null)
            {


             
                
                const int fromRow = 0;
                const int toRow = 10;
                rowCount = DbSet.Where(c => c.Type != 5).Count();
                if (isAdmin)
                {
                    IEnumerable<AdCampaign> adCampaigns = DbSet.OrderByDescending(g => g.priority).Where(c => c.Type != 5)
                           .Skip(fromRow)
                           .Take(toRow)
                           .ToList();

                    if (adCampaigns != null && adCampaigns.Count() > 0)
                    {
                        foreach (var ad in adCampaigns)
                        {
                            ad.AdViews = db.AdCampaignResponses.Where(c => c.CampaignId == ad.CampaignId).Count();
                        }

                    }
                    return adCampaigns;


                }
                else
                {
                    IEnumerable<AdCampaign> adCampaigns = DbSet.Where(g => g.CompanyId == CompanyId && g.Type != 5).OrderByDescending(g => g.priority)
                           .Skip(fromRow)
                           .Take(toRow)
                           .ToList();
                    if (adCampaigns != null && adCampaigns.Count() > 0)
                    {
                        foreach (var ad in adCampaigns)
                        {
                            ad.AdViews = db.AdCampaignResponses.Where(c => c.CampaignId == ad.CampaignId).Count();
                        }

                    }
                    return adCampaigns;

                }
            }
            else   //searching mode
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<AdCampaign, bool>> query =
                    campaign =>
                        (string.IsNullOrEmpty(request.SearchText) ||
                         (campaign.DisplayTitle.Contains(request.SearchText)) || (campaign.CampaignName.Contains(request.SearchText))
                          || (campaign.Description.Contains(request.SearchText)) || (campaign.CampaignDescription.Contains(request.SearchText)))
                         && (campaign.CompanyId == CompanyId || isAdmin);


                rowCount = DbSet.Where(c => c.Type != 5).Count(query);
                IEnumerable<AdCampaign> adCampaigns = null;
                if (request.ShowCoupons != null && request.ShowCoupons == true)
                {
                    rowCount = DbSet.Where(c => c.Type == 5).Count();
                    adCampaigns = DbSet.Where(query).Where(g => g.Type == 5).OrderByDescending(g => g.priority)
                      .Skip(fromRow)
                      .Take(toRow)
                      .ToList();
                }
                else
                {
                    adCampaigns = DbSet.Where(query).Where(g => g.Type != 5).OrderByDescending(g => g.priority)
                         .Skip(fromRow)
                         .Take(toRow)
                         .ToList();
                }

                if (adCampaigns != null && adCampaigns.Count() > 0)
                {
                    foreach (var ad in adCampaigns)
                    {
                        ad.AdViews = db.AdCampaignResponses.Where(c => c.CampaignId == ad.CampaignId).Count();
                    }

                }
                return adCampaigns;
            }
        }



        public IEnumerable<SearchCampaigns_Result> SearchCampaigns(AdCampaignSearchRequest request, out int rowCount)
        {
            var results =  db.SearchCampaigns(request.status, request.SearchText,this.CompanyId, (request.PageNo - 1) * request.PageSize, request.PageSize, false).Where(g=>g.Type == request.mode).ToList();

            if (results.Count() > 0)
            {
                var firstrec = results.First();
                rowCount = firstrec.TotalItems.Value;
            }
            else
                rowCount = 0;

            //rowCount = 211;
            return results;
        }
        /// <summary>
        /// Get Ad Campaign by id
        /// </summary>
        public IEnumerable<AdCampaign> GetAdCampaignById(long campaignId)
        {

            Expression<Func<AdCampaign, bool>> query =
                ad => ad.CampaignId == campaignId;

            return DbSet.Where(query);

        }
        /// <summary>
        /// Get user by id
        /// </summary>
        public User GetUserById()
        {

            return db.Users.SingleOrDefault(i => i.Id == LoggedInUserIdentity);
        }
        /// <summary>
        /// Get User And Cost
        /// </summary>
        public UserAndCostDetail GetUserAndCostDetail()
        {

            var query = from usr in db.Users.Include("Cities").Include("Countries")
                        join prod in db.Products on usr.Company.CountryId equals prod.CountryId
                        where usr.Id == LoggedInUserIdentity && prod.ProductId == 2 //&& prod.ProductCode == code
                        select new UserAndCostDetail
                        {
                            AgeClausePrice = prod.AgeClausePrice ?? 0,
                            City = usr.Company.City,
                            CountryId = usr.Company.CountryId,
                            EducationClausePrice = prod.EducationClausePrice ?? 0,
                            EducationId = usr.EducationId,
                            GenderClausePrice = prod.GenderClausePrice ?? 0,
                            IndustryId = usr.IndustryId,
                            LanguageId = usr.LanguageId,
                            LocationClausePrice = prod.LocationClausePrice ?? 0,
                            OtherClausePrice = prod.OtherClausePrice ?? 0,
                            ProfessionClausePrice = prod.ProfessionClausePrice ?? 0
                        };

            return query.FirstOrDefault();
        }

        public List<GetCoupons_Result> GetCoupons(string UserId)
        {
         
            return db.GetCoupons(UserId).ToList();

            //var query = from ad in db.AdCampaigns
            //            where ad.Type == 5 && (ad.CouponTakenCount == null || ad.CouponTakenCount < ad.CouponQuantity)
            //            && (ad.Archived == null || ad.Archived == false)
            //            select new Coupons
            //            {
            //                CouponId = ad.CampaignId,
            //                CouponActualValue = ad.CouponActualValue,
            //                CouponName = ad.CampaignName,
            //                CouponTitle = ad.DisplayTitle,
            //                Firstline = ad.Description,
            //                SecondLine = ad.CampaignDescription,
            //                CouponSwapValue = ad.CouponSwapValue,
            //                CouponImage = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/" + ad.ImagePath
            //            };

            //return query.ToList<Coupons>();
        }

        public IEnumerable<CompanyBranch> GetAllBranches()
        {
            var user = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
            if (user == null)
                return null;
            var comp = db.Companies.Where(g => g.CompanyId == user.CompanyId).SingleOrDefault();
            if (comp == null)
                return null;
            var branches = db.CompanyBranches.Where(g => g.CompanyId == comp.CompanyId);
            return branches;
        }
        public string CampaignVerifyQuestionById(int CampaignID)
        {
            return DbSet.Where(i => i.CampaignId == CampaignID).FirstOrDefault().VerifyQuestion;
        
        }
        public AdCampaign GetCampaignByID(long CampaignID)
        {
            return DbSet.Where(i => i.CampaignId == CampaignID).FirstOrDefault();
        
        }
        public IEnumerable<getAdsCampaignByCampaignId_Result> getAdsCampaignByCampaignIdForAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {

            return db.getAdsCampaignByCampaignId(compaignId, CampStatus, dateRange, Granularity);
        }
        public IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getDisplayAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity)
        {
           
            return db.getDisplayAdsCampaignByCampaignIdAnalytics(compaignId, CampStatus, dateRange, Granularity);
        }
    }
}
