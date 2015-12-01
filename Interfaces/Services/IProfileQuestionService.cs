
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Profile Question Service Interface 
    /// </summary>
    public interface IProfileQuestionService
    {
        /// <summary>
        /// Profile Question Search request 
        /// </summary>
        ProfileQuestionSearchRequestResponse GetProfileQuestions(ProfileQuestionSearchRequest request);
    }
}
