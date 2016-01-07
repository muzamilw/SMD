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
using System.Text;
using System.Threading.Tasks;
using SMD.Models.IdentityModels;

namespace SMD.Repository.Repositories
{
    public class AdCampaignRepository : BaseRepository<AdCampaign>, IAdCampaignRepository
    {

        /// <summary>
        ///Ad Campaign Orderby clause
        /// </summary>
        private readonly Dictionary<AdCampaignByColumn, Func<AdCampaign, object>> _addCampaignByClause =
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
        /// Gets Combination of Ads, Surveys, Questions in a paged view
        /// </summary>
        public IEnumerable<GetProducts_Result> GetProducts(GetProductsRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
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
                    .OrderBy(_addCampaignByClause[request.AdCampaignOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(_addCampaignByClause[request.AdCampaignOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AdCampaign> SearchCampaign(AdCampaignSearchRequest request, out int rowCount)
        {
            if (request == null)
            {
                int fromRow = 0;
                int toRow = 10;
                rowCount = DbSet.Count();
                return DbSet.Where(g => g.UserId == LoggedInUserIdentity).OrderBy(g => g.CampaignId)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
            }
            else
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<AdCampaign, bool>> query =
                    campaign =>
                        (string.IsNullOrEmpty(request.SearchText) ||
                         (campaign.DisplayTitle.Contains(request.SearchText)))
                         && (campaign.UserId == LoggedInUserIdentity);


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
        public IEnumerable<AdCampaign> GetAdCampaignById(long CampaignId)
        {

            Expression<Func<AdCampaign, bool>> query =
                ad => ad.CampaignId == CampaignId;

            return DbSet.Where(query);
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        public User GetUserById()
        {

            return db.Users.Where(i => i.Id == LoggedInUserIdentity).SingleOrDefault();
        }
        /// <summary>
        /// Get User And Cost
        /// </summary>
        public UserAndCostDetail GetUserAndCostDetail()
        {
            
            var query = from usr in db.Users.Include("Cities").Include("Countries")
                        join prod in db.Products on usr.CountryId equals prod.CountryId
                        where usr.Id == LoggedInUserIdentity && prod.ProductId == 2 //&& prod.ProductCode == code
                        select new UserAndCostDetail()
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
                            ProfessionClausePrice = prod.ProfessionClausePrice//,
                            //CityName = usr.Cities.Cities != null ? usr.Cities.CityName : "",
                            //CountryName = usr.Countries != null ? usr.Countries.CountryName : "",
                            //EducationTitle = usr.Education != null?usr.Education.Title : "",
                            //IndustryName = usr.Industry != null?usr.Industry.IndustryName:"",
                            //LanguageName = usr.Language != null? usr.Language.LanguageName: ""
                           
                        };

            return query.FirstOrDefault();
        }
    }
}
