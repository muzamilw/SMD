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

        IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity);
        IEnumerable<getAdsCampaignByCampaignIdRatioAnalytic_Result> getAdsCampaignByCampaignIdRatioAnalytic(int ID, int dateRange);
        IEnumerable<getAdsCampaignByCampaignIdtblAnalytic_Result> getAdsCampaignByCampaignIdtblAnalytic(int ID);
        IEnumerable<getCampaignROItblAnalytic_Result> getCampaignROItblAnalytic(int ID);

        IEnumerable<AdCampaign> GetSpecialAdCampaigns(out string CompanyLogoPath);
        List<getCampaignByIdFormDataAnalytic_Result> getCampaignByIdFormDataAnalytic(long CampaignId);
        List<GetRandomAdCampaign_Result> GetRandomAdCampaign(int Type);
        List<getAdsCampaignPerCityPerGenderFormAnalytic_Result> getAdsCampaignPerCityPerGenderFormAnalytic(long _Id);
        
        List<getAdsCampaignPerCityPerAgeFormAnalytic_Result> getAdsCampaignPerCityPerAgeFormAnalytic(long _Id);
       List<getCampaignImpressionByAgeByCId_Result> getCampaignImpressionByAgeByCId(long CampaignId);
        List<getCampaignImpressionByProfessionByCId_Result> getCampaignImpressionByProfessionByCId(long CampaignId);
        List<getCampaignImpressionByGenderByCId_Result> getCampaignImpressionByGenderByCId(long CampaignId);
        

    }
}
