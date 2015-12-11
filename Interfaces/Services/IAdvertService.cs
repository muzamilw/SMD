using SMD.Models.DomainModels;
using SMD.Models.Common;
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
        AdCampaignBaseResponse GetProfileQuestionData();
        AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId);
    }
}
