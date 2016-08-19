using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ISurveyQuestionRepository : IBaseRepository<SurveyQuestion, long>
    {
        IEnumerable<SurveyQuestion> UpdateQuestionsListCompanyID(IEnumerable<SurveyQuestion> SurveyQuestions);
        IEnumerable<SurveyQuestion> SearchSurveyQuestions(SurveySearchRequest request, out int rowCount);

        /// <summary>
        /// Get Rejected Survey Questions | baqer
        /// </summary>
        IEnumerable<SurveyQuestion> SearchRejectedProfileQuestions(SurveySearchRequest request, out int rowCount);

        IEnumerable<SurveyQuestion> GetAll();

        SurveyQuestion Get(long SqId);

        /// <summary>
        /// Get Ads Campaigns | SP-API | baqer
        /// </summary>
        IEnumerable<GetAds_Result> GetAdCompaignForApi(GetAdsApiRequest request);

        /// <summary>
        /// Get Surveys | SP-API | baqer
        /// </summary>
        IEnumerable<GetSurveys_Result> GetSurveysForApi(GetSurveysApiRequest request);

        /// <summary>
        /// Returns count of Matches Surveys| baqer
        /// </summary>
        long GetAudienceSurveyCount(GetAudienceSurveyRequest request);


        /// <summary>
        /// Returns count of Matches AdCampaigns | baqer
        /// </summary>
        long GetAudienceAdCampaignCount(GetAudienceSurveyRequest request);
        GetAudience_Result GetAudienceCount(GetAudienceCountRequest request);
        UserBaseData getBaseData();
    }
}
