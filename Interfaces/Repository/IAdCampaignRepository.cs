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
        IEnumerable<AdCampaign> SearchAdCampaignsForApproval(AdCampaignSearchRequest request, out int rowCount);

        IEnumerable<AdCampaign> SearchCampaign(AdCampaignSearchRequest request, out int rowCount);


        IEnumerable<SearchCampaigns_Result> SearchCampaigns(AdCampaignSearchRequest request, out int rowCount);
        

        IEnumerable<AdCampaign> GetAdCampaignById(long campaignId);
        UserAndCostDetail GetUserAndCostDetail();
        User GetUserById();
      
        List<GetCoupons_Result> GetCoupons(string UserId);
        IEnumerable<CompanyBranch> GetAllBranches();

        string CampaignVerifyQuestionById(int CampaignID);
        AdCampaign GetCampaignByID(long CampaignID);
        IEnumerable<getAdsCampaignByCampaignId_Result> getAdsCampaignByCampaignIdForAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity);
        IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getDisplayAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity);
        
    }
}
