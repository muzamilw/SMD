
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
        /// Get Survey Questions that are need aprroval
        /// </summary>
        SurveyQuestionResposneModelForAproval GetRejectedSurveyQuestionsForAproval(SurveySearchRequest request);

        /// <summary>
        /// Edit Survey Question 
        /// </summary>
        SurveyQuestion EditSurveyQuestion(SurveyQuestion source);
    }
}
