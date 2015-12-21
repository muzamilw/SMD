using SMD.Models.DomainModels;
using SMD.Models.Common;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IAdvertService
    {
       
        AdCampaignBaseResponse GetCampaignBaseData();
        AdCampaignBaseResponse SearchCountriesAndCities(string searchString);
        AdCampaignBaseResponse SearchLanguages(string searchString);
        void CreateCampaign(AdCampaign campaignModel);

        /// <summary>
        /// Get Ad Campaigns that are need aprroval | baqer
        /// </summary>
        AdCampaignResposneModelForAproval GetAdCampaignForAproval(AdCampaignSearchRequest request);

        /// <summary>
        /// Update Ad CAmpaign  | baqer
        /// </summary>
        AdCampaign UpdateAdCampaign(AdCampaign source);

        /// <summary>
        /// Get Ads For API  | baqer
        /// </summary>
        AdCampaignApiSearchRequestResponse GetAdCampaignsForApi(GetAdsApiRequest source);

        AdCampaignBaseResponse GetProfileQuestionData();
        AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId);
        AdCampaignBaseResponse GetSurveyQuestionData(long surveyId);
        CampaignResponseModel GetCampaigns(AdCampaignSearchRequest request);
        CampaignResponseModel GetCampaignById(long CampaignId);
    }
}
