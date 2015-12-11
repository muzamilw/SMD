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
        List<CampaignGridModel> GetCampaignByUserId();
        AdCampaignBaseResponse GetCampaignBaseData();
        AdCampaignBaseResponse SearchCountriesAndCities(string searchString);
        AdCampaignBaseResponse SearchLanguages(string searchString);
        bool CreateCampaign(AdCampaign campaignModel);

        /// <summary>
        /// Get Ad Campaigns that are need aprroval | baqer
        /// </summary>
        AdCampaignResposneModelForAproval GetAdCampaignForAproval(AdCampaignSearchRequest request);

        /// <summary>
        /// Update Ad CAmpaign  | baqer
        /// </summary>
        AdCampaign UpdateAdCampaign(AdCampaign source); 
        
           AdCampaignBaseResponse GetProfileQuestionData();
        AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId);

    }
}
