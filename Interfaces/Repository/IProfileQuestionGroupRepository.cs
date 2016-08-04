using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Profile Question Group Repository Interface 
    /// </summary>
    public interface IProfileQuestionGroupRepository : IBaseRepository<ProfileQuestionGroup, int>
    {
        /// <summary>
        /// Get List of Profile Question Groups 
        /// </summary>
        IEnumerable<ProfileQuestionGroup> GetAllProfileQuestionGroups(); 
    }
}
