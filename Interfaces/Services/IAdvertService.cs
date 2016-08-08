using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Services
{
    public interface IAdvertService
    {
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


        //CouponCodeModel GenerateCouponCodes(int numbers, long CampaignId);
        
    }
}
