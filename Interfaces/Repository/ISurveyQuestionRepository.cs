using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
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
        IEnumerable<SurveyQuestion> GetSurveyQuestionsForAproval(SurveySearchRequest request, out int rowCount);

        IEnumerable<SurveyQuestion> GetAll();

        IEnumerable<SurveyQuestion> GetAllByCompanyId();

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
        IEnumerable<SurveyQuestion> GetSurveyQuestionAnswer(long SurveyQuestionId);
        IEnumerable<getPollsBySQID_Result> getPollsBySQIDAnalytics(int SQId, int CampStatus, int dateRange, int Granularity);
        List<getPollBySQIDRatioAnalytic_Result> getPollBySQIDRatioAnalytic(int ID, int dateRange);
        IEnumerable<getPollBySQIDtblAnalytic_Result> getPollBySQIDtblAnalytic(int ID);
        List<GetRandomPolls_Result> GetRandomPolls();
        int getPollImpressionStatBySQIdFormAnalytic(long Id, int Gender, int age);
        
        
    }
}
