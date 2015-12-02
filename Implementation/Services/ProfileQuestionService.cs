using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question Service 
    /// </summary>
    public sealed class ProfileQuestionService : IProfileQuestionService
    {
        #region Private
        private readonly IProfileQuestionRepository _profileQuestionRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>

        public ProfileQuestionService(IProfileQuestionRepository profileQuestionRepository)
        {
            _profileQuestionRepository = profileQuestionRepository;
        }

        #endregion
        #region Public
        /// <summary>
        /// Profile Question Search request 
        /// </summary>
        public ProfileQuestionSearchRequestResponse GetProfileQuestions(ProfileQuestionSearchRequest request)
        {
            int rowCount;
            return new ProfileQuestionSearchRequestResponse
            {
                ProfileQuestions = _profileQuestionRepository.SearchProfileQuestions(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Delete Profile Question
        /// </summary>
        public bool DeleteProfileQuestion(ProfileQuestion profileQuestion)
        {
            var dBprofileQuestion=_profileQuestionRepository.Find(profileQuestion.PqId);
            if (dBprofileQuestion != null)
            {
                dBprofileQuestion.Status = 0;     // 0 -> archived  || 1 -> active
                _profileQuestionRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
