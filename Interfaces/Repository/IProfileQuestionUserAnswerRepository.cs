
using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Profile Question User Answer Interface Model
    /// </summary>
    public interface IProfileQuestionUserAnswerRepository : IBaseRepository<ProfileQuestionUserAnswer, long>
    {
        /// <summary>
        /// Get Question's Answer
        /// </summary>
        IEnumerable<ProfileQuestionUserAnswer> GetProfileQuestionUserAnswerByQuestionId(UpdateProfileQuestionUserAnswerApiRequest request);
    }
}
