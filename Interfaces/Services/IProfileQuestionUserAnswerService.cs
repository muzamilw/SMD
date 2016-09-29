
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Profile Question User Answer Interface
    /// </summary>
    public interface IProfileQuestionUserAnswerService
    {
        /// <summary>
        /// Update user's answer for question
        /// </summary>
        string SaveProfileQuestionUserResponse(UpdateProfileQuestionUserAnswerApiRequest request);
    }
}
