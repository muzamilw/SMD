
using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Profile Question Answer Service Interface 
    /// </summary>
    public interface IProfileQuestionAnswerService
    {
        /// <summary>
        /// Get Answer by Profile Question Id 
        /// </summary>
        IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerByQuestionId(long profileQuestionId);
        IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerOrderBySortorder(long profileQuestionId);
    }
}
