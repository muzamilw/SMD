using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question Answer Service 
    /// </summary>
    public sealed class ProfileQuestionAnswerService  : IProfileQuestionAnswerService 
    {
        #region Private
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>

        public ProfileQuestionAnswerService(IProfileQuestionAnswerRepository profileQuestionAnswerRepository)
        {
            _profileQuestionAnswerRepository = profileQuestionAnswerRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Answer by Profile Question Id 
        /// </summary>
        public IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerByQuestionId(long profileQuestionId)
        {
            return _profileQuestionAnswerRepository.GetProfileQuestionAnswerByQuestionId(profileQuestionId);
        }
        public IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerOrderBySortorder(long profileQuestionId)
        {
            return _profileQuestionAnswerRepository.GetAllProfileQuestionAnswerOrderbySortOrder(profileQuestionId);
        }
        #endregion
    }
}
