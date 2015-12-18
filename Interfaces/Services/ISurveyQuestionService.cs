
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

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
        SurveyQuestionResposneModelForAproval GetRejectedSurveyQuestionsForAproval(SurveySearchRequest request);

        /// <summary>
        /// Edit Survey Question | baqer
        /// </summary>
        SurveyQuestion EditSurveyQuestion(SurveyQuestion source);

        bool Create(SurveyQuestion survey);
        SurveyQuestionEditResponseModel GetSurveyQuestion(long SqId);

        /// <summary>
        /// Get Surveys For APi | baqer
        /// </summary>
        SurveyForApiSearchResponse GetSueveysForApi(GetSurveysApiRequest request);
    }
}
