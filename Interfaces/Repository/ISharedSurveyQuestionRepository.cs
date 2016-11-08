using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ISharedSurveyQuestionRepository : IBaseRepository<SharedSurveyQuestion, long>
    {

        GetSharedSurveyQuestion_Result GetSharedSurveyQuestionDetails(long SSQID);
        bool DeleteSharedSurveyQuestion(long SSQID);
    }
}
