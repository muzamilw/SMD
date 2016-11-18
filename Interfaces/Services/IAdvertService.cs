using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;

namespace SMD.Interfaces.Services
{
    public interface IAdvertService
    {
        AdCampaignBaseResponse GetSurveyQuestionAnser(long SqID);
        AdCampaignBaseResponse GetALLSurveyQuestionData();

        AdCampaignBaseResponse GetSurveyQuestionDataByCompanyId();

        AdCampaignBaseResponse GetCampaignBaseData();
        AdCampaignBaseResponse SearchCountriesAndCities(string searchString);
        AdCampaignBaseResponse SearchLanguages(string searchString);
        IEnumerable<Industry> SearchIndustry(string searchString);
        IEnumerable<Education> SearchEducation(string searchString);
        void CreateCampaign(AdCampaign campaignModel);

        /// <summary>
        /// Get Ad Campaigns that are need aprroval | baqer
        /// </summary>
        AdCampaignResposneModelForAproval GetAdCampaignForAproval(AdCampaignSearchRequest request);

        /// <summary>
        /// Update Ad CAmpaign  | baqer
        /// </summary>
        //AdCampaign UpdateAdCampaign(AdCampaign source);

        /// <summary>
        /// Get Ads For API  | baqer
        /// </summary>
        AdCampaignApiSearchRequestResponse GetAdCampaignsForApi(GetAdsApiRequest source);

        AdCampaignBaseResponse GetProfileQuestionData();
        AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId);
        AdCampaignBaseResponse GetSurveyQuestionData(long surveyId);
        CampaignResponseModel GetCampaigns(AdCampaignSearchRequest request);

        CampaignSearchResponseModel SearchCampaigns(AdCampaignSearchRequest request);

        CampaignResponseModel GetCampaignById(long CampaignId);
        void UpdateCampaign(AdCampaign campaignModel);

        /// <summary>
        /// Get Ad Campaign By Id
        /// </summary>
        AdCampaign GetAdCampaignById(long campaignId);

        long CopyAddCampaigns(long CampaignId);
        AdCampaignBaseResponse getQuizCampaigns();
        AdCampaignBaseResponse getCompanyBranches();
        
        List<GetCoupons_Result> GetCoupons(string UserId);


        AdCampaign SendApprovalRejectionEmail(AdCampaign source);
        string UpdateAdApprovalCampaign(AdCampaign source);
        IEnumerable<getCampaignsByStatus_Result> getCampaignsByStatus();
        IEnumerable<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity);
       

        //CouponCodeModel GenerateCouponCodes(int numbers, long CampaignId);
        IEnumerable<getDisplayAdsCampaignByCampaignIdAnalytics_Result> getAdsCampaignByCampaignIdAnalytics(int compaignId, int CampStatus, int dateRange, int Granularity);
        IEnumerable<getAdsCampaignByCampaignIdRatioAnalytic_Result> getAdsCampaignByCampaignIdRatioAnalytic(int ID, int dateRange);
        IEnumerable<getAdsCampaignByCampaignIdtblAnalytic_Result> getAdsCampaignByCampaignIdtblAnalytic(int ID);
        IEnumerable<getCampaignROItblAnalytic_Result> getCampaignROItblAnalytic(int ID);

        
        
    }
}
