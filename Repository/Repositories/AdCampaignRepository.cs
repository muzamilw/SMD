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
        public IEnumerable<AdCampaign> SearchAdCampaigns(AdCampaignSearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<AdCampaign, bool>> query =
                ad => ad.Status == (Int32)AdCampaignStatus.SubmitForApproval;

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(addCampaignByClause[request.AdCampaignOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)                   
                : DbSet.Where(query)
                    .OrderByDescending(addCampaignByClause[request.AdCampaignOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AdCampaign> SearchCampaign(AdCampaignSearchRequest request, out int rowCount)
        {
            bool isAdmin = false;
             var users = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
            if (users.Roles.FirstOrDefault().Name == Roles.Adminstrator)
                isAdmin = true;
            if (request == null)
            {
                const int fromRow = 0;
                const int toRow = 10;
                rowCount = DbSet.Count();
                if (isAdmin)
                {
                    return DbSet.OrderBy(g => g.CampaignId)
                            .Skip(fromRow)
                            .Take(toRow)
                            .ToList();
                }
                else
                {
                    return DbSet.Where(g => g.UserId == LoggedInUserIdentity).OrderBy(g => g.CampaignId)
                            .Skip(fromRow)
                            .Take(toRow)
                            .ToList();
                }
            } 
            else
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<AdCampaign, bool>> query =
                    campaign =>
                        (string.IsNullOrEmpty(request.SearchText) ||
                         (campaign.DisplayTitle.Contains(request.SearchText)))
                         && (campaign.UserId == LoggedInUserIdentity || isAdmin);


                rowCount = DbSet.Count(query);
                return DbSet.Where(query).OrderBy(g => g.CampaignId)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
            }
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
                        join prod in db.Products on usr.CountryId equals prod.CountryId
                        where usr.Id == LoggedInUserIdentity && prod.ProductId == 2 //&& prod.ProductCode == code
                        select new UserAndCostDetail
                        {
                            AgeClausePrice = prod.AgeClausePrice,
                            CityId = usr.CityId,
                            CountryId = usr.CountryId,
                            EducationClausePrice = prod.EducationClausePrice,
                            EducationId = usr.EducationId,
                            GenderClausePrice = prod.GenderClausePrice,
                            IndustryId = usr.IndustryId,
                            LanguageId = usr.LanguageId,
                            LocationClausePrice = prod.LocationClausePrice,
                            OtherClausePrice = prod.OtherClausePrice,
                            ProfessionClausePrice = prod.ProfessionClausePrice
                        };

            return query.FirstOrDefault();
        }
    }
}
