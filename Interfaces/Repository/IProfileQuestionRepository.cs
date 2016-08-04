using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Profile Question Repository Interface 
    /// </summary>
    public interface IProfileQuestionRepository : IBaseRepository<ProfileQuestion, int>
    {
        /// <summary>
        /// Search Profile Question 
        /// </summary>
        IEnumerable<ProfileQuestion> SearchProfileQuestions(ProfileQuestionSearchRequest request, out int rowCount);

        /// <summary>
        /// Get All Profile Questions
        /// </summary>
        IEnumerable<ProfileQuestion> GetAllProfileQuestions();

        
        /// <summary>
        /// Get Un-answered Profile Questions by Group Id 
        /// </summary>
        IEnumerable<ProfileQuestion> GetUnansweredQuestionsByGroupId(GetProfileQuestionApiRequest request);

        /// <summary>
        /// Get Count of PQ For Group Id
        /// </summary>
        int GetTotalCountOfGroupQuestion(double groupId);

        /// <summary>
        /// Get Answered Questions count 
        /// </summary>
        int GetCountOfUnAnsweredQuestionsByGroupId(double groupId, string userId);

    }
}
