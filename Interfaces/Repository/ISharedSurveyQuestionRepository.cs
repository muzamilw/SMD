using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ISharedSurveyQuestionRepository : IBaseRepository<SharedSurveyQuestion, long>
    {


        List<GetSharedSurveyQuestionsByUserId_Result> GetSharedSurveysByuserID(string UserId);

        GetSharedSurveyQuestion_Result GetSharedSurveyQuestionDetails(long SSQID);
        bool DeleteSharedSurveyQuestion(long SSQID);

    }
}
