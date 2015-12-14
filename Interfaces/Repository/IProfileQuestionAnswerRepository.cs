using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Profile Question Answer Repository Interface 
    /// </summary>
    public interface IProfileQuestionAnswerRepository : IBaseRepository<ProfileQuestionAnswer, int>
    {
        /// <summary>
        /// Get Answer by Profile Question Id 
        /// </summary>
        IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerByQuestionId(long profileQuestionId);

        IEnumerable<ProfileQuestionAnswer> GetAllProfileQuestionAnswerByQuestionId(int profileQuestionId);
    }
}
