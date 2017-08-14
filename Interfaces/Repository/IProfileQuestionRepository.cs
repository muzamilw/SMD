using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.Common;

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
        /// 

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
        IEnumerable<ProfileQuestion> UpdateQuestionsCompanyID(IEnumerable<ProfileQuestion> ProfileQuestions);
        void AddProfileQuestions(ProfileQuestion Obj);
        UserBaseData getBaseData();
        IEnumerable<ProfileQuestion> GetProfileQuestionsForApproval(GetPagedListRequest request, out int rowCount);
        IEnumerable<getSurvayByPQID_Result> getSurvayByPQIDAnalytics(int PQId, int CampStatus, int dateRange, int Granularity);
        IEnumerable<getSurveyByPQIDRatioAnalytic_Result> getSurveyByPQIDRatioAnalytic(int ID, int dateRange);
        IEnumerable<getSurvayByPQIDtblAnalytic_Result> getSurvayByPQIDtblAnalytic(int ID);

        IEnumerable<GetUserProfileQuestionsList_Result> GetUserProfileQuestionsList(string UserID);
        
    }

}
