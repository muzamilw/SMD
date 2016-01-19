using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignRepository : IBaseRepository<AdCampaign, long>
    {
        /// <summary>
        /// Reset User Products Responses
        /// </summary>
        void ResetUserProductsResponses();

        /// <summary>
        /// Gets Ads, Surveys, Questions as paged view
        /// </summary>
        IEnumerable<GetProducts_Result> GetProducts(GetProductsRequest request);

        /// <summary>
        /// Get Ad Campaigns
        /// </summary>
        IEnumerable<AdCampaign> SearchAdCampaigns(AdCampaignSearchRequest request, out int rowCount);

        IEnumerable<AdCampaign> SearchCampaign(AdCampaignSearchRequest request, out int rowCount);

        IEnumerable<AdCampaign> GetAdCampaignById(long CampaignId);
        UserAndCostDetail GetUserAndCostDetail();
        User GetUserById();
       
    }
}
