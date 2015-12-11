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
                        {AdCampaignByColumn.CreationDate, d => d.CreatedDateTime} 
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

        public List<CampaignGridModel> GetCampaignByUserId()
        {
            var query = from ad in db.AdCampaigns
                        where ad.UserId == LoggedInUserIdentity
                        select new CampaignGridModel()
                        {
                            AmountSpent = ad.AmountSpent ?? 0.0,
                            CampaignId = ad.CampaignId,
                            DisplayTitle = ad.DisplayTitle,
                            StartDateTime = ad.StartDateTime ?? DateTime.Now,
                            Status = ad.Status ?? 0,
                            ClickRate = ad.ClickRate ?? 0.0,
                            EndDateTime = ad.EndDateTime ?? DateTime.Now,
                            MaxBudget = ad.MaxBudget ?? 0.0,
                            ResultClicks = ad.ResultClicks ?? 0,
                            StatusName = ad.Status == (int)AdCampaignStatus.Draft ? AdCampaignStatus.Draft.ToString() : ad.Status == (int)AdCampaignStatus.SubmitForApproval ? AdCampaignStatus.SubmitForApproval.ToString() : ad.Status == (int)AdCampaignStatus.Completed ? AdCampaignStatus.Completed.ToString() : ad.Status == (int)AdCampaignStatus.Live ? AdCampaignStatus.Live.ToString() : ad.Status == (int)AdCampaignStatus.Paused ? AdCampaignStatus.Paused.ToString() : ad.Status == (int)AdCampaignStatus.ApprovalRejected ? AdCampaignStatus.ApprovalRejected.ToString() : "NONE",
                            StatusColor = ad.Status == (int)AdCampaignStatus.Draft ? "#cccccc" : ad.Status == (int)AdCampaignStatus.SubmitForApproval ? "#e74c3c" : ad.Status == (int)AdCampaignStatus.Completed ? "#52AE27" : ad.Status == (int)AdCampaignStatus.Live ? "#52AE27" : ad.Status == (int)AdCampaignStatus.Paused ? "#f1c40f" : ad.Status == (int)AdCampaignStatus.ApprovalRejected ? "#e74c3c" : "#cccccc"
                        };

            return query.ToList<CampaignGridModel>();

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
    }
}
