using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignRepository : IBaseRepository<AdCampaign, long>
    {
        List<CampaignGridModel> GetCampaignByUserId();

        /// <summary>
        /// Get Ad Campaigns
        /// </summary>
        IEnumerable<AdCampaign> SearchAdCampaigns(AdCampaignSearchRequest request, out int rowCount);
    }
}
