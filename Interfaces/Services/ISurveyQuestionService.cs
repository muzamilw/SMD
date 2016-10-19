
using System.Security.Cryptography.X509Certificates;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Survey Question Service Interface 
    /// </summary>
    public interface ISurveyQuestionService
    {
        SurveyQuestionResponseModel GetSurveyQuestions(SurveySearchRequest request);
        SurveyQuestionResponseModel GetSurveyQuestions();

        /// <summary>
        /// Get Survey Questions that are need aprroval | baqer
        /// </summary>
        SurveyQuestionResposneModelForAproval GetSurveyQuestionsForAproval(SurveySearchRequest request);

        /// <summary>
        /// Edit Survey Question | baqer
        /// </summary>
        SurveyQuestion EditSurveyQuestion(SurveyQuestion source);

        bool Create(SurveyQuestion survey);
        bool Update(SurveyQuestion survey);
        SurveyQuestionEditResponseModel GetSurveyQuestion(long SqId);

        /// <summary>
        /// Get Surveys For APi | baqer
        /// </summary>
        SurveyForApiSearchResponse GetSueveysForApi(GetSurveysApiRequest request);

        /// <summary>
        /// Returns count of Matches Surveys| baqer
        /// </summary>
        long GetAudienceSurveyCount(GetAudienceSurveyRequest request);

        /// <summary>
        /// Returns count of Matches AdCampaigns| baqer
        /// </summary>
        long GetAudienceAdCampaignCount(GetAudienceSurveyRequest request);


        /// <summary>
        /// Stripe Work
        /// </summary>
        string CreateChargeWithCustomerId(int? amount, string customerId);


        GetAudience_Result GetAudienceCount(GetAudienceCountRequest request);

        /// <summary>
        /// Get Survey By Id
        /// </summary>
        SurveyQuestion GetSurveyQuestionById(long sqid);
        IEnumerable<getPollsBySQID_Result> getPollsBySQIDAnalytics(int SQId, int CampStatus, int dateRange, int Granularity);
        IEnumerable<getPollBySQIDRatioAnalytic_Result> getPollBySQIDRatioAnalytic(int ID, int dateRange);
      
    }
}
