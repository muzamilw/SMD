using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Survey Question Response Repository Interface
    /// </summary>
    public interface ISurveyQuestionResponseRepository : IBaseRepository<SurveyQuestionResponse, long>
    {
        /// <summary>
        /// Returns Response against user
        /// </summary>
        SurveyQuestionResponse GetByUserId(long sqId, string userId);
    }
}
